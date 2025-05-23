using System;
using System.Linq;
using System.Reflection;
using System.Text;

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
}
