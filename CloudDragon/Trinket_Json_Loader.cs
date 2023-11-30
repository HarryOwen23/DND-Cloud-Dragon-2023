using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Trinket
    {
        [JsonPropertyName("Dice Number")]
        public int DiceNumber { get; set; }

        [JsonPropertyName("Trinket Description")]
        public string TrinketDescription { get; set; }

        public Trinket()
        {
            TrinketDescription = string.Empty; // or any default value that makes sense
        }
    }

    public class TrinketCategory
    {
        [JsonPropertyName("Trinkets")]
        public List<Trinket> Trinkets { get; set; }

        public TrinketCategory()
        {
            Trinkets = new List<Trinket>();
        }
    }

    public class TrinketsData
    {
        [JsonPropertyName("Trinket Categories")]
        public Dictionary<string, List<Trinket>> TrinketCategories { get; set; }

        public TrinketsData()
        {
            TrinketCategories = new Dictionary<string, List<Trinket>>();
        }
    }


    internal class TrinketJsonLoader
    {
        public static TrinketsData LoadTrinketData(string jsonFilePath)
        {
            try
            {
                if (jsonFilePath == null)
                {
                    throw new ArgumentNullException(nameof(jsonFilePath), "File path cannot be null.");
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new TrinketsData();
                }

                var trinketData = JsonSerializer.Deserialize<TrinketsData>(jsonData);

                if (trinketData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default TrinketsData.");
                    return new TrinketsData();
                }

                return trinketData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class TrinketLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Trinket Data ...");

            // Define the paths to the JSON files
            string jsonFilePathAcquisitionsIncorperated = "Trinkets\\Trinkets_Acquisitions_Incorporated.json";
            string jsonFilePathCurseofStrahd = "Trinkets\\Trinkets_Curse_of_Strahd.json";
            string jsonFilePathEbberonAerenal = "Trinkets\\Trinkets_Ebberon_Rising_from_the_Last_War_Aerenal.json";
            string jsonFilePathEbberonArgonnessen = "Trinkets\\Trinkets_Ebberon_Rising_from_the_Last_War_Argonnessen.json";
            string jsonFilePathEbberonFrostfell = "Trinkets\\Trinkets_Ebberon_Rising_from_the_Last_War_Frostfell_Everice.json";
            string jsonFilePathEbberonKhyber = "Trinkets\\Trinkets_Ebberon_Rising_from_the_Last_War_Khyber.json";
            string jsonFilePathEbberonSarlona = "Trinkets\\Trinkets_Ebberon_Rising_from_the_Last_War_Sarlona.json";
            string jsonFilePathEbberonXendrik = "Trinkets\\Trinkets_Ebberon_Rising_from_the_Last_War_Xendrik.json";
            string jsonFilePathElementalEvil = "Trinkets\\Trinkets_Elemental_Evil.json";
            string jsonFilePathIcewind = "Trinkets\\Trinkets_Icewind_Dale_Rime_of_the_Frostmaiden.json";
            string jsonFilePathLostLab = "Trinkets\\Trinkets_Lost_Laboratory_of_Kwalish.json";
            string jsonFilePathMordenkainen = "Trinkets\\Trinkets_Mordenkainens_Tome_of_Foes.json";
            string jsonFilePathPlayersHandbook = "Trinkets\\Trinkets_Players_Handbook.json";
            string jsonFilePathWildBeyond = "Trinkets\\Trinkets_The_Wild_Beyond_The_Witchlight.json";
            string jsonFilePathVanRitchen = "Trinkets\\Trinkets_Van_Ritchens_Guide_to_Ravenloft.json";

            // Load the equipment data using the EquipmentJsonLoader
            var trinketsAcquisitionsIncorporated = TrinketJsonLoader.LoadTrinketData(jsonFilePathAcquisitionsIncorperated);
            var trinketsCurseofStrahd = TrinketJsonLoader.LoadTrinketData(jsonFilePathCurseofStrahd);
            var trinketsEbberonAerenal = TrinketJsonLoader.LoadTrinketData(jsonFilePathEbberonAerenal);
            var trinketsEbberonArgonnessen = TrinketJsonLoader.LoadTrinketData(jsonFilePathEbberonArgonnessen);
            var trinketsEbberonFrostfell = TrinketJsonLoader.LoadTrinketData(jsonFilePathEbberonFrostfell);
            var trinketsEbberonKhyber = TrinketJsonLoader.LoadTrinketData(jsonFilePathEbberonKhyber);
            var trinketsEbberonSarlona = TrinketJsonLoader.LoadTrinketData(jsonFilePathEbberonSarlona);
            var trinketsEbberonXendrik = TrinketJsonLoader.LoadTrinketData(jsonFilePathEbberonXendrik);
            var trinketsElementalEvil = TrinketJsonLoader.LoadTrinketData(jsonFilePathElementalEvil);
            var trinketsIcewind = TrinketJsonLoader.LoadTrinketData(jsonFilePathIcewind);
            var trinketsLostLab = TrinketJsonLoader.LoadTrinketData(jsonFilePathLostLab);
            var trinketsMordenkainen = TrinketJsonLoader.LoadTrinketData(jsonFilePathMordenkainen);
            var trinketsPlayersHandbook = TrinketJsonLoader.LoadTrinketData(jsonFilePathPlayersHandbook);
            var trinketsWildBeyond = TrinketJsonLoader.LoadTrinketData(jsonFilePathWildBeyond);
            var trinketsVanRitchen = TrinketJsonLoader.LoadTrinketData(jsonFilePathVanRitchen);

            // Display the trinket data for Acquisitions Incorporated
            if (trinketsAcquisitionsIncorporated != null &&
                trinketsAcquisitionsIncorporated.TrinketCategories != null &&
                trinketsAcquisitionsIncorporated.TrinketCategories.ContainsKey("Acquisitions_Incorporated"))
            {
                Console.WriteLine("Acquisitions Incorporated Trinkets:");

                var acquisitionsTrinkets = trinketsAcquisitionsIncorporated.TrinketCategories["Acquisitions_Incorporated"];

                if (acquisitionsTrinkets != null)
                {
                    foreach (var trinket in acquisitionsTrinkets)
                    {
                        Console.WriteLine($"- Dice Number: {trinket?.DiceNumber}, Description: {trinket?.TrinketDescription}");
                    }
                }
                else
                {
                    Console.WriteLine("Trinkets for Acquisitions Incorporated are null.");
                }
            }
            else
            {
                Console.WriteLine("Trinket data for Acquisitions Incorporated is null or the specified key is not present.");
            }


            // Display the trinket data for Curse of Strahd
            if (trinketsCurseofStrahd != null && trinketsCurseofStrahd.TrinketCategories != null && trinketsCurseofStrahd.TrinketCategories.ContainsKey("Curse_of_Strahd"))
            {
                Console.WriteLine("Curse of Strahd Trinkets:");
                foreach (var trinket in trinketsCurseofStrahd.TrinketCategories["Curse_of_Strahd"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Eberron - Aerenal
            if (trinketsEbberonAerenal != null && trinketsEbberonAerenal.TrinketCategories != null && trinketsEbberonAerenal.TrinketCategories.ContainsKey("Eberron_Rising_from_the_Last_War_Aerenal"))
            {
                Console.WriteLine("Eberron - Aerenal Trinkets:");
                foreach (var trinket in trinketsEbberonAerenal.TrinketCategories["Eberron_Rising_from_the_Last_War_Aerenal"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Eberron - Argonnessen
            if (trinketsEbberonArgonnessen != null)
            {
                Console.WriteLine("Eberron - Argonnessen Trinkets:");
                foreach (var trinket in trinketsEbberonArgonnessen.TrinketCategories["Eberron_Rising_from_the_Last_War_Argonnessen"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Eberron - Frostfell
            if (trinketsEbberonFrostfell != null)
            {
                Console.WriteLine("Eberron - Frostfell Trinkets:");
                foreach (var trinket in trinketsEbberonFrostfell.TrinketCategories["Eberron_Rising_from_the_Last_War_Frostfell"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Eberron - Khyber
            if (trinketsEbberonKhyber != null)
            {
                Console.WriteLine("Eberron - Khyber Trinkets:");
                foreach (var trinket in trinketsEbberonKhyber.TrinketCategories["Eberron_Rising_from_the_Last_War_Khyber"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Eberron - Sarlona
            if (trinketsEbberonSarlona != null)
            {
                Console.WriteLine("Eberron - Sarlona Trinkets:");
                foreach (var trinket in trinketsEbberonSarlona.TrinketCategories["Eberron_Rising_from_the_Last_War_Sarlona"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Eberron - Xendrik
            if (trinketsEbberonXendrik != null)
            {
                Console.WriteLine("Eberron - Xendrik Trinkets:");
                foreach (var trinket in trinketsEbberonXendrik.TrinketCategories["Eberron_Rising_from_the_Last_War_Xen'drik"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Elemental Evil
            if (trinketsElementalEvil != null)
            {
                Console.WriteLine("Elemental Evil Trinkets:");
                foreach (var trinket in trinketsElementalEvil.TrinketCategories["Elemental_Evil"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Icewind Dale: Rime of the Frostmaiden
            if (trinketsIcewind != null)
            {
                Console.WriteLine("Icewind Dale: Rime of the Frostmaiden Trinkets:");
                foreach (var trinket in trinketsIcewind.TrinketCategories["Icewind_Dale_Rime_of_the_Frostmaiden"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Lost Laboratory of Kwalish
            if (trinketsLostLab != null)
            {
                Console.WriteLine("Lost Laboratory of Kwalish Trinkets:");
                foreach (var trinket in trinketsLostLab.TrinketCategories["Lost_Laboratory_of_Kwalish"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Mordenkainen's Tome of Foes
            if (trinketsMordenkainen != null)
            {
                Console.WriteLine("Mordenkainen's Tome of Foes Trinkets:");
                foreach (var trinket in trinketsMordenkainen.TrinketCategories["Mordenkainen's_Tome_of_Foes"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Player's Handbook
            if (trinketsPlayersHandbook != null)
            {
                Console.WriteLine("Player's Handbook Trinkets:");
                foreach (var trinket in trinketsPlayersHandbook.TrinketCategories["Player's_Handbook"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for The Wild Beyond the Witchlight
            if (trinketsWildBeyond != null)
            {
                Console.WriteLine("The Wild Beyond the Witchlight Trinkets:");
                foreach (var trinket in trinketsWildBeyond.TrinketCategories["The_Wild_Beyond_The_Witchlight"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }

            // Display the trinket data for Van Richten's Guide to Ravenloft
            if (trinketsVanRitchen != null)
            {
                Console.WriteLine("Van Richten's Guide to Ravenloft Trinkets:");
                foreach (var trinket in trinketsVanRitchen.TrinketCategories["Van_Richten's_Guide_to_Ravenloft"])
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.TrinketDescription}");
                }
            }
        }

    }
}