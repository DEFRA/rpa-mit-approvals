using Approvals.Api.Services;
using System.Diagnostics.CodeAnalysis;
using Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Api.Services.Interfaces;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

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
        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<ISchemeRepository, SchemeRepository>();
        services.AddScoped<ISchemeGradeApproverRepository, SchemeGradeApproverRepository>();

        return services;
    }
}