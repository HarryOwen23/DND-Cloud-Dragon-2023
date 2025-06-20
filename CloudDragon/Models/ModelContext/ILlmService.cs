using System.Threading.Tasks;

namespace CloudDragon.Models.ModelContext
{
    /// <summary>
    /// Abstraction for a language model service used to generate text.
    /// </summary>
    public interface ILlmService
    {
        /// <summary>
        /// Generates text based on the supplied prompt.
        /// </summary>
        /// <param name="prompt">Prompt describing the desired output.</param>
        /// <returns>Generated text.</returns>
        Task<string> GenerateAsync(string prompt);
    }
}
