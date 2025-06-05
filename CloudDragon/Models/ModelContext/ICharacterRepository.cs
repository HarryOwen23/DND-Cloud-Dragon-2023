using System.Threading.Tasks;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragonApi.Services
{
    /// <summary>
    /// Provides access to storing and retrieving <see cref="CharacterModel"/> data.
    /// </summary>
    public interface ICharacterRepository
    {
        Task SaveAsync(CharacterModel character);
        Task<CloudDragonLib.Models.Character> GetAsync(string id);
        // Task SaveAsync(CloudDragonLib.Models.Character character);
    }
}
