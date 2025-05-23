using System.Threading.Tasks;

namespace CloudDragonApi.Services
{
    public interface ICharacterRepository
    {
        /// <summary>
        /// Retrieves a character by its ID.
        // This interface defines the contract for a character repository, which is responsible for
        // retrieving and saving character data. The `GetAsync` method retrieves a character by its ID,
        Task<CloudDragonLib.Models.Character> GetAsync(string id);
        Task SaveAsync(CloudDragonLib.Models.Character character);
    }
}