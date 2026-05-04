namespace CashManagementSolution.Api.Models;

public record SubmitWireTransferRequest(
    string FromAccountId,
    string ToAccountId,
    decimal Amount
);
