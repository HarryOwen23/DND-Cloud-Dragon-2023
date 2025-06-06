using System.Threading.Tasks;

namespace CloudDragonApi.Models
{
    public interface ICombatSessionRepository
    {
        Task SaveAsync(CombatSession session);
        Task<CombatSession?> GetAsync(string id);
    }
}
