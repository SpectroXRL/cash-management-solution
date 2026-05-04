using CashManagementSolution.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace CashManagementSolution.Api.Workers;

public class WireTransferProcessingWorker(IServiceScopeFactory scopeFactory, ILogger<WireTransferProcessingWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessSubmittedTransfersAsync();
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task ProcessSubmittedTransfersAsync()
    {
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CashManagementDbContext>();

        var pending = await context.WireTransfers
            .Where(w => w.Status == WireTransferStatus.Submitted)
            .ToListAsync();

        foreach (var transfer in pending)
        {
            try
            {
                transfer.Validate();
                logger.LogInformation("Validated transfer {Id}", transfer.Id);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogWarning("Could not validate transfer {Id}: {Reason}", transfer.Id, ex.Message);
            }
        }

        await context.SaveChangesAsync();
    }
}
