using CashManagementSolution.Api.Domain.Events;

namespace CashManagementSolution.Api.Domain;

public class WireTransfer
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void ClearDomainEvents() => _domainEvents.Clear();

    public Guid Id { get; private set; }
    public string FromAccountId { get; private set; }
    public string ToAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public WireTransferStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }

    private WireTransfer()
    {
        FromAccountId = null!;
        ToAccountId = null!;
    }

    public static WireTransfer Submit(string fromAccountId, string toAccountId, decimal amount)
    {
        var transfer = new WireTransfer
        {
            Id = Guid.NewGuid(),
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            Status = WireTransferStatus.Submitted,
            SubmittedAt = DateTime.UtcNow
        };

        transfer._domainEvents.Add(new WireTransferSubmitted(transfer.Id, transfer.SubmittedAt));

        return transfer;
    }

    public void Validate()
    {
        if (Status != WireTransferStatus.Submitted)
            throw new InvalidOperationException($"Cannot validate a transfer in '{Status}' status.");
        Status = WireTransferStatus.Validated;
    }

    public void Authorize()
    {
        if (Status != WireTransferStatus.Validated)
            throw new InvalidOperationException($"Cannot authorize a transfer in '{Status}' status.");
        Status = WireTransferStatus.Authorized;
    }

    public void Settle()
    {
        if (Status != WireTransferStatus.Authorized)
            throw new InvalidOperationException($"Cannot settle a transfer in '{Status}' status.");
        Status = WireTransferStatus.Settled;
    }

    public void Fail()
    {
        if (Status is WireTransferStatus.Settled or WireTransferStatus.Reversed)
            throw new InvalidOperationException($"Cannot fail a transfer in '{Status}' status.");
        Status = WireTransferStatus.Failed;
    }

    public void Reverse()
    {
        if (Status != WireTransferStatus.Settled)
            throw new InvalidOperationException($"Cannot reverse a transfer in '{Status}' status.");
        Status = WireTransferStatus.Reversed;
    }
}
