using System.Threading.Tasks;
using CloudDragonLib.Models;

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

        public async Task<Character> GetAsync(string id)
        {
            var raw = await _cosmosLoader.GetItemByIdAsync(ContainerName, id, id);
            return raw != null
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<Character>(raw.ToString())
                : null;
        }

        public async Task SaveAsync(Character character)
        {
            await _cosmosLoader.UpsertItemAsync(ContainerName, character, character.Id);
        }
    }
}
