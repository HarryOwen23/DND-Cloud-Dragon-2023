using System.Threading.Tasks;

namespace CloudDragonApi.Services
{
    public interface ILlmService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
