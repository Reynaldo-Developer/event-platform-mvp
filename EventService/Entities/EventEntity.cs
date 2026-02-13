namespace EventService.Entities
{
    public class EventEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Place { get; set; } = default!;
        public List<ZoneEntity> Zones { get; set; } = new();
    }
}
