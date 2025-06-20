using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDragon
{
    /// <summary>
    /// Utility wrapper for interacting with Cosmos DB containers.
    /// </summary>
    public class Cosmos_Loader
    {
        private CosmosClient _client;
        private Database _database;
        private Dictionary<string, Container> _containers; // To hold multiple containers

        /// <summary>
        /// Initializes the loader and reads configuration for Cosmos DB.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
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

        /// <summary>
        /// Creates or updates an item in the given container.
        /// </summary>
        /// <param name="containerName">Target container name.</param>
        /// <param name="item">The item to save.</param>
        /// <param name="partitionKey">Partition key for the item.</param>
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

        /// <summary>
        /// Retrieves an item by id from the specified container.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <param name="itemId">Item identifier.</param>
        /// <param name="partitionKeyValue">Partition key value.</param>
        /// <returns>The item if found; otherwise <c>null</c>.</returns>
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

        /// <summary>
        /// Writes all items from every registered container to the console.
        /// </summary>
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
}
