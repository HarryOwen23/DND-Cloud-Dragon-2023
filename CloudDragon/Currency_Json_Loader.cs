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
        public static Currency LoadCurrencyData(string jsonFilePathGems, string jsonFilePathCoins)
        {
            try
            {
                string jsonDataGems = File.ReadAllText(jsonFilePathGems);
                string jsonDataCoins = File.ReadAllText(jsonFilePathCoins);

                var currencyDataGems= JsonSerializer.Deserialize<Currency>(jsonDataGems);
                var currencyDataCoins= JsonSerializer.Deserialize<Currency>(jsonDataCoins);

                return new Currency { Coins = currencyDataCoins.Coins, Gemstones = currencyDataGems.Gemstones};
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw e;
            }
        }
    }

    internal interface ILoader
    {
        void Load();
    }
    internal class CurrencyLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Cloud Dragon ....");

            string jsonFilePathGems = "Currency\\Currency_Gemstones.json";
            string jsonFilePathCoins = "Currency\\Currency_Coins.json";

            var currencyData = Currency_Json_Loader.LoadCurrencyData(jsonFilePathGems, jsonFilePathCoins);

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
