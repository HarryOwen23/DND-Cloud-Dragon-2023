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
        public int Cost { get; set; }

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
        public int Cost { get; set; }

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
        public int Cost { get; set; }

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
        public object? Cost { get; set; }
    }

    public class Firearms
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cost")]
        public string Cost { get; set; } = string.Empty;

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
        public Dictionary<string, List<MartialMeleeWeapon>> MartialMeleeCategories { get; set; }
    }

    public class MartialRangeData
    {
        [JsonPropertyName("Martial Ranged Weapon Categories")]
        public Dictionary<string, List<MartialRangedWeapon>> MartialRangedCategories { get; set; }
    }

    public class SimpleMeleeData
    {
        [JsonPropertyName("Simple Melee Weapon Categories")]
        public Dictionary<string, List<SimpleMeleeWeapon>> SimpleMeleeCategories { get; set; }
    }

    public class SimpleRangedData
    {
        [JsonPropertyName("Simple Ranged Weapon Categories")]
        public Dictionary<string, List<SimpleRangedWeapon>> SimpleRangedCategories { get; set; }
    }

    public class ExplosiveData
    {
        [JsonPropertyName("Explosive Categories")]
        public Dictionary<string, List<Explosive>> ExplosiveCategories { get; set; }
    }
    public class FirearmsData
    {
        [JsonPropertyName("Firearms Categories")]
        public Dictionary<string, List<Firearms>> FirearmsCategories { get; set; }
    }

    internal class Martial_Melee_Weapons_Json_Loader
    {
        public static MartialMeleeData LoadMartialMeleeData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var martialMeleeCategory = JsonSerializer.Deserialize<MartialMeleeCategory>(jsonData);

                if (martialMeleeCategory != null)
                {
                    return new MartialMeleeData
                    {
                        MartialMeleeCategories = new Dictionary<string, List<MartialMeleeWeapon>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), martialMeleeCategory.MartialMeleeWeapons ?? new List<MartialMeleeWeapon>() }
                    }
                    };
                }
                else
                {
                    Console.WriteLine("Deserialization failed or the category is null.");
                    return new MartialMeleeData(); // or handle it according to your application's logic
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Martial_Ranged_Weapons_Json_Loader
    {
        public static MartialRangeData LoadMartialRangedData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var martialRangeCategory = JsonSerializer.Deserialize<MartialRangedCategory>(jsonData);

                if (martialRangeCategory != null)
                {
                    return new MartialRangeData
                    {
                        MartialRangedCategories = new Dictionary<string, List<MartialRangedWeapon>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), martialRangeCategory.MartialRangedWeapons ?? new List<MartialRangedWeapon>() }
                    }
                    };
                }
                else
                {
                    Console.WriteLine("Deserialization failed or the category is null.");
                    return new MartialRangeData(); // or handle it according to your application's logic
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Simple_Melee_Weapons_Json_Loader
    {
        public static SimpleMeleeData LoadSimpleMeleeData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var simpleMeleeCategory = JsonSerializer.Deserialize<SimpleMeleeCategory>(jsonData);

                if (simpleMeleeCategory != null)
                {
                    return new SimpleMeleeData
                    {
                        SimpleMeleeCategories = new Dictionary<string, List<SimpleMeleeWeapon>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), simpleMeleeCategory.SimpleMeleeWeapons ?? new List<SimpleMeleeWeapon>() }
                    }
                    };
                }
                else
                {
                    Console.WriteLine("Deserialization failed or the category is null.");
                    return new SimpleMeleeData(); // or handle it according to your application's logic
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Simple_Ranged_Weapons_Json_Loader
    {
        public static SimpleRangedData LoadSimpleRangedData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var simpleRangedCategory = JsonSerializer.Deserialize<SimpleRangedCategory>(jsonData);

                if (simpleRangedCategory != null)
                {
                    return new SimpleRangedData
                    {
                        SimpleRangedCategories = new Dictionary<string, List<SimpleRangedWeapon>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), simpleRangedCategory.SimpleRangedWeapons ?? new List<SimpleRangedWeapon>() }
                    }
                    };
                }
                else
                {
                    Console.WriteLine("Deserialization failed or the category is null.");
                    return new SimpleRangedData(); // or handle it according to your application's logic
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }


    internal class Explosive_Json_Loader
    {
        public static ExplosiveData LoadExplosiveData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var explosiveCategory = JsonSerializer.Deserialize<ExplosiveCategory>(jsonData);

                if (explosiveCategory != null)
                {
                    return new ExplosiveData
                    {
                        ExplosiveCategories = new Dictionary<string, List<Explosive>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), explosiveCategory.Explosives ?? new List<Explosive>() }
                    }
                    };
                }
                else
                {
                    Console.WriteLine("Deserialization failed or the category is null.");
                    return new ExplosiveData(); // or handle it according to your application's logic
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class Firearms_Json_Loader
    {
        public static FirearmsData LoadFirearmsData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                var firearmsCategory = JsonSerializer.Deserialize<FirearmsCategory>(jsonData);

                if (firearmsCategory != null)
                {
                    return new FirearmsData
                    {
                        FirearmsCategories = new Dictionary<string, List<Firearms>>
                    {
                        { Path.GetFileNameWithoutExtension(jsonFilePath), firearmsCategory.Firearm ?? new List<Firearms>() }
                    }
                    };
                }
                else
                {
                    Console.WriteLine("Deserialization failed or the category is null.");
                    return new FirearmsData(); // or handle it according to your application's logic
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading JSON file: " + e.Message);
                throw;
            }
        }
    }

    internal class WeaponLoaderHelper
    {
        public static void DisplayWeaponData<T>(string category, List<T> weapons)
        {
            Console.WriteLine($"{category}:");
            foreach (var weapon in weapons)
            {
                Console.WriteLine("- " + GetWeaponString(weapon));
            }
        }

        private static string GetWeaponString<T>(T weapon)
        {
            var properties = typeof(T).GetProperties();
            var propertyValues = properties.Select(prop =>
            {
                if (typeof(T) == typeof(MartialMeleeWeapon) || typeof(T) == typeof(MartialRangedWeapon) ||
                    typeof(T) == typeof(SimpleMeleeWeapon) || typeof(T) == typeof(SimpleRangedWeapon) ||
                    typeof(T) == typeof(Firearms))
                {
                    return $"{prop.Name}: {prop.GetValue(weapon)}";
                }
                else if (typeof(T) == typeof(Explosive))
                {
                    return prop.Name == "Cost" ? null : $"{prop.Name}: {prop.GetValue(weapon)}";
                }
                else
                {
                    return null;
                }
            });

            return string.Join(", ", propertyValues.Where(p => p != null));
        }
    }


    internal class MartialMeleeWeaponLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Martial Melee Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathMeleeMartial = "\\Weapons\\Martial_Weapons_Melee.json";

            var martMelee = Martial_Melee_Weapons_Json_Loader.LoadMartialMeleeData(jsonFilePathMeleeMartial);

            // Display the data for Martial Melee Weapons
            if (martMelee != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Martial Melee Weapons", martMelee.MartialMeleeCategories["Martial Melee Weapons"]);
            }
        }
    }

    internal class MartialRangedWeaponLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Martial Ranged Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathRangedMartial = "\\Weapons\\Martial_Weapons_Ranged.json";

            var martRange = Martial_Ranged_Weapons_Json_Loader.LoadMartialRangedData(jsonFilePathRangedMartial);

            // Display the data for Martial Ranged Weapons 
            if (martRange != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Martial Ranged Weapons", martRange.MartialRangedCategories["Martial Ranged Weapons"]);
            }
        }
    }

    internal class SimpleMeleeWeaponLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Simple Melee Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathMeleSimplel = "\\Weapons\\Simple_Weapons_Melee.json";

            var simpMelee = Simple_Melee_Weapons_Json_Loader.LoadSimpleMeleeData(jsonFilePathMeleSimplel);

            // Display the data for Simple Melee Weapons 
            if (simpMelee != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Simple Melee Weapons", simpMelee.SimpleMeleeCategories["Simple Weapons"]);
            }
        }
    }

    internal class SimpleRangedWeaponLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Ranged Melee Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathRangedSimple = "\\Weapons\\Simple_Weapons_Ranged.json";

            var simpRang = Simple_Ranged_Weapons_Json_Loader.LoadSimpleRangedData(jsonFilePathRangedSimple);

            // Display the data for Simple Melee Weapons 
            if (simpRang != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Simple Ranged Weapons", simpRang.SimpleRangedCategories["Simple Weapons"]);
            }
        }
    }

    internal class ExplosiveLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Explosive Weapon Data ...");
            // Define the paths to the JSON files
            string jsonFilePathExplosiveRenaissance = "\\Weapons\\Explosives_Renaissance.json";
            string jsonFilePathExplosiveModern = "\\Weapons\\Explosives_Modern.json";

            var renexplosive = Explosive_Json_Loader.LoadExplosiveData(jsonFilePathExplosiveRenaissance);
            var modexplosive = Explosive_Json_Loader.LoadExplosiveData(jsonFilePathExplosiveModern);

            if (renexplosive != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Renaissance Explosive", renexplosive.ExplosiveCategories["Explosives"]);
            }

            if (modexplosive != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Modern Explosive", modexplosive.ExplosiveCategories["Explosives"]);
            }
        }
    }

    internal class FirearmsLoader : ILoader
    {
        void ILoader.Load()
        {
            Console.WriteLine("Loading Firearms Data ...");
            // Define the paths to the JSON files#
            string jsonFilePathFirearmsRenaissance = "\\Weapons\\Firearms_Renaissance.json";
            string jsonFilePathFirearmsModern = "\\Weapons\\Firearms_Modern.json";
            string jsonFilePathFirearmsFuture = "\\Weapons\\Firearms_Futuristic.json";

            var renFirearms = Firearms_Json_Loader.LoadFirearmsData(jsonFilePathFirearmsRenaissance);
            var modFirearms = Firearms_Json_Loader.LoadFirearmsData(jsonFilePathFirearmsModern);
            var futFirearms = Firearms_Json_Loader.LoadFirearmsData(jsonFilePathFirearmsFuture);

            if (renFirearms != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Fire Renaissance Weapons", renFirearms.FirearmsCategories["RenaissanceFirearms"]);
            }

            if (modFirearms != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Fire Modern Weapons", modFirearms.FirearmsCategories["ModernFirearms"]);
            }

            if (futFirearms != null)
            {
                WeaponLoaderHelper.DisplayWeaponData("Fire Futuristic Weapons", futFirearms.FirearmsCategories["FuturisticFirearms"]);
            }
        }
    }
}
