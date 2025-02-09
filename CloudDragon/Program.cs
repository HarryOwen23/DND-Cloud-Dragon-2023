using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using CloudDragon;

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

        // Debugging: Query and list all items from all containers
        Console.WriteLine("\nQuerying all items in all containers (for debugging purposes)...");
        await cosmosLoader.QueryAllItemsFromAllContainersAsync();

        // Example: Retrieve and display specific items from different containers
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Heavy Armor", "Armor", "Heavy_Armor");
        await RetrieveAndDisplayItemAsync(cosmosLoader, "Acolyte Background", "Backgrounds", "acolyte_background");
    }

    // Method to retrieve and display an item from a specific container
    private static async Task RetrieveAndDisplayItemAsync(Cosmos_Loader cosmosLoader, string itemName, string containerName, string itemId)
    {
        Console.WriteLine($"\nRetrieving {itemName} from container '{containerName}'...");

        // Assuming the partition key is the same as the itemId for simplicity
        var item = await cosmosLoader.GetItemByIdAsync(containerName, itemId, itemId);

        if (item != null)
        {
            Console.WriteLine($"{itemName}:");
            Console.WriteLine(item);
        }
        else
        {
            Console.WriteLine($"{itemName} not found in '{containerName}'.");
        }
    }
}
