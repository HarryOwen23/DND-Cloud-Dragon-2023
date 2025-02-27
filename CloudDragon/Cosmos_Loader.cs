using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Cosmos_Loader
{
    private CosmosClient _client;
    private Database _database;
    private Dictionary<string, Container> _containers; // To hold multiple containers

    public Cosmos_Loader(IConfiguration configuration)
    {
        // Try to get secrets from environment variables first
        string endpointUri = Environment.GetEnvironmentVariable("COSMOSDB_ENDPOINT")
                             ?? configuration["CosmosDb:EndpointURI"];
        string primaryKey = Environment.GetEnvironmentVariable("COSMOSDB_PRIMARYKEY")
                            ?? configuration["CosmosDb:PrimaryKey"];
        string databaseId = configuration["CosmosDb:DatabaseId"]; // Database ID is not a secret, keep it in appsettings.json

        // Ensure credentials are not null or empty
        if (string.IsNullOrEmpty(endpointUri) || string.IsNullOrEmpty(primaryKey))
        {
            throw new InvalidOperationException("Cosmos DB credentials are missing. Check environment variables.");
        }

        _client = new CosmosClient(endpointUri, primaryKey);
        _database = _client.GetDatabase(databaseId);
        _containers = new Dictionary<string, Container>();

        var containersSection = configuration.GetSection("CosmosDb:Containers");
        foreach (var containerConfig in containersSection.GetChildren())
        {
            string containerName = containerConfig.Key;
            _containers[containerName] = _database.GetContainer(containerName);
        }
    }

    public async Task<dynamic> GetItemByIdAsync(string containerName, string itemId, string partitionKeyValue)
    {
        if (_containers.ContainsKey(containerName))
        {
            try
            {
                var container = _containers[containerName];
                ItemResponse<dynamic> response = await container.ReadItemAsync<dynamic>(itemId, new PartitionKey(partitionKeyValue));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Item with ID '{itemId}' not found in container '{containerName}'.");
                return null;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Cosmos DB Error for container '{containerName}': {ex.Message}");
                return null;
            }
        }
        else
        {
            Console.WriteLine($"Container '{containerName}' does NOT exist in Cosmos DB.");
            return null;
        }
    }

    public async Task QueryAllItemsFromAllContainersAsync()
    {
        foreach (var containerEntry in _containers)
        {
            string containerName = containerEntry.Key;
            var container = containerEntry.Value;

            Console.WriteLine($"--- Querying all items from container: {containerName} ---");

            try
            {
                var query = container.GetItemQueryIterator<dynamic>("SELECT * FROM c");
                while (query.HasMoreResults)
                {
                    foreach (var item in await query.ReadNextAsync())
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Error: Container '{containerName}' NOT FOUND.");
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Unexpected error querying container '{containerName}': {ex.Message}");
            }
        }
    }
}
