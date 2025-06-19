using System.Threading.Tasks;
using CharacterModel = CloudDragonLib.Models.Character;

namespace CloudDragon.Models.ModelContext
{
    /// <summary>
    /// Provides access to storing and retrieving <see cref="CharacterModel"/> data.
    /// </summary>
    public interface ICharacterRepository
    {
        /// <summary>
        /// Persists the supplied character to the underlying data store.
        /// </summary>
        /// <param name="character">Character instance to save.</param>
        Task SaveAsync(CharacterModel character);

        /// <summary>
        /// Retrieves a character by identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the character.</param>
        /// <returns>The stored character or <c>null</c> if not found.</returns>
        Task<CharacterModel> GetAsync(string id);
    }
}
