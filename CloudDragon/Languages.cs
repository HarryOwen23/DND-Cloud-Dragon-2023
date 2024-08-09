using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class LanguageStuff
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Speakers")]
        public string Speakers { get; set; }
    }

    public class LanguageCategory
    {
        [JsonPropertyName("Languages")]
        public List<LanguageStuff> Languages { get; set; }
    }

    public class LanguageData
    {
        [JsonPropertyName("Language Categories")]
        public List<LanguageCategory> LanguageCategories { get; set; }
    }
    internal class LanguageJsonLoader
    {
        public static LanguageCategory LoadLanguageData(string jsonFilePath)
        {
            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null or empty.");
            }

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return new LanguageCategory();
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                return JsonSerializer.Deserialize<LanguageCategory>(jsonData) ?? new LanguageCategory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON file: {ex.Message}");
                throw;
            }
        }
    }

    internal class LanguageLoader : ILoader
    {
        private const string JsonFilePathLanguage = "Language\\Language.json";

        public void Load()
        {
            Console.WriteLine("Loading Language Data ...");
            var language = LanguageJsonLoader.LoadLanguageData(JsonFilePathLanguage);

            if (language?.Languages != null)
            {
                Console.WriteLine("Language's:");
                foreach (var lang in language.Languages)
                {
                    Console.WriteLine($"- Name: {lang.Name}, Description: {lang.Description}, Speakers: {lang.Speakers} ");
                }

            }
        }
    }
}
