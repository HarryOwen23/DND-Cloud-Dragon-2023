using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

namespace CloudDragon
{
    public class Cosmos_Loader : IDisposable
    {
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var cosmosDbSettings = configuration.GetSection("CosmosDb");
            string connectionString = cosmosDbSettings["ConnectionString"];
            string databaseId = cosmosDbSettings["DatabaseId"];
            string containerId = cosmosDbSettings["ContainerId"];
            string partitionKeyPath = cosmosDbSettings["PartitionKeyPath"];

            cosmosClient = new CosmosClient(connectionString, new CosmosClientOptions()
            {
                ApplicationRegion = Regions.EastUS2,
            });

            database = await cosmosClient.CreateDatabaseAsync(databaseId);
            container = await database.CreateContainerAsync(containerId, partitionKeyPath);
        }

        public void Dispose()
        {
            cosmosClient?.Dispose();
        }
    }
}