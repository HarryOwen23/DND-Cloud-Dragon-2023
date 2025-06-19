using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CloudDragon.CloudDragonApi.Utils;

namespace CloudDragon.CloudDragonApi.Functions.Combat
{
    /// <summary>
    /// Simple file-based repository for storing combat sessions locally.
    /// </summary>
    public class FileCombatSessionRepository : ICombatSessionRepository
    {
        private readonly string _directory;

        /// <summary>
        /// Creates a new repository using the specified directory path.
        /// </summary>
        /// <param name="directory">Directory where session files are stored.</param>
        public FileCombatSessionRepository(string directory)
        {
            _directory = directory;
            Directory.CreateDirectory(_directory);
        }

        /// <summary>
        /// Saves the provided session to disk as JSON.
        /// </summary>
        /// <param name="session">Session to persist.</param>
        public Task SaveAsync(CombatSession session)
        {
            string path = Path.Combine(_directory, $"{session.Id}.json");
            string json = JsonConvert.SerializeObject(session, Formatting.Indented);
            File.WriteAllText(path, json);
            DebugLogger.Log($"Saved combat session {session.Id} to {path}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves a session from disk by its identifier.
        /// </summary>
        /// <param name="id">Session id to load.</param>
        /// <returns>The loaded session or <c>null</c> if not found.</returns>
        public Task<CombatSession?> GetAsync(string id)
        {
            string path = Path.Combine(_directory, $"{id}.json");
            if (!File.Exists(path))
            {
                DebugLogger.Log($"Session file not found: {path}");
                return Task.FromResult<CombatSession?>(null);
            }
            string json = File.ReadAllText(path);
            var session = JsonConvert.DeserializeObject<CombatSession>(json);
            DebugLogger.Log($"Loaded combat session {id} from {path}");
            return Task.FromResult(session);
        }
    }
}
