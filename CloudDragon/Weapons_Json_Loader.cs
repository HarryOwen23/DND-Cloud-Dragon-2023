using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudDragon
{
    public class MartialMeleeWeapon
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string? Cost { get; set; } 

        [JsonPropertyName("Damage")]
        public string Damage { get; set; } 

        [JsonPropertyName("Weight")]
        public string Weight { get; set; } 

        [JsonPropertyName("Properties")]
        public string Properties { get; set; }
    }

    public class MartialRangedWeapon
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }

        [JsonPropertyName("Damage")]
        public string Damage { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }

        [JsonPropertyName("Properties")]
        public string Properties { get; set; }
    }


    public class SimpleMeleeWeapon
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }

        [JsonPropertyName("Damage")]
        public string Damage { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }

        [JsonPropertyName("Properties")]
        public string Properties { get; set; }

    }

    public class SimpleRangedWeapon
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }

        [JsonPropertyName("Damage")]
        public string Damage { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }

        [JsonPropertyName("Properties")]
        public string Properties { get; set; }
    }

    public class Explosive
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }
    }

    public class Firearms
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; }

        [JsonPropertyName("Damage")]
        public string Damage { get; set; }

        [JsonPropertyName("Weight")]
        public string Weight { get; set; }

        [JsonPropertyName("Properties")]
        public string Properties { get; set; }
    }

    public class MartialMeleeCategory
    {
        [JsonPropertyName("Martial Melee Weapons")]
        public List<MartialMeleeWeapon> MartialMeleeWeapons { get; set; }
    }

    public class MartialRangedCategory
    {
        [JsonPropertyName("Martial Ranged Weapons")]
        public List<MartialRangedWeapon> MartialRangedWeapons { get; set; }
    }

    public class SimpleMeleeCategory
    {
        [JsonPropertyName("Simple Melee Weapons")]
        public List<SimpleMeleeWeapon> SimpleMeleeWeapons { get; set; }
    }

    public class SimpleRangedCategory
    {
        [JsonPropertyName("Simple Ranged Weapons")]
        public List<SimpleRangedWeapon> SimpleRangedWeapons { get; set; }
    }

    public class ExplosiveCategory
    {
        [JsonPropertyName("Explosives")]
        public List<Explosive> Explosives { get; set; }
    }

    public class FirearmsCategory
    {
        [JsonPropertyName("Firearms")]
        public List<Firearms> Firearm { get; set; }
    }

    public class MartialMeleeData
    {
        [JsonPropertyName("Martial Melee Weapon Categories")]
        public List<MartialMeleeWeapon> MartialMeleeCategories { get; set; }
    }

    public class MartialRangeData
    {
        [JsonPropertyName("Martial Ranged Weapon Categories")]
        public List<MartialRangedWeapon> MartialRangedCategories { get; set; }
    }

    public class SimpleMeleeData
    {
        [JsonPropertyName("Simple Melee Weapon Categories")]
        public List<SimpleMeleeWeapon> SimpleMeleeCategories { get; set; }
    }

    public class SimpleRangedData
    {
        [JsonPropertyName("Simple Ranged Weapon Categories")]
        public List<SimpleRangedWeapon> SimpleRangedCategories { get; set; }
    }

    public class ExplosiveData
    {
        [JsonPropertyName("Explosive Categories")]
        public List<Explosive> ExplosiveCategories { get; set; }
    }
    public class FirearmsData
    {
        [JsonPropertyName("Firearms Categories")]
        public  List<Firearms> FirearmsCategories { get; set; }
    }

    internal class Martial_Melee_Weapons_Json_Loader
    {
        // martialMeleeCategory
        public static MartialMeleeData LoadMartialMeleeData(string jsonFilePath)
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
                    return new MartialMeleeData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new MartialMeleeData();
                }

                var martialMeleelData = JsonSerializer.Deserialize<MartialMeleeData>(jsonData);

                if (martialMeleelData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MartialMeleeData.");
                    return new MartialMeleeData();
                }

                return martialMeleelData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class Martial_Ranged_Weapons_Json_Loader
    {
        public static MartialRangeData LoadMartialRangedData(string jsonFilePath)
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
                    return new MartialRangeData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new MartialRangeData();
                }

                var martialRangedData = JsonSerializer.Deserialize<MartialRangeData>(jsonData);

                if (martialRangedData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default MartialRangedData.");
                    return new MartialRangeData();
                }

                return martialRangedData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class Simple_Melee_Weapons_Json_Loader
    {
        public static SimpleMeleeData LoadSimpleMeleeData(string jsonFilePath)
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
                    return new SimpleMeleeData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new SimpleMeleeData();
                }

                var simpleMeleeData = JsonSerializer.Deserialize<SimpleMeleeData>(jsonData);

                if (simpleMeleeData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default SimpleMeleeData.");
                    return new SimpleMeleeData();
                }

                return simpleMeleeData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class Simple_Ranged_Weapons_Json_Loader
    {
        public static SimpleRangedData LoadSimpleRangedData(string jsonFilePath)
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
                    return new SimpleRangedData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new SimpleRangedData();
                }

                var simpleRangedData = JsonSerializer.Deserialize<SimpleRangedData>(jsonData);

                if (simpleRangedData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default SimpleRangedData.");
                    return new SimpleRangedData();
                }

                return simpleRangedData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }


    internal class Explosive_Json_Loader
    {
        public static ExplosiveData LoadExplosiveData(string jsonFilePath)
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
                    return new ExplosiveData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new ExplosiveData();
                }

                var explosiveData = JsonSerializer.Deserialize<ExplosiveData>(jsonData);

                if (explosiveData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default ExplosiveData.");
                    return new ExplosiveData();
                }

                return explosiveData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class Firearms_Json_Loader
    {
        public static FirearmsData LoadFirearmsData(string jsonFilePath)
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
                    return new FirearmsData();
                }

                string jsonData = File.ReadAllText(jsonFilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new FirearmsData();
                }

                var firearmsData = JsonSerializer.Deserialize<FirearmsData>(jsonData);

                if (firearmsData == null)
                {
                    Console.WriteLine("Deserialization returned null. Returning default FirearmsData.");
                    return new FirearmsData();
                }

                return firearmsData;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw; // Use 'throw;' without specifying the exception to re-throw the caught exception.
            }
        }
    }

    internal class MartialMeleeWeaponLoader : ILoader
    {
        public void Load()
        {
            throw new NotImplementedException();
        }

        void ILoader.Load()
        {
            Console.WriteLine("Loading Martial Melee Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathMeleeMartial = "Weapons\\Martial_Weapons_Melee.json";

            var martMelee = Martial_Melee_Weapons_Json_Loader.LoadMartialMeleeData(jsonFilePathMeleeMartial);

            // Display the data for Martial Melee Weapons
            if (martMelee != null && martMelee.MartialMeleeCategories != null)
            {
                Console.WriteLine("Martial Melee Weapons:");
                foreach (var martmeweapons in martMelee.MartialMeleeCategories)
                {
                    Console.WriteLine($"- Name: {martmeweapons.Name}, Cost: {martmeweapons.Cost}, Damage: {martmeweapons.Damage}, Weight: {martmeweapons.Weight}, Properties: {martmeweapons.Properties}");
                }
            }
        }
    }

    internal class MartialRangedWeaponLoader : ILoader
    {
        public void Load()
        {
            throw new NotImplementedException();
        }

        void ILoader.Load()
        {
            Console.WriteLine("Loading Martial Ranged Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathRangedMartial = "\\Weapons\\Martial_Weapons_Ranged.json";

            var martRange = Martial_Ranged_Weapons_Json_Loader.LoadMartialRangedData(jsonFilePathRangedMartial);

            // Display the data for Martial Ranged Weapons 
            if (martRange != null && martRange.MartialRangedCategories != null)
            {
                Console.WriteLine("Martial Ranged Weapons::");
                foreach (var martRaweapons in martRange.MartialRangedCategories)
                {
                    Console.WriteLine($"- Name: {martRaweapons.Name}, Cost: {martRaweapons.Cost}, Damage: {martRaweapons.Damage}, Weight: {martRaweapons.Weight}, Properties: {martRaweapons.Properties}");
                }
            }
        }
    }

    internal class SimpleMeleeWeaponLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Simple Melee Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathMeleSimplel = "\\Weapons\\Simple_Weapons_Melee.json";

            var simpMelee = Simple_Melee_Weapons_Json_Loader.LoadSimpleMeleeData(jsonFilePathMeleSimplel);

            // Display the data for Simple Melee Weapons 
            if (simpMelee != null && simpMelee.SimpleMeleeCategories != null)
            {
                Console.WriteLine("Simple Melee Weapons:");
                foreach (var simpleMeweapons in simpMelee.SimpleMeleeCategories)
                {
                    Console.WriteLine($"- Name: {simpleMeweapons.Name}, Cost: {simpleMeweapons.Cost}, Damage: {simpleMeweapons.Damage}, Weight: {simpleMeweapons.Weight}, Properties: {simpleMeweapons.Properties}");
                }
            }
        }
    }

    internal class SimpleRangedWeaponLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Ranged Melee Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathRangedSimple = "\\Weapons\\Simple_Weapons_Ranged.json";

            var simpRang = Simple_Ranged_Weapons_Json_Loader.LoadSimpleRangedData(jsonFilePathRangedSimple);

            // Display the data for Simple Melee Weapons 
            if (simpRang != null && simpRang.SimpleRangedCategories != null)
            {
                Console.WriteLine("Simple Ranged Weapons:");
                foreach (var simRaweapons in simpRang.SimpleRangedCategories)
                {
                    Console.WriteLine($"- Name: {simRaweapons.Name}, Cost: {simRaweapons.Cost}, Damage: {simRaweapons.Damage}, Weight: {simRaweapons.Weight}, Properties: {simRaweapons.Properties}");
                }
            }
        }
    }

    internal class ExplosiveLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Explosive Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathExplosiveRenaissance = "\\Weapons\\Explosives_Renaissance.json";
            string jsonFilePathExplosiveModern = "\\Weapons\\Explosives_Modern.json";

            var renexplosive = Explosive_Json_Loader.LoadExplosiveData(jsonFilePathExplosiveRenaissance);
            var modexplosive = Explosive_Json_Loader.LoadExplosiveData(jsonFilePathExplosiveModern);

            if (renexplosive != null && renexplosive.ExplosiveCategories != null)
            {
                Console.WriteLine("Renaissance Explosives:");
                foreach (var renExplode in renexplosive.ExplosiveCategories)
                {
                    Console.WriteLine($"- Name: {renExplode.Name}, Cost: {renExplode.Cost}, Weight: {renExplode.Weight}, Description: {renExplode.Description}");
                }
            }

            if (modexplosive != null && modexplosive.ExplosiveCategories != null)
            {
                Console.WriteLine("Modern Explosives:");
                foreach (var modExplode in modexplosive.ExplosiveCategories)
                {
                    Console.WriteLine($"- Name: {modExplode.Name}, Cost: {modExplode.Cost}, Weight: {modExplode.Weight}, Description: {modExplode.Description}");
                }
            }
        }
    }

    internal class FirearmsLoader : ILoader
    {
        public void Load()
        {
            Console.WriteLine("Loading Firearms Data ...");
            // Define the paths to the JSON files#
            string jsonFilePathFirearmsRenaissance = "\\Weapons\\Firearms_Renaissance.json";
            string jsonFilePathFirearmsModern = "\\Weapons\\Firearms_Modern.json";
            string jsonFilePathFirearmsFuture = "\\Weapons\\Firearms_Futuristic.json";

            var renFirearms = Firearms_Json_Loader.LoadFirearmsData(jsonFilePathFirearmsRenaissance);
            var modFirearms = Firearms_Json_Loader.LoadFirearmsData(jsonFilePathFirearmsModern);
            var futFirearms = Firearms_Json_Loader.LoadFirearmsData(jsonFilePathFirearmsFuture);

            if (renFirearms != null && renFirearms.FirearmsCategories != null)
            {
                Console.WriteLine("Rennaissance Firearms:");
                foreach (var renFire in renFirearms.FirearmsCategories)
                {
                    Console.WriteLine($"- Name: {renFire.Name}, Cost: {renFire.Cost}, Damage: {renFire.Damage}, Weight: {renFire.Weight}, Properties: {renFire.Properties}");
                }
            }

            if (modFirearms != null && modFirearms.FirearmsCategories != null)
            {
                Console.WriteLine("Modern Firearms:");
                foreach (var modFire in modFirearms.FirearmsCategories)
                {
                    Console.WriteLine($"- Name: {modFire.Name}, Cost: {modFire.Cost}, Damage: {modFire.Damage}, Weight: {modFire.Weight}, Properties: {modFire.Properties}");
                }
            }

            if (futFirearms != null && futFirearms.FirearmsCategories != null)
            {
                Console.WriteLine("Futuristic Firearms:");
                foreach (var futFire in futFirearms.FirearmsCategories)
                {
                    Console.WriteLine($"- Name: {futFire.Name}, Cost: {futFire.Cost}, Damage: {futFire.Damage}, Weight: {futFire.Weight}, Properties: {futFire.Properties}");
                }
            }
        }
    }
}
