using CashManagementSolution.Api.Domain;
using CashManagementSolution.Api.Models;

namespace CashManagementSolution.Api.Services;

public class WireTransferService(CashManagementDbContext context)
{
    public async Task<WireTransferResponse> SubmitAsync(string fromAccountId, string toAccountId, decimal amount)
    {
        var wireTransfer = WireTransfer.Submit(fromAccountId, toAccountId, amount);

        await context.WireTransfers.AddAsync(wireTransfer);
        await context.SaveChangesAsync();

        return new WireTransferResponse(
            wireTransfer.Id,
            wireTransfer.FromAccountId,
            wireTransfer.ToAccountId,
            wireTransfer.Amount,
            wireTransfer.Status,
            wireTransfer.SubmittedAt
        );
    }
}
