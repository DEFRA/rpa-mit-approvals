using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;

namespace Approvals.Api.Endpoints;

[ExcludeFromCodeCoverage]
public static class SwaggerDefinition
{
    public static void SwaggerEndpoints(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "ManualInvoiceTemplatesApprovalsApi", Version = "v1", Description = "Approvals api for manual invoice templates" }));
    }
}