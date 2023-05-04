using System.Diagnostics.CodeAnalysis;

namespace Approvals.Api.Endpoints;

public static class HealthCheckGetEndpoints
{
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapHealthCheckGetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/approvals/healthcheck/ping", HealthCheckPing)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("HealthCheckPing");

        return app;
    }

    public static IResult HealthCheckPing()
    {
        return Results.Ok($"Approvals.Api Endpoint up and running @{DateTime.Now.ToString()}");
    }
}
