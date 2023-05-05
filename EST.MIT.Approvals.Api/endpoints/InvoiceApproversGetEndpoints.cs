using System.Diagnostics.CodeAnalysis;

namespace Approvals.Api.Endpoints;

public static class InvoiceApproversGetEndpoints
{
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapInvoiceApproversGetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/approvals/invoiceapprovers", (string invoiceScheme, decimal invoiceAmount) => GetApproversForInvoiceBySchemeAndAmount(invoiceScheme, invoiceAmount))
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("GetApproversForInvoiceBySchemeAndAmount");

        return app;
    }

    public static IResult GetApproversForInvoiceBySchemeAndAmount(string invoiceScheme, decimal invoiceAmount)
    {
        return Results.Ok($"GET /approvals/invoiceapprovers called with invoiceScheme: {invoiceScheme} and invoiceAmount: {invoiceAmount}");
    }
}
