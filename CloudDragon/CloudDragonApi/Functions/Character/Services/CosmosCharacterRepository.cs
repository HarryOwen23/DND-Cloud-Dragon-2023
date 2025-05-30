using System.Threading.Tasks;
using CharacterModel = CloudDragonLib.Models.Character;


namespace CloudDragonApi.Services
{
    public class CosmosCharacterRepository : ICharacterRepository
    {
        private readonly Cosmos_Loader _cosmosLoader;
        private const string ContainerName = "Characters";

        public CosmosCharacterRepository(Cosmos_Loader cosmosLoader)
        {
            _cosmosLoader = cosmosLoader;
        }

        public async Task<CharacterModel> GetAsync(string id)
        {
            var raw = await _cosmosLoader.GetItemByIdAsync(ContainerName, id, id);
            return raw != null
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterModel>(raw.ToString())
                : null;
        }

        public async Task SaveAsync(CharacterModel  character)
        {
            await _cosmosLoader.UpsertItemAsync(ContainerName, character, character.Id);
        }
    }
}
