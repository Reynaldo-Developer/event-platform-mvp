namespace EventService.Contracts
{
    public class CreateEventRequest
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public string Place { get; set; } = default!;
        public List<CreateZoneRequest> Zones { get; set; } = new();
    }

    public class CreateZoneRequest
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}
