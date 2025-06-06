using System;
using System.Linq;
using System.Reflection;
using System.Text;
using CloudDragonLib.Models;

namespace CloudDragonApi.Services
{
    public class McpPromptBuilder
    {
        public string BuildPrompt(object model)
        {
            var type = model.GetType();

            if (!Attribute.IsDefined(type, typeof(ModelContextAttribute)))
                throw new InvalidOperationException($"Type {type.Name} is not marked with [ModelContext].");

            var sb = new StringBuilder();
            sb.AppendLine("Build a D&D character based on the following details:");

            var props = type.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(ModelFieldAttribute)));

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<ModelFieldAttribute>();
                var value = prop.GetValue(model)?.ToString() ?? "(unspecified)";
                sb.AppendLine($"{attr.Description}: {value}");
            }

            return sb.ToString();
        }

        public string BuildFlavorQuotePrompt(Character character)
        {
            return
                $@"Write a short, poetic flavor quote for a Dungeons & Dragons character card.
                The quote should reflect the character’s essence and fantasy theme.

                Name: {character.Name}
                Race: {character.Race}
                Class: {character.Class}
                Personality: {character.Personality}

            Limit to 30 words. Style it like fantasy card flavor text — poetic, cryptic, or heroic, as in Magic: The Gathering or Cardfight!! Vanguard.";
    }

    }
}
