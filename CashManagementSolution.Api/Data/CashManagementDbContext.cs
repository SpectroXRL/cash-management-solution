using CashManagementSolution.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace CashManagementSolution.Api;

public class CashManagementDbContext : DbContext
{
    public CashManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    protected CashManagementDbContext()
    {
    }

    public DbSet<WireTransfer> WireTransfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WireTransfer>()
            .Property(w => w.Status)
            .HasConversion<string>();
    }
}
