using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Currency
    {
        [JsonPropertyName("Currency_Coins")]
        public List<Coin> Coins { get; set; }

        [JsonPropertyName("Gemstones")]
        public List<Gemstone> Gemstones { get; set; }
    }

    public class Coin
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }

    public class Gemstone
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    internal class Currency_Json_Loader
    {
        public static Currency LoadCurrencyData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                var currencyData = JsonSerializer.Deserialize<Currency>(jsonData);

                return currencyData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                return null;
            }
        }
    }
    internal class CurrencyLoader
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading Cloud Dragon ....");

            string jsonFilePath = "path_to_your_combined_currency_data.json";

            var currencyData = Currency_Json_Loader.LoadCurrencyData(jsonFilePath);

            if (currencyData != null)
            {
                // Access and display coin data
                Console.WriteLine("Coins:");
                foreach (var coin in currencyData.Coins)
                {
                    Console.WriteLine($"- {coin.Name}: {coin.Value}");
                }

                // Access and display gemstone data
                Console.WriteLine("Gemstones:");
                foreach (var gemstone in currencyData.Gemstones)
                {
                    Console.WriteLine($"- {gemstone.Name}: {gemstone.Cost}, {gemstone.Description}");
                }
            }
        }
    }
}
