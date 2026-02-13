namespace EventService.Contracts
{
    public record EventCreated(
      Guid MessageId,
      Guid EventId,
      string Name,
      DateTime OccurredAt,
      Guid CorrelationId,
      int Version
  );
}
