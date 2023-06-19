using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EST.MIT.Approvals.Data.Models;
using Microsoft.Extensions.Configuration;

namespace EST.MIT.Approvals.Data;

[ExcludeFromCodeCoverage]
public class ApprovalsContext : DbContext
{
    public DbSet<ApproverEntity> Approvers => Set<ApproverEntity>();

    public DbSet<GradeEntity> Grades => Set<GradeEntity>();

    public DbSet<SchemeEntity> Schemes => Set<SchemeEntity>();

    public DbSet<SchemeGradeEntity> SchemeGrades => Set<SchemeGradeEntity>();

    public DbSet<SchemeApprovalGradeEntity> SchemeApprovalGrades => Set<SchemeApprovalGradeEntity>();

    public DbSet<ApproverSchemeGradeEntity> ApproverSchemeGrades => Set<ApproverSchemeGradeEntity>();


    public ApprovalsContext(DbContextOptions<ApprovalsContext> options) : base(options)
    {
    }
}