using System.Threading.Tasks;

public class MockLlmService : ILlmService
{
    public Task<string> GenerateAsync(string prompt)
    {
        // Simple mock: just echoes the prompt back with a pretend "backstory".
        var response =
            "[MOCK BACKSTORY GENERATED]\n" +
            "This character has a mysterious past tied to ancient ruins and forgotten lore.\n" +
            "Prompt used:\n" +
            prompt;

        return Task.FromResult(response);
    }
}