using System.Collections.Generic;
using System.Threading.Tasks;
using CharacterModel = CloudDragonLib.Models.Character;
using CloudDragonApi.Utils;

namespace CloudDragonApi.Services
{
    /// <summary>
    /// Generates narrative details and statistics for characters using an underlying
    /// language model service and persists them via an <see cref="ICharacterRepository"/>.
    /// </summary>
    public class CharacterContextEngine
    {
        private readonly ILlmService _llmService;
        private readonly ICharacterRepository _repository;
        private readonly McpPromptBuilder _promptBuilder = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterContextEngine"/> class.
        /// </summary>
        /// <param name="llmService">Service used to generate text using an LLM.</param>
        /// <param name="repository">Repository used for persisting characters.</param>
        public CharacterContextEngine(ILlmService llmService, ICharacterRepository repository)
        {
            _llmService = llmService;
            _repository = repository;
        }

        /// <summary>
        /// Generates any missing fields for the supplied character and saves it using
        /// the configured repository.
        /// </summary>
        /// <param name="character">Character instance to populate and store.</param>
        /// <returns>The populated character.</returns>
        public async Task<CharacterModel> BuildAndStoreCharacterAsync(CharacterModel character)
        {
            DebugLogger.Log($"Building character '{character.Name}'");
            await GenerateMissingFieldsAsync(character);
            await _repository.SaveAsync(character);
            DebugLogger.Log($"Character '{character.Name}' saved");
            return character;
        }

        /// <summary>
        /// Populates any unset narrative fields on the character by calling the language model.
        /// </summary>
        /// <param name="character">Character instance to enrich.</param>
        /// <returns>The enriched character.</returns>
        public async Task<CharacterModel> GenerateMissingFieldsAsync(CharacterModel character)
        {
            DebugLogger.Log($"Generating missing fields for '{character.Name}'");
            if (string.IsNullOrWhiteSpace(character.Appearance))
                character.Appearance = await GenerateAppearanceAsync(character);

            if (string.IsNullOrWhiteSpace(character.Personality))
                character.Personality = await GeneratePersonalityAsync(character);

           if (string.IsNullOrWhiteSpace(character.Backstory))
               character.Backstory = await GenerateBackstoryAsync(character);

            if (string.IsNullOrWhiteSpace(character.Goals))
                character.Goals = await GenerateGoalsAsync(character);

            if (string.IsNullOrWhiteSpace(character.Allies))
                character.Allies = await GenerateAlliesAsync(character);

            if (string.IsNullOrWhiteSpace(character.Secrets))
                character.Secrets = await GenerateSecretsAsync(character);

            if (string.IsNullOrWhiteSpace(character.FlavorText))
                character.FlavorText = await GenerateFlavorQuoteAsync(character);

            return character;
        }

        /// <summary>
        /// Generates a physical description for the given character.
        /// </summary>
        /// <param name="character">Character to describe.</param>
        /// <returns>Description text.</returns>
        public async Task<string> GenerateAppearanceAsync(CharacterModel character)
        {
            string prompt = _promptBuilder.BuildPrompt(character) +
                "\nWrite a vivid physical description of this character. Focus on unique traits, attire, and demeanor.";

            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates personality details for the specified character.
        /// </summary>
        /// <param name="character">Character to describe.</param>
        /// <returns>Personality description.</returns>
        public async Task<string> GeneratePersonalityAsync(CharacterModel character)
        {
            string prompt = _promptBuilder.BuildPrompt(character) +
                "\nDescribe this character's personality. Include quirks, virtues, flaws, and behavioral tendencies.";

            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates a flavorful backstory for the character.
        /// </summary>
        /// <param name="character">Character to create a backstory for.</param>
        /// <returns>Backstory text.</returns>
        public async Task<string> GenerateBackstoryAsync(CharacterModel character)
        {
            string prompt = BuildFlavorfulPrompt(character);
            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates short term or long term goals for the character.
        /// </summary>
        /// <param name="character">Character to generate goals for.</param>
        /// <returns>Goals text.</returns>
        public async Task<string> GenerateGoalsAsync(CharacterModel character)
        {
            string prompt = _promptBuilder.BuildPrompt(character) +
                "\nList this character's primary goals or ambitions in a short paragraph.";
            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates a brief description of allies connected to the character.
        /// </summary>
        /// <param name="character">Character whose allies are described.</param>
        /// <returns>Allies description.</returns>
        public async Task<string> GenerateAlliesAsync(CharacterModel character)
        {
            string prompt = _promptBuilder.BuildPrompt(character) +
                "\nBriefly describe any notable allies or organizations connected to this character.";
            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates a secret that the character keeps hidden.
        /// </summary>
        /// <param name="character">Character for which to generate the secret.</param>
        /// <returns>Secret text.</returns>
        public async Task<string> GenerateSecretsAsync(CharacterModel character)
        {
            string prompt = _promptBuilder.BuildPrompt(character) +
                "\nReveal a secret this character keeps hidden from most people.";
            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates a short flavor quote for the character.
        /// </summary>
        /// <param name="character">Character to generate a quote for.</param>
        /// <returns>Flavor quote.</returns>
        public async Task<string> GenerateFlavorQuoteAsync(CharacterModel character)
        {
            string prompt = _promptBuilder.BuildPrompt(character) +
                "\nWrite a short, iconic quote that reflects this character's essence — something they might say in a dramatic moment.";

            return await _llmService.GenerateAsync(prompt);
        }

        /// <summary>
        /// Generates a random set of ability scores for the character.
        /// </summary>
        /// <param name="character">Character to generate stats for.</param>
        /// <returns>Dictionary of ability scores.</returns>
        public async Task<Dictionary<string, int>> GenerateStatsAsync(CharacterModel character)
        {
            int RollStat() => Random.Shared.Next(8, 16); // adjust range as desired

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
        }

        /// <summary>
        /// Constructs a prompt used to request a rich backstory from the language model.
        /// </summary>
        /// <param name="character">Character providing context.</param>
        /// <returns>Prompt string.</returns>
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
}
