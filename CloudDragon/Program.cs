﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using CloudDragon;
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using DotNetEnv;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragonLib.Models;
using System.Collections.Generic;
using CloudDragon.Models.ModelContext;

namespace CloudDragon
{
    /// <summary>
    /// Console entry point for interacting with the Cloud Dragon sample data.
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// Runs the interactive character creation workflow.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        private static async Task Main(string[] args)
        {
            Env.Load();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Cosmos_Loader cosmosLoader = new Cosmos_Loader(configuration);
            Console.WriteLine("\nQuerying all items from all containers...");
            await cosmosLoader.QueryAllItemsFromAllContainersAsync();

            var repository = new CosmosCharacterRepository(cosmosLoader);
            var llmService = new MockLlmService();
            var engine = new CharacterContextEngine(llmService, repository);

            var character = new CharacterModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = Prompt("Enter character name: "),
                Race = Prompt("Enter character race: "),
                Class = Prompt("Enter character class: "),
                Level = int.TryParse(Prompt("Enter level (default 1): "), out var lvl) ? lvl : 1
            };

            // Optional age input
            string ageInput = Prompt("Enter character age (or leave blank): ");
            if (int.TryParse(ageInput, out int age))
                character.Age = age;

            // Optional appearance input
            string appearanceInput = Prompt("Enter appearance (or type 'generate'): ");
            character.Appearance = string.Equals(appearanceInput, "generate", StringComparison.OrdinalIgnoreCase)
                ? await engine.GenerateAppearanceAsync(character)
                : appearanceInput;

            // Optional personality, backstory, and flavor text inputs
            string personalityInput = Prompt("Enter personality (or type 'generate'): ");
            character.Personality = string.Equals(personalityInput, "generate", StringComparison.OrdinalIgnoreCase)
                ? await engine.GeneratePersonalityAsync(character)
                : personalityInput;

            string backstoryInput = Prompt("Enter backstory (or type 'generate'): ");
            character.Backstory = string.Equals(backstoryInput, "generate", StringComparison.OrdinalIgnoreCase)
                ? await engine.GenerateBackstoryAsync(character)
                : backstoryInput;

            // Optional flavor text input
            string flavorInput = Prompt("Enter flavor quote (or type 'generate'): ");
            character.FlavorText = string.Equals(flavorInput, "generate", StringComparison.OrdinalIgnoreCase)
                ? await engine.GenerateFlavorQuoteAsync(character)
                : flavorInput;

            string statsChoice = Prompt("Do you want to enter stats manually or generate? (enter/generate): ");
            if (string.Equals(statsChoice, "generate", StringComparison.OrdinalIgnoreCase))
            {
                character.Stats = await engine.GenerateStatsAsync(character);
            }
            else
            {
                character.Stats = new Dictionary<string, int>();
                string[] attributes = { "STR", "DEX", "CON", "INT", "WIS", "CHA" };
                foreach (var attr in attributes)
                {
                    int value = int.TryParse(Prompt($"Enter value for {attr}: "), out var v) ? v : 10;
                    character.Stats[attr] = value;
                }
            }

            Console.WriteLine("\nReview your character:");
            Console.WriteLine($"Name: {character.Name}, Race: {character.Race}, Class: {character.Class}, Level: {character.Level}");
            Console.WriteLine($"Age: {(character.Age.HasValue ? character.Age.Value.ToString() : "Unspecified")}");
            Console.WriteLine($"Appearance: {character.Appearance}");
            Console.WriteLine($"Personality: {character.Personality}");
            Console.WriteLine($"Flavor Quote: {character.FlavorText}");
            Console.WriteLine($"Backstory: {character.Backstory}");
            Console.WriteLine("Stats:");
            foreach (var stat in character.Stats)
                Console.WriteLine($"  {stat.Key}: {stat.Value}");

            if (Prompt("Save character? (yes/no): ").Trim().ToLower() == "yes")
            {
                await repository.SaveAsync(character);
                Console.WriteLine("Character saved.");
            }
            else
            {
                Console.WriteLine("Character not saved.");
                return;
            }

            await RetrieveAndDisplayItemAsync(cosmosLoader, configuration, "Characters", character.Id);
        }

        /// <summary>
        /// Displays a prompt and reads user input from the console.
        /// </summary>
        /// <param name="message">Prompt text to display.</param>
        private static string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        /// <summary>
        /// Fetches an item from Cosmos DB and writes it to the console.
        /// </summary>
        /// <param name="cosmosLoader">Loader instance.</param>
        /// <param name="config">Application configuration.</param>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="itemKey">Key identifying the item.</param>
        private static async Task RetrieveAndDisplayItemAsync(Cosmos_Loader cosmosLoader, IConfiguration config, string containerName, string itemKey)
        {
            var containerConfig = config.GetSection($"CosmosDb:Containers:{containerName}");

            if (containerConfig.Exists())
            {
                var partitionKeyPath = containerConfig["PartitionKey"];
                var itemId = containerConfig.GetSection($"ItemIds:{itemKey}").Value;

                if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(partitionKeyPath))
                {
                    Console.WriteLine($"Missing Item ID or Partition Key for '{itemKey}' in '{containerName}'.");
                    return;
                }

                string partitionKeyValue = itemId;

                Console.WriteLine($"\nRetrieving '{itemKey}' from '{containerName}'...");
                Console.WriteLine($"Using Item ID: '{itemId}' and Partition Key: '{partitionKeyValue}'");

                var item = await cosmosLoader.GetItemByIdAsync(containerName, itemId, partitionKeyValue);

                if (item != null)
                {
                    Console.WriteLine($"Found '{itemKey}':");
                    Console.WriteLine(item);
                }
                else
                {
                    Console.WriteLine($"'{itemKey}' not found in '{containerName}'. Verify item ID and partition key.");
                }
            }
            else
            {
                Console.WriteLine($"Container '{containerName}' is not defined in appsettings.json or does NOT exist in Cosmos DB.");
            }
        }
    }
}
