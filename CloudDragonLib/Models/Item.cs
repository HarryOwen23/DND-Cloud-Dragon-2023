namespace CloudDragonLib.Models
{
    /// <summary>
    /// Generic item definition used for simple equipment lists.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Item name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Item type such as Tool, Potion or Misc.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Weight in pounds.
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// Optional description of the item.
        /// </summary>
        public string Description { get; set; }
    }
}
