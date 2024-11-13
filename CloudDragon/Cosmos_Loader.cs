using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

public class Cosmos_Loader
{
    private CosmosClient _client;
    private Database _database;
    private Container _container;

    // Constructor to initialize the Cosmos Client using configuration
    public Cosmos_Loader(IConfiguration configuration)
    {
        var cosmosSettings = configuration.GetSection("CosmosDb");
        string endpointUri = cosmosSettings["EndpointURI"];
        string primaryKey = cosmosSettings["PrimaryKey"];
        string databaseId = cosmosSettings["DatabaseId"];
        string containerId = cosmosSettings["ContainerId"];

        _client = new CosmosClient(endpointUri, primaryKey);
        _database = _client.GetDatabase(databaseId);
        _container = _database.GetContainer(containerId);
    }

    // Method to retrieve an item by ID
    public async Task<dynamic> GetItemByIdAsync(string id, string partitionKeyValue)
    {
        try
        {
            ItemResponse<dynamic> response = await _container.ReadItemAsync<dynamic>(id, new PartitionKey(partitionKeyValue));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine($"Item with ID '{id}' not found.");
            return null;
        }
    }

    // Method to query all items (for debugging purposes)
    public async Task QueryAllItemsAsync()
    {
        var query = _container.GetItemQueryIterator<dynamic>("SELECT * FROM c");
        while (query.HasMoreResults)
        {
            foreach (var item in await query.ReadNextAsync())
            {
                Console.WriteLine(item);
            }
        }
    }
}
