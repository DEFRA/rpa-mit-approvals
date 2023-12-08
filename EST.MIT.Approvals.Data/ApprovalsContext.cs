using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data;

[ExcludeFromCodeCoverage]
public class ApprovalsContext : DbContext
{
    public DbSet<ApproverEntity> Approvers => Set<ApproverEntity>();

    public DbSet<SchemeEntity> Schemes => Set<SchemeEntity>();
    public DbSet<ApprovalGroupEntity> ApprovalGroups => Set<ApprovalGroupEntity>();
    public DbSet<ApproverApprovalGroupEntity> ApproverAprovalGroups => Set<ApproverApprovalGroupEntity>();

    public ApprovalsContext(DbContextOptions<ApprovalsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApproverEntity>()
            .HasMany(e => e.Schemes)
            .WithMany(e => e.Approvers)
            .UsingEntity<ApproverSchemeEntity>(
                l => l.HasOne<SchemeEntity>().WithMany().HasForeignKey(e => e.SchemeId),
                r => r.HasOne<ApproverEntity>().WithMany().HasForeignKey(e => e.ApproverId));
    }
}