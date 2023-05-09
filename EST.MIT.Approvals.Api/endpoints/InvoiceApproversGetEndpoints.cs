using System.Diagnostics.CodeAnalysis;
using Approvals.Api.Models;
using Approvals.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Approvals.Api.Endpoints;

public static class InvoiceApproversGetEndpoints
{
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapInvoiceApproversGetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/approvals/invoiceapprovers", ([FromServices] IInvoiceApproverService invoiceApproverService, string invoiceScheme, decimal invoiceAmount) => GetApproversForInvoiceBySchemeAndAmountAsync(invoiceApproverService, invoiceScheme, invoiceAmount))
            .Produces<IEnumerable<InvoiceApprover>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("GetApproversForInvoiceBySchemeAndAmount");

        return app;
    }

    public static async Task<IResult> GetApproversForInvoiceBySchemeAndAmountAsync(IInvoiceApproverService invoiceApproverService, string invoiceScheme, decimal invoiceAmount)
    {
        var response = await invoiceApproverService.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        if (!response.IsSuccess)
        {
            return Results.BadRequest(response.Message);
        }

        if (!response.Data.Any())
        {
            return Results.NotFound();
        }

        return Results.Ok(response.Data);
    }
}
