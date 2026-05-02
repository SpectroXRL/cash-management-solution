namespace CashManagementSolution.Api.Domain;

public class WireTransfer
{
    public Guid Id { get; private set; }
    public string FromAccountId { get; private set; }
    public string ToAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public WireTransferStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }

    private WireTransfer() { }

    public static WireTransfer Submit(string fromAccountId, string toAccountId, decimal amount)
    {
        return new WireTransfer
        {
            Id = Guid.NewGuid(),
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            Amount = amount,
            Status = WireTransferStatus.Submitted,
            SubmittedAt = DateTime.UtcNow
        };
    }
}
