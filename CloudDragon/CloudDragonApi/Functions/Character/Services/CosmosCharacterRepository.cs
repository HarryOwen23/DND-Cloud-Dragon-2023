using System.Threading.Tasks;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragon.Models.ModelContext;


namespace CloudDragon.CloudDragonApi.Functions.Character.Services
{
    /// <summary>
    /// Implementation of <see cref="ICharacterRepository"/> using Azure Cosmos DB.
    /// </summary>
    public class CosmosCharacterRepository : ICharacterRepository
    {
        private readonly Cosmos_Loader _cosmosLoader;
        private const string ContainerName = "Characters";

        /// <summary>
        /// Initializes a new instance of the repository with the provided loader.
        /// </summary>
        /// <param name="cosmosLoader">Helper used to access Cosmos DB.</param>
        public CosmosCharacterRepository(Cosmos_Loader cosmosLoader)
        {
            _cosmosLoader = cosmosLoader;
        }

        /// <inheritdoc />
        public async Task<CharacterModel> GetAsync(string id)
        {
            var raw = await _cosmosLoader.GetItemByIdAsync(ContainerName, id, id);
            return raw != null
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterModel>(raw.ToString())
                : null;
        }

        /// <inheritdoc />
        public async Task SaveAsync(CharacterModel  character)
        {
            await _cosmosLoader.UpsertItemAsync(ContainerName, character, character.Id);
        }
    }
}
