namespace CloudDragonLib.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; } // Tool, Potion, Misc
        public float Weight { get; set; }
        public string Description { get; set; }
    }
}
