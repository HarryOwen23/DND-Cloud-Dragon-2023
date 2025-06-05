using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudDragonApi.Models
{
    /// <summary>
    /// Simple file-based repository for storing combat sessions locally.
    /// </summary>
    public class FileCombatSessionRepository : ICombatSessionRepository
    {
        private readonly string _directory;

        public FileCombatSessionRepository(string directory)
        {
            _directory = directory;
            Directory.CreateDirectory(_directory);
        }

        public Task SaveAsync(CombatSession session)
        {
            string path = Path.Combine(_directory, $"{session.Id}.json");
            string json = JsonConvert.SerializeObject(session, Formatting.Indented);
            File.WriteAllText(path, json);
            return Task.CompletedTask;
        }

        public Task<CombatSession?> GetAsync(string id)
        {
            string path = Path.Combine(_directory, $"{id}.json");
            if (!File.Exists(path))
                return Task.FromResult<CombatSession?>(null);
            string json = File.ReadAllText(path);
            var session = JsonConvert.DeserializeObject<CombatSession>(json);
            return Task.FromResult(session);
        }
    }
}
