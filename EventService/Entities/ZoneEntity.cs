namespace EventService.Entities
{
    public class ZoneEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }

        public Guid EventId { get; set; }
    }
}
