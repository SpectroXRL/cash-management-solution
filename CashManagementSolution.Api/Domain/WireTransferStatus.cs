namespace CashManagementSolution.Api.Domain;

public enum WireTransferStatus
{
    Submitted,
    Validated,
    Authorized,
    Settled,
    Failed,
    Reversed
}
