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
    private Dictionary<string, string> _partitionKeys; // To hold partition key paths for each container

    // Constructor to initialize the Cosmos Client and load all containers
    public Cosmos_Loader(IConfiguration configuration)
    {
        var cosmosSettings = configuration.GetSection("CosmosDb");
        string endpointUri = cosmosSettings["EndpointURI"];
        string primaryKey = cosmosSettings["PrimaryKey"];
        string databaseId = cosmosSettings["DatabaseId"];

        _client = new CosmosClient(endpointUri, primaryKey);
        _database = _client.GetDatabase(databaseId);
        _containers = new Dictionary<string, Container>();
        _partitionKeys = new Dictionary<string, string>();

        // Load all containers from the configuration
        var containersSection = cosmosSettings.GetSection("Containers");
        foreach (var containerConfig in containersSection.GetChildren())
        {
            string containerName = containerConfig.Key;
            string partitionKeyPath = containerConfig["PartitionKey"];

            if (string.IsNullOrEmpty(partitionKeyPath))
            {
                Console.WriteLine($"Warning: PartitionKey is missing for container '{containerName}'. Skipping...");
                continue;
            }

            _containers[containerName] = _database.GetContainer(containerName);
            _partitionKeys[containerName] = partitionKeyPath;
        }
    }

    // Method to retrieve an item by ID from a specific container
    public async Task<dynamic> GetItemByIdAsync(string containerName, string id, string partitionKeyValue)
    {
        if (_containers.ContainsKey(containerName))
        {
            try
            {
                var container = _containers[containerName];
                ItemResponse<dynamic> response = await container.ReadItemAsync<dynamic>(id, new PartitionKey(partitionKeyValue));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Item with ID '{id}' not found in container '{containerName}'.");
                return null;
            }
            catch (CosmosException ex)
            {
                Console.WriteLine($"Unexpected error for container '{containerName}': {ex.Message}");
                return null;
            }
        }
        else
        {
            Console.WriteLine($"Container '{containerName}' not found.");
            return null;
        }
    }

    // Method to query all items from all containers
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
            catch (CosmosException ex)
            {
                Console.WriteLine($"Error querying container '{containerName}': {ex.Message}");
            }
        }
    }
}
