using System.Threading.Tasks;

public class MockLlmService : ILlmService
{
    public Task<string> GenerateAsync(string prompt)
    {
        // Simple mock: just echoes the prompt back with a pretend "backstory"
        return Task.FromResult($"""
            [MOCK BACKSTORY GENERATED]
            This character has a mysterious past tied to ancient ruins and forgotten lore.
            Prompt used:
            {prompt}
        """);
    }
}
