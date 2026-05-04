namespace CashManagementSolution.Api.Domain.Events;

public record WireTransferSubmitted(Guid WireTransferId, DateTime OccurredAt) : IDomainEvent;
