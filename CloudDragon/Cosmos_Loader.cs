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
        var cosmosSettings = configuration.GetSection("CosmosDb");
        string endpointUri = cosmosSettings["EndpointURI"];
        string primaryKey = cosmosSettings["PrimaryKey"];
        string databaseId = cosmosSettings["DatabaseId"];

        string envEndpoint = configuration["COSMOSDB_ENDPOINT"];
        string envPrimaryKey = configuration["COSMOSDB_PRIMARY_KEY"];

        if (!string.IsNullOrEmpty(envEndpoint))
        {
            endpointUri = envEndpoint;
        }

        if (!string.IsNullOrEmpty(envPrimaryKey))
        {
            primaryKey = envPrimaryKey;
        }

        _client = new CosmosClient(endpointUri, primaryKey);
        _database = _client.GetDatabase(databaseId);
        _containers = new Dictionary<string, Container>();

        var containersSection = cosmosSettings.GetSection("Containers");
        foreach (var containerConfig in containersSection.GetChildren())
        {
            string containerName = containerConfig.Key;
            _containers[containerName] = _database.GetContainer(containerName);
        }
    }

    public async Task UpsertItemAsync(string containerName, object item, string partitionKey)
    {
        if (_containers.ContainsKey(containerName))
        {
            var container = _containers[containerName];
            await container.UpsertItemAsync(item, new PartitionKey(partitionKey));
        }
        else
        {
            Console.WriteLine($"Container '{containerName}' not found in registered containers.");
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
                Console.WriteLine($"Partition Key Used: '{partitionKeyValue}'");
                Console.WriteLine($"Error Details: {ex.Message}");
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
                Console.WriteLine($"Suggestion: Verify the container name in Cosmos DB Data Explorer.");
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Unexpected error querying container '{containerName}': {ex.Message}");
            }
        }
    }
}
