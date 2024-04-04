using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class Trinkettype
    {
        [JsonPropertyName("Dice Number")]
        public int DiceNumber { get; set; }

        [JsonPropertyName("Trinket")]
        public string Trinket { get; set; }

        [JsonPropertyName("Dice Numbers")]
        public string DiceNumbers { get; set; }
    }

    public class TrinketCategory
    {
        [JsonPropertyName("Trinkets")]
        public List<Trinkettype> Trinkets { get; set; }
    }

    public class TrinketsData
    {
        [JsonPropertyName("Acquisitions Incorporated Trinkets")]
        public List<Trinkettype> AcquisitionsIncorporatedTrinkets { get; set; }

        [JsonPropertyName("Curse of Strahd Trinkets")]
        public List<Trinkettype> CurseOfStrahdTrinkets { get; set; }

        [JsonPropertyName("Eberron - Aerenal Trinkets")]
        public List<Trinkettype> EbberonAerenaTrinkets { get; set; }

        [JsonPropertyName("Eberron - Argonnessen Trinkets")]
        public List<Trinkettype> EberronArgonnessenTrinkets { get; set; }

        [JsonPropertyName("Eberron - Frostfell Trinkets")]
        public List<Trinkettype> EbberonFrostfellTrinkets { get; set; }

        [JsonPropertyName("Eberron - Khyber Trinkets")]
        public List<Trinkettype> EbberonKhyberTrinkets { get; set; }

        [JsonPropertyName("Eberron - Sarlona Trinkets")]
        public List<Trinkettype> EbberonSarlonaTrinkets { get; set; }

        [JsonPropertyName("Eberron - Xendrik Trinkets")]
        public List<Trinkettype> EbberonXendrikTrinkets { get; set; }

        [JsonPropertyName("Elemental Evil Trinkets")]
        public List<Trinkettype> ElementalEvilTrinkets { get; set; }

        [JsonPropertyName("Icewind Dale: Rime of the Frostmaiden Trinkets")]
        public List<Trinkettype> IcewindTrinkets { get; set; }

        [JsonPropertyName("Lost Laboratory of Kwalish Trinkets")]
        public List<Trinkettype> LostLabTrinkets { get; set; }

        [JsonPropertyName("Mordenkainen's Tome of Foes Trinkets")]
        public List<Trinkettype> MordenkainenTrinkets { get; set; }

        [JsonPropertyName("Player's Handbook Trinkets")]
        public List<Trinkettype> PlayersHandbookTrinkets { get; set; }

        [JsonPropertyName("The Wild Beyond the Witchlight Trinkets")]
        public List<Trinkettype> WildBeyondTrinkets { get; set; }

        [JsonPropertyName("Van Richten's Guide to Ravenloft Trinkets")]
        public List<Trinkettype> VanRitchenTrinkets { get; set; }
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

                if (!File.Exists(jsonFilePath))
                {
                    Console.WriteLine($"File not found: {jsonFilePath}");
                    return new TrinketsData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new TrinketsData();
                }

                // Debugging: Log JSON data to verify it's loaded correctly
                Console.WriteLine($"JSON Data: {jsonData}");

                var trinketData = JsonSerializer.Deserialize<TrinketsData>(jsonData);

                // Debugging: Log deserialized data to verify it's loaded correctly
                Console.WriteLine($"Deserialized Data: {trinketData}");

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
            string baseDirectory = Directory.GetCurrentDirectory();
            string jsonFilePathAcquisitionsIncorperated = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Acquisitions_Incorporated.json");
            string jsonFilePathCurseofStrahd = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Curse_of_Strahd.json");
            string jsonFilePathEbberonAerenal = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Ebberon_Rising_from_the_Last_War_Aerenal.json");
            string jsonFilePathEbberonArgonnessen = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Ebberon_Rising_from_the_Last_War_Argonnessen.json");
            string jsonFilePathEbberonFrostfell = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Ebberon_Rising_from_the_Last_War_Frostfell_Everice.json");
            string jsonFilePathEbberonKhyber = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Ebberon_Rising_from_the_Last_War_Khyber.json");
            string jsonFilePathEbberonSarlona = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Ebberon_Rising_from_the_Last_War_Sarlona.json");
            string jsonFilePathEbberonXendrik = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Ebberon_Rising_from_the_Last_War_Xendrik.json");
            string jsonFilePathElementalEvil = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Elemental_Evil.json");
            string jsonFilePathIcewind = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Icewind_Dale_Rime_of_the_Frostmaiden.json");
            string jsonFilePathLostLab = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Lost_Laboratory_of_Kwalish.json");
            string jsonFilePathMordenkainen = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Mordenkainens_Tome_of_Foes.json");
            string jsonFilePathPlayersHandbook = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Players_Handbook.json");
            string jsonFilePathWildBeyond = Path.Combine(baseDirectory, "Trinkets", "Trinkets_The_Wild_Beyond_The_Witchlight.json");
            string jsonFilePathVanRitchen = Path.Combine(baseDirectory, "Trinkets", "Trinkets_Van_Ritchens_Guide_to_Ravenloft.json");

            // Load the trinket data using the TrinketJsonLoader
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
            if (trinketsAcquisitionsIncorporated != null && trinketsAcquisitionsIncorporated.AcquisitionsIncorporatedTrinkets != null)
            {
                Console.WriteLine("Acquisitions Incorporated Trinkets:");
                foreach (var trinket in trinketsAcquisitionsIncorporated.AcquisitionsIncorporatedTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Curse of Strahd
            if (trinketsCurseofStrahd != null && trinketsCurseofStrahd.CurseOfStrahdTrinkets != null)
            {
                Console.WriteLine("Curse of Strahd Trinkets:");
                foreach (var trinket in trinketsCurseofStrahd.CurseOfStrahdTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Eberron - Aerenal
            if (trinketsEbberonAerenal != null && trinketsEbberonAerenal.EbberonAerenaTrinkets != null)
            {
                Console.WriteLine("Eberron - Aerenal Trinkets:");
                foreach (var trinket in trinketsEbberonAerenal.EbberonAerenaTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Eberron - Argonnessen
            if (trinketsEbberonArgonnessen != null && trinketsEbberonArgonnessen.EberronArgonnessenTrinkets != null)
            {
                Console.WriteLine("Eberron - Argonnessen Trinkets:");
                foreach (var trinket in trinketsEbberonArgonnessen.EberronArgonnessenTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Eberron - Frostfell
            if (trinketsEbberonFrostfell != null && trinketsEbberonFrostfell.EbberonFrostfellTrinkets != null)
            {
                Console.WriteLine("Eberron - Frostfell Trinkets:");
                foreach (var trinket in trinketsEbberonFrostfell.EbberonFrostfellTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Eberron - Khyber
            if (trinketsEbberonKhyber != null && trinketsEbberonKhyber.EbberonKhyberTrinkets != null)
            {
                Console.WriteLine("Eberron - Khyber Trinkets:");
                foreach (var trinket in trinketsEbberonKhyber.EbberonKhyberTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Eberron - Sarlona
            if (trinketsEbberonSarlona != null && trinketsEbberonSarlona.EbberonSarlonaTrinkets != null)
            {
                Console.WriteLine("Eberron - Sarlona Trinkets:");
                foreach (var trinket in trinketsEbberonSarlona.EbberonSarlonaTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Eberron - Xendrik
            if (trinketsEbberonXendrik != null && trinketsEbberonXendrik.EbberonXendrikTrinkets != null)
            {
                Console.WriteLine("Eberron - Xendrik Trinkets:");
                foreach (var trinket in trinketsEbberonXendrik.EbberonXendrikTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Elemental Evil
            if (trinketsElementalEvil != null && trinketsElementalEvil.ElementalEvilTrinkets != null)
            {
                Console.WriteLine("Elemental Evil Trinkets:");
                foreach (var trinket in trinketsElementalEvil.ElementalEvilTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Icewind Dale: Rime of the Frostmaiden
            if (trinketsIcewind != null && trinketsIcewind.IcewindTrinkets != null)
            {
                Console.WriteLine("Icewind Dale: Rime of the Frostmaiden Trinkets:");
                foreach (var trinket in trinketsIcewind.IcewindTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Lost Laboratory of Kwalish
            if (trinketsLostLab != null && trinketsLostLab.LostLabTrinkets != null)
            {
                Console.WriteLine("Lost Laboratory of Kwalish Trinkets:");
                foreach (var trinket in trinketsLostLab.LostLabTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumbers}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Mordenkainen's Tome of Foes
            if (trinketsMordenkainen != null && trinketsMordenkainen.MordenkainenTrinkets != null)
            {
                Console.WriteLine("Mordenkainen's Tome of Foes Trinkets:");
                foreach (var trinket in trinketsMordenkainen.MordenkainenTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Player's Handbook
            if (trinketsPlayersHandbook != null && trinketsPlayersHandbook.PlayersHandbookTrinkets != null)
            {
                Console.WriteLine("Player's Handbook Trinkets:");
                foreach (var trinket in trinketsPlayersHandbook.PlayersHandbookTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for The Wild Beyond the Witchlight
            if (trinketsWildBeyond != null && trinketsWildBeyond.WildBeyondTrinkets != null)
            {
                Console.WriteLine("The Wild Beyond the Witchlight Trinkets:");
                foreach (var trinket in trinketsWildBeyond.WildBeyondTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }

            // Display the trinket data for Van Richten's Guide to Ravenloft
            if (trinketsVanRitchen != null && trinketsVanRitchen.VanRitchenTrinkets != null)
            {
                Console.WriteLine("Van Richten's Guide to Ravenloft Trinkets:");
                foreach (var trinket in trinketsVanRitchen.VanRitchenTrinkets)
                {
                    Console.WriteLine($"- Dice Number: {trinket.DiceNumber}, Description: {trinket.Trinket}");
                }
            }
        }
    }
}
