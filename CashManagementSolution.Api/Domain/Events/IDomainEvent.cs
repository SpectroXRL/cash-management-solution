namespace CashManagementSolution.Api.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}
