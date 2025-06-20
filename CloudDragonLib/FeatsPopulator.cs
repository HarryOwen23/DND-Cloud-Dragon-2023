using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CloudDragonLib.Models;

namespace CloudDragonLib
{
    /// <summary>
    /// Abstraction for loading feats from an external file.
    /// </summary>
    public interface IFeatsPopulator
    {
        /// <summary>
        /// Reads and parses feats from the specified JSON file.
        /// </summary>
        /// <param name="fileName">Path to the file containing feat data.</param>
        /// <returns>List of parsed feats.</returns>
        Task<List<Feats>> Populate(string fileName);
    }

    /// <summary>
    /// Reads feats from a JSON file and converts them into <see cref="Feats"/> objects.
    /// </summary>
    public class FeatsPopulator : IFeatsPopulator
    {
        public async Task<List<Feats>> Populate(string fileName)
        {
            Console.WriteLine($"Populating feats from {fileName}");
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name must be provided", nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException("Feats file not found", fileName);

            var json = await File.ReadAllTextAsync(fileName);
            Console.WriteLine($"Loaded feats JSON, length {json.Length}");
            var jObject = JObject.Parse(json);
            var featsArray = jObject["Feats"] as JArray;
            var result = new List<Feats>();

            if (featsArray != null)
            {
                foreach (var featToken in featsArray)
                {
                    var name = featToken["Feat Name"]?.ToString();
                    var description = featToken["Description"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
                    {
                        result.Add(new Feats(name, description));
                        Console.WriteLine($"Loaded feat {name}");
                    }
                }
            }

            Console.WriteLine($"Parsed {result.Count} feats");
            return result;
        }
    }
}
