using System.Threading.Tasks;
using Xunit;
using CloudDragon.CloudDragonApi.Functions.Character.Services;
using CloudDragonLib.Models;
using CloudDragon.Models.ModelContext;

namespace CloudDragon.Tests
{
public class CharacterContextEngineTests
{
    [Fact]
    public async Task GenerateMissingFields_populates_new_sections()
    {
        var llm = new MockLlmService();
        var repo = new InMemoryCharacterRepository();
        var engine = new CharacterContextEngine(llm, repo);
        var character = new Character { Name = "Test", Race = "Elf", Class = "Ranger" };

        await engine.GenerateMissingFieldsAsync(character);

        Assert.False(string.IsNullOrWhiteSpace(character.Goals));
        Assert.False(string.IsNullOrWhiteSpace(character.Allies));
        Assert.False(string.IsNullOrWhiteSpace(character.Secrets));
    }

    private class InMemoryCharacterRepository : ICharacterRepository
    {
        public Task<Character> GetAsync(string id) => Task.FromResult<Character>(null);
        public Task SaveAsync(Character c) => Task.CompletedTask;
    }
}
}
