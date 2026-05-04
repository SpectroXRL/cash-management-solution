using CashManagementSolution.Api.Domain;
using CashManagementSolution.Api.Models;

namespace CashManagementSolution.Api.Services;

public class WireTransferService(CashManagementDbContext context)
{

    public async Task<WireTransferResponse> SubmitAsync(string fromAccountId, string toAccountId, decimal amount)
    {
        var wireTransfer = WireTransfer.Submit(fromAccountId, toAccountId, amount);

        context.WireTransfers.Add(wireTransfer);
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

    public async Task<WireTransferResponse?> ValidateAsync(Guid id)
    {
        var wireTransfer = await context.WireTransfers.FindAsync(id);

        if (wireTransfer is null)
            return null;

        wireTransfer.Validate();

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

    public async Task<WireTransferResponse?> GetByIdAsync(Guid id)
    {
        var wireTransfer = await context.WireTransfers.FindAsync(id);

        if (wireTransfer is null)
            return null;

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
