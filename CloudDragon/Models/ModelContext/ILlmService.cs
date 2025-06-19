using System.Threading.Tasks;

namespace CloudDragon.Models.ModelContext
{
    public interface ILlmService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
