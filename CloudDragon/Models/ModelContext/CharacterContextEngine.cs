using System.Collections.Generic;
using System.Threading.Tasks;
using CloudDragonApi.Services;
using CharacterModel = CloudDragonLib.Models.Character;

public class CharacterContextEngine
{
    private readonly ILlmService _llmService;
    private readonly ICharacterRepository _repository;
    private readonly McpPromptBuilder _promptBuilder = new();

    public CharacterContextEngine(ILlmService llmService, ICharacterRepository repository)
    {
        _llmService = llmService;
        _repository = repository;
    }

    public async Task<CharacterModel> BuildAndStoreCharacterAsync(CharacterModel character)
    {
        await GenerateMissingFieldsAsync(character);
        await _repository.SaveAsync(character);
        return character;
    }

    public async Task<CharacterModel> GenerateMissingFieldsAsync(CharacterModel character)
    {
        if (string.IsNullOrWhiteSpace(character.Appearance))
            character.Appearance = await GenerateAppearanceAsync(character);

        if (string.IsNullOrWhiteSpace(character.Personality))
            character.Personality = await GeneratePersonalityAsync(character);

        if (string.IsNullOrWhiteSpace(character.Backstory))
            character.Backstory = await GenerateBackstoryAsync(character);

        if (string.IsNullOrWhiteSpace(character.FlavorText))
            character.FlavorText = await GenerateFlavorQuoteAsync(character);

        return character;
    }

    public async Task<string> GenerateAppearanceAsync(CharacterModel character)
    {
        string prompt = _promptBuilder.BuildPrompt(character) +
            "\nWrite a vivid physical description of this character. Focus on unique traits, attire, and demeanor.";

        return await _llmService.GenerateAsync(prompt);
    }

    public async Task<string> GeneratePersonalityAsync(CharacterModel character)
    {
        string prompt = _promptBuilder.BuildPrompt(character) +
            "\nDescribe this character's personality. Include quirks, virtues, flaws, and behavioral tendencies.";

        return await _llmService.GenerateAsync(prompt);
    }

    public async Task<string> GenerateBackstoryAsync(CharacterModel character)
    {
        string prompt = BuildFlavorfulPrompt(character);
        return await _llmService.GenerateAsync(prompt);
    }

    public async Task<string> GenerateFlavorQuoteAsync(CharacterModel character)
    {
        string prompt = _promptBuilder.BuildPrompt(character) +
            "\nWrite a short, iconic quote that reflects this character's essence — something they might say in a dramatic moment.";

        return await _llmService.GenerateAsync(prompt);
    }

    public async Task<Dictionary<string, int>> GenerateStatsAsync(CharacterModel character)
    {
        var stats = new Dictionary<string, int>
        {
            ["STR"] = RollStat(),
            ["DEX"] = RollStat(),
            ["CON"] = RollStat(),
            ["INT"] = RollStat(),
            ["WIS"] = RollStat(),
            ["CHA"] = RollStat()
        };

        return await Task.FromResult(stats); // keep async-compatible signature

        int RollStat() => new Random().Next(8, 16); // adjust range as desired
    }

    private string BuildFlavorfulPrompt(CharacterModel character)
    {
        string context = _promptBuilder.BuildPrompt(character);
        return
            "You are a storyteller and world-builder for a Dungeons & Dragons campaign.\n" +
            "Using the context below, write a compelling and flavorful backstory for the character.\n" +
            "The story should highlight personality traits, moral values, and past events that shaped them.\n" +
            "Use rich, engaging prose — the kind a DM could read aloud to set the scene.\n\n" +
            context;
    }
}
