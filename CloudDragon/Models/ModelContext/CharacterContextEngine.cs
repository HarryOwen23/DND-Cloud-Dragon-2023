using System.Collections.Generic;
using CloudDragonApi.Services;
using CharacterModel = CloudDragonLib.Models.Character;
using System.Threading.Tasks;

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
        // Only generate if fields are missing
        if (string.IsNullOrWhiteSpace(character.Backstory) || string.IsNullOrWhiteSpace(character.Appearance))
        {
            string prompt = BuildFlavorfulPrompt(character);
            string generated = await _llmService.GenerateAsync(prompt);

            // For now, assign all to backstory (can later split appearance)
            character.Backstory ??= generated.Trim();
        }

        await _repository.SaveAsync(character);
        return character;
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
