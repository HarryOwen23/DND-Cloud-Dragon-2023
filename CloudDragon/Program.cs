using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using CloudDragon;
using CloudDragonApi.Services;
using DotNetEnv;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using CharacterModel = CloudDragonLib.Models.Character;

public partial class Program
{
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
            Name = "Garrick Husty",
            Race = "Human",
            Class = "Fighter",
            Level = 1,
            Appearance = "Plain-looking teen with untamed brown hair, hazel eyes, and bandages completely wrapped around his right arm to hide its appearance",
            Personality = "A young, shy swordsman haunted by nightmares of a man in armor",
            Backstory = @"Garrick Husty was raised in the stone-cold halls of an orphanage..."
        };

        Console.WriteLine("Generating backstory for Garrick Husty...\n");

        var result = await engine.BuildAndStoreCharacterAsync(character);

        Console.WriteLine("Character Created:");
        Console.WriteLine($"ID: {result.Id}");
        Console.WriteLine($"Name: {result.Name}");
        Console.WriteLine($"Race: {result.Race}");
        Console.WriteLine($"Class: {result.Class}");
        Console.WriteLine($"Level: {result.Level}");
        Console.WriteLine($"Appearance: {result.Appearance}");
        Console.WriteLine($"Personality: {result.Personality}");
        Console.WriteLine($"\n Backstory:\n{result.Backstory}");

        await RetrieveAndDisplayItemAsync(cosmosLoader, configuration, "Characters", result.Id);
    }

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
