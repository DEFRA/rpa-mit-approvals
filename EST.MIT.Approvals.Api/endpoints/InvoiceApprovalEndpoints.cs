using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Approvals.Api.Endpoints;

public static class InvoiceApprovalEndpoints
{
    [ExcludeFromCodeCoverage]
    public static IEndpointRouteBuilder MapInvoiceApprovalsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/approvals/invoiceapprovers", ([FromServices] IInvoiceApproverService invoiceApproverService, string invoiceScheme, decimal invoiceAmount) => GetApproversForInvoiceBySchemeAndAmountAsync(invoiceApproverService, invoiceScheme, invoiceAmount))
            .Produces<IEnumerable<InvoiceApprover>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("GetApproversForInvoiceBySchemeAndAmount");

        app.MapPost("/approvals/approver/validate", ValidateApproverAsync)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status200OK)
            .WithName("ValidateApprover");

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

    public static async Task<IResult> ValidateApproverAsync(IInvoiceApproverService invoiceApproverService, IValidator<ValidateApprover> validator, ValidateApprover validateApprover)
    {
        var validationResult = await validator.ValidateAsync(validateApprover);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        return Results.Ok();
    }

}
