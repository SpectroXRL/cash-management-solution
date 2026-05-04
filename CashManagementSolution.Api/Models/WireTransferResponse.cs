using CashManagementSolution.Api.Domain;

namespace CashManagementSolution.Api.Models;

public record WireTransferResponse(
    Guid Id,
    string FromAccountId,
    string ToAccountId,
    decimal Amount,
    WireTransferStatus Status,
    DateTime SubmittedAt
);
