using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;

/// <summary>
/// Represents a playable character class such as Fighter or Wizard.
/// </summary>
public class DNDClasses
{
    /// <summary>
    /// Name of the character class.
    /// </summary>
    [JsonProperty("Class Name")]
    public string ClassName { get; set; }

    /// <summary>
    /// Textual description summarizing the class features.
    /// </summary>
    private string _classDescription;

    /// <summary>
    /// Textual description summarizing the class features.
    /// </summary>
    [JsonProperty("Class Description")]
    public string ClassDescription
    {
        get => _classDescription;
        set
        {
            Console.WriteLine($"Set description for {ClassName}");
            _classDescription = value;
        }
    }

    /// <summary>
    /// Creates a new <see cref="DNDClasses"/> with the provided name.
    /// </summary>
    /// <param name="className">Name of the class.</param>
    public DNDClasses(string className)
    {
        ClassName = className;
        Console.WriteLine($"Created class {className}");
    }

    /// <summary>
    /// Initializes a new instance of <see cref="DNDClasses"/> without setting properties.
    /// Useful when classes are deserialized from JSON.
    /// </summary>
    public DNDClasses()
    {
        Console.WriteLine("Created empty class");
    }
}
