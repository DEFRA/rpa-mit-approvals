using System.Diagnostics.CodeAnalysis;
using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services;
using EST.MIT.Approvals.Api.Services.Interfaces;

namespace EST.MIT.Approvals.Api.endpoints;

[ExcludeFromCodeCoverage]
public static class ApprovalsDefinition
{
    public static IServiceCollection AddApprovalsServices(this IServiceCollection services)
    {
        // services
        services.AddScoped<IInvoiceApproverService, InvoiceApproverService>();

        // repositories
        services.AddScoped<IApproverRepository, ApproverRepository>();
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<ISchemeRepository, SchemeRepository>();
        services.AddScoped<ISchemeGradeApprovalRepository, SchemeGradeApprovalRepository>();

        return services;
    }
}