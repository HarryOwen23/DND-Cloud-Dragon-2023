using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

public class Program
{
    private static async Task Main(string[] args)
    {
        // Set up configuration to read from appsettings.json
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Initialize Cosmos Loader with configuration
        Cosmos_Loader cosmosLoader = new Cosmos_Loader(configuration);

        // Retrieve partition keys and item IDs from configuration
        var partitionKeys = configuration.GetSection("CosmosDb:PartitionKeys").Get<PartitionKeysConfig>();
        var itemIds = configuration.GetSection("CosmosDb:ItemIds").Get<ItemIdsConfig>();

        if (partitionKeys == null)
        {
            Console.WriteLine("PartitionKeysConfig is null");
        }
        if (itemIds == null)
        {
            Console.WriteLine("ItemIdsConfig is null");
        }

        // Retrieve and display items based on configuration
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Fighter Class", itemIds.ClassFighter, partitionKeys.ClassFighter);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Champion Subclass", itemIds.Subclasses.Champion, partitionKeys.Subclasses.Champion);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Battle Master Subclass", itemIds.Subclasses.BattleMaster, partitionKeys.Subclasses.BattleMaster);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Eldritch Knight Subclass", itemIds.Subclasses.EldritchKnight, partitionKeys.Subclasses.EldritchKnight);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Arcane Archer Subclass", itemIds.Subclasses.ArcaneArcher, partitionKeys.Subclasses.ArcaneArcher);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Banneret Subclass", itemIds.Subclasses.Banneret, partitionKeys.Subclasses.Banneret);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Cavalier Subclass", itemIds.Subclasses.Cavalier, partitionKeys.Subclasses.Cavalier);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Echo Knight Subclass", itemIds.Subclasses.EchoKnight, partitionKeys.Subclasses.EchoKnight);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Psi Warrior Subclass", itemIds.Subclasses.PsiWarrior, partitionKeys.Subclasses.PsiWarrior);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Rune Knight Subclass", itemIds.Subclasses.RuneKnight, partitionKeys.Subclasses.RuneKnight);
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Samurai Subclass", itemIds.Subclasses.Samurai, partitionKeys.Subclasses.Samurai);

        // Debugging: Query and list all items
        Console.WriteLine("\nQuerying all items in the container (for debugging purposes)...");
        await cosmosLoader.QueryAllItemsAsync();
    }

    private static async Task RetrieveAndDisplayItemAsync(Cosmos_Loader cosmosLoader, string itemName, string itemId, string partitionKey)
    {
        Console.WriteLine($"\nRetrieving {itemName}...");
        var item = await cosmosLoader.GetItemByIdAsync(itemId, partitionKey);
        if (item != null)
        {
            Console.WriteLine($"{itemName}:");
            Console.WriteLine(item);
        }
        else
        {
            Console.WriteLine($"{itemName} not found.");
        }
    }
}

// Configuration classes for mapping PartitionKeys and ItemIds sections in appsettings.json
public class PartitionKeysConfig
{
    public string ClassFighter { get; set; }

    // public string ClassBarbarian { get; set; }
    public SubclassesConfig Subclasses { get; set; }
}

public class ItemIdsConfig
{
    public string ClassFighter { get; set; }
    // public string ClassBarbarian { get; set; }

    public SubclassesConfig Subclasses { get; set; }
}

public class SubclassesConfig
{
    public string Champion { get; set; }
    public string BattleMaster { get; set; }
    public string EldritchKnight { get; set; }
    public string ArcaneArcher { get; set; }
    public string Banneret { get; set; }
    public string Cavalier { get; set; }
    public string EchoKnight { get; set; }
    public string PsiWarrior { get; set; }
    public string RuneKnight { get; set; }
    public string Samurai { get; set; }

    // Barbarian Subclasses 

    // Bard Subclasses

    // Rogue Subclasses

    // Wizard Subclasses

    // Cleric Subclasses

    // Sorcerer Subclasses

    // Warlock Subclasses

    // Druid Subclasses

    // Ranger Subclasses

    // Paladin Subclasses


}