using System.Threading.Tasks;

namespace CloudDragonApi.Models
{
    /// <summary>
    /// Abstraction for storing and retrieving combat sessions.
    /// Implementations may persist sessions to any backing store such as
    /// Cosmos DB or the file system.
    /// </summary>
    public interface ICombatSessionRepository
    {
        /// <summary>
        /// Persists the given session to the underlying storage mechanism.
        /// </summary>
        /// <param name="session">Session to save.</param>
        Task SaveAsync(CombatSession session);

        /// <summary>
        /// Retrieves a previously saved session by its identifier.
        /// </summary>
        /// <param name="id">Unique session id.</param>
        /// <returns>The session if found; otherwise <c>null</c>.</returns>
        Task<CombatSession?> GetAsync(string id);
    }
}
