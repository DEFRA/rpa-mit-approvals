using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;

namespace EST.MIT.Approvals.Api.SeedProvider.Provider;

[ExcludeFromCodeCoverage]
public static class SeedProvider
{
    private const string BaseDir = "Resources/SeedData";
    private static readonly string ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    public static void SeedReferenceData(ApprovalsContext context, IConfiguration configuration, SQLscriptWriter? scriptWriter, string version)
    {
        if (configuration.IsLocalDatabase(configuration))
        {
            // If prod allow LiquiBase to perform schema setup and seed from SQL script

            // 1) ensure nuget.exe is downloaded installed (placed in system32)
            //   Note: Make sure there's a package source mapping for EST.MIT.* to DEFRA-EST in (Visual Studio) NuGet Package Manager
            // 2) from cmd prompt, navigate to the solution folder, e.g.
            //        cd C:\Users\<userid>\source\repos\DEFRA\rpa-mit-approvals
            // 3) at the command prompt, run:
            //        nuget install "EST.MIT.Approvals.SeedData" - source "DEFRA-EST" - version "1.0.2" (Where version is the package version)
            // this should install the package files to
            //        C:\Users\<userid>\source\repos\DEFRA\rpa-mit-approvals\EST.MIT.Approvals.SeedData.1.0.2
            // the BaseDir is set to load the seed data from there.
            // 4) After seeding locally, the database should be created and populated and the SQL scripts to be run on the server should be saved to:
            //        C:\Users\<userid>\source\repos\DEFRA\rpa-mit-approvals\EST.MIT.Approvals.Api

            var BaseDirRelativeToExecutionPath = $"..\\..\\..\\..\\EST.MIT.Approvals.SeedData.{version}\\contentFiles\\any\\any\\Resources\\SeedData";

            var ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

            var BaseDir = Path.Combine(ExecutionPath, BaseDirRelativeToExecutionPath);

            using (scriptWriter)
            {
                scriptWriter?.Open(version);

                object[] parameters = Array.Empty<object>();
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS approver_aproval_groups", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS approvers ", parameters);
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS approval_groups", parameters);

                context.Database.EnsureCreated();

                context.SeedData(context.ApprovalGroups, ReadSeedData<ApprovalGroupEntity>($"{BaseDir}/approvalGroups.json"));
                context.SeedData(context.Approvers, ReadSeedData<ApproverEntity>($"{BaseDir}/approvers.json"));
                context.SeedApproverGroupLinks(context.ApproverAprovalGroups, ReadSeedData<ApproverApprovalGroupMap>($"{BaseDir}/approverApprovalGroups.json"));

                scriptWriter?.Close();
            }
        }
    }

    public static void SeedData<T>(this ApprovalsContext context, DbSet<T> entity, IEnumerable<T> data)
        where T : class
    {
        foreach (var existingEntity in entity)
        {
            entity.Remove(existingEntity);
        }
        entity.AddRange(data);
        context.SaveChanges();
    }

    private static void SeedApproverGroupLinks(this ApprovalsContext context, DbSet<ApproverApprovalGroupEntity> approverGroupLinks, IEnumerable<ApproverApprovalGroupMap> mappingData)
    {
        foreach (var existingLink in approverGroupLinks)
        {
            approverGroupLinks.Remove(existingLink);
        }
        foreach (ApproverApprovalGroupMap mapItem in mappingData)
        {
            var newLink = context.CreateApproverApprovalGroupEntity(mapItem);
            if (newLink is not null && !approverGroupLinks.Any(l => newLink.ApproverId == l.ApproverId && newLink.ApprovalGroupId == l.ApprovalGroupId))
            {
                approverGroupLinks.Add(newLink);
                context.SaveChanges();
            }
        }
    }

    private static ApproverApprovalGroupEntity? CreateApproverApprovalGroupEntity(this ApprovalsContext context, ApproverApprovalGroupMap link)
    {
        var approver = context.Approvers.SingleOrDefault(x => x.EmailAddress.ToLower() == link.ApproverEmail.ToLower());
        var approvalGroup = context.ApprovalGroups.SingleOrDefault(x => x.Code.ToLower() == link.ApprovalGroup.ToLower());
        if (approver is null) return null;
        if (approvalGroup is null) return null;
        return new ApproverApprovalGroupEntity(approver.Id, approvalGroup.Id);
    }

    private static IEnumerable<T> ReadSeedData<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            var raw = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<IEnumerable<T>>(raw)!;
        }
        return null!;
    }

    private class ApproverApprovalGroupMap
    {
        public string ApproverEmail { get; set; } = "";
        public string ApprovalGroup { get; set; } = "";
    }
}