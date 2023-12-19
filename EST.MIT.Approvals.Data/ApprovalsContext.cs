using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data;

[ExcludeFromCodeCoverage]
public class ApprovalsContext : DbContext
{
    public DbSet<ApproverEntity> Approvers => Set<ApproverEntity>();
    public DbSet<ApprovalGroupEntity> ApprovalGroups => Set<ApprovalGroupEntity>();
    public DbSet<ApproverApprovalGroupEntity> ApproverAprovalGroups => Set<ApproverApprovalGroupEntity>();

    public ApprovalsContext(DbContextOptions<ApprovalsContext> options) : base(options)
    {
    }

    private readonly StreamWriter _logStream = new StreamWriter("MIT_Approvals_Seed_SQL.log", append: true);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name });
        optionsBuilder.LogTo(_logStream.WriteLine, new[] { DbLoggerCategory.Database.Name });
        optionsBuilder.LogTo(_logStream.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApproverEntity>()
            .HasMany(e => e.ApprovalGroups)
            .WithMany(e => e.Approvers)
            .UsingEntity<ApproverApprovalGroupEntity>(
                l => l.HasOne<ApprovalGroupEntity>().WithMany().HasForeignKey(e => e.ApprovalGroupId),
                r => r.HasOne<ApproverEntity>().WithMany().HasForeignKey(e => e.ApproverId));
    }
}