namespace CloudDragonApi.Models
{
    public class CombatSession
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public List<Combatant> Combatants { get; set; } = new();

        public int Round { get; set; } = 1;
        public int TurnIndex { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<string> Log { get; set; } = new();
    }
}
