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

    public static void SeedReferenceData(ApprovalsContext context, IConfiguration configuration)
    {
        if (configuration.IsLocalDatabase(configuration))
        {
            // If prod allow LiquiBase to perform schema setup and seed from SQL script
            context.Database.EnsureCreated();

            context.SeedData(context.ApprovalGroups, ReadSeedData<ApprovalGroupEntity>($"{BaseDir}/approvalGroups.json"));
            context.SeedData(context.Approvers, ReadSeedData<ApproverEntity>($"{BaseDir}/approvers.json"));
            context.SeedApproverGroupLinks(context.ApproverAprovalGroups, ReadSeedData<ApproverApprovalGroupMap>($"{BaseDir}/approverApprovalGroups.json"));
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
    private static IEnumerable<T> ReadSeedData<T>(string path)
    {
        var raw = File.ReadAllText(Path.Combine(ExecutionPath, path));
        return JsonSerializer.Deserialize<IEnumerable<T>>(raw)!;
    }

    private class ApproverApprovalGroupMap
    {
        public string ApproverEmail { get; set; } = "";
        public string ApprovalGroup { get; set; } = "";
    }
}