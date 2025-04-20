using CloudDragon.Equipment;
using Newtonsoft.Json;

namespace CloudDragon.Equipment
{
    public static class EquipmentDataLoader
    {
        public static List<EquipmentItem> LoadFallback(string filename)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Equipment");
            var path = Path.Combine(basePath, filename);

            if (!File.Exists(path))
                throw new FileNotFoundException($"Equipment file missing: {path}");

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<EquipmentItem>>(json);
        }
    }
}
