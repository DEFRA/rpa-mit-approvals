using Approvals.Api.Services;
using System.Diagnostics.CodeAnalysis;

namespace Approvals.Api.Endpoints;

[ExcludeFromCodeCoverage]
public static class ApprovalsDefinition
{
    public static IServiceCollection AddApprovalsServices(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceApproverService, InvoiceApproverService>();
        return services;
    }
}