using Approvals.Api.Services;
using System.Diagnostics.CodeAnalysis;
using Approvals.Api.Data.Repositories;

namespace Approvals.Api.Endpoints;

[ExcludeFromCodeCoverage]
public static class ApprovalsDefinition
{
    public static IServiceCollection AddApprovalsServices(this IServiceCollection services)
    {
        // services
        services.AddScoped<IInvoiceApproverService, InvoiceApproverService>();

        // repositories
        services.AddScoped<IApproverRepository, ApproverRepository>();

        return services;
    }
}