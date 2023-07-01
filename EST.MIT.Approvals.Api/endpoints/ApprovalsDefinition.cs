using System.Diagnostics.CodeAnalysis;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services;
using EST.MIT.Approvals.Api.Services.Interfaces;
using EST.MIT.Approvals.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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
        services.AddScoped<ISchemeApprovalGradeRepository, SchemeApprovalGradeRepository>();

        // validators
        services.AddScoped<IValidator<ValidateApprover>, ValidateApproverValidator>();

        return services;
    }
}