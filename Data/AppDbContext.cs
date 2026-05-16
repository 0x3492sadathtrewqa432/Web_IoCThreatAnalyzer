using IoCThreatAnalyzer.Models;
using Microsoft.EntityFrameworkCore;

namespace IoCThreatAnalyzer.Data;

public class AppDbContext : DbContext
{
    public DbSet<ScanResult> ScanResults => Set<ScanResult>();

    public DbSet<IocIndicator> Indicators => Set<IocIndicator>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ScanResult>()
            .HasMany(x => x.Indicators)
            .WithOne(x => x.ScanResult)
            .HasForeignKey(x => x.ScanResultId);
    }
}