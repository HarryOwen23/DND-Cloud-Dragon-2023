public interface ILlmService
{
    Task<string> GenerateAsync(string prompt);
}
