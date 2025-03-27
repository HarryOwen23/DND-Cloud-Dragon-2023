using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using CloudDragon;

public class Program
{
    private static async Task Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Cosmos_Loader cosmosLoader = new Cosmos_Loader(configuration);

        Console.WriteLine("\nQuerying all items from all containers...");
        await cosmosLoader.QueryAllItemsFromAllContainersAsync();
        
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

            // Use itemId as the partition key value
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


