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
        app.MapPost("/approvals/approver/validate", ValidateApproverAsync)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK)
            .WithName("ValidateApprover");

        return app;
    }

    public static async Task<IResult> ValidateApproverAsync(IInvoiceApproverService invoiceApproverService, IValidator<ValidateApprover> validator, ValidateApprover validateApprover)
    {
        var validationResult = await validator.ValidateAsync(validateApprover);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await invoiceApproverService.ConfirmApproverForInvoiceBySchemeAsync(validateApprover.ApproverEmailAddress, validateApprover.Scheme);

        if (!response.IsSuccess)
        {
            return Results.BadRequest(response.Message);
        }

        return response.Data ? Results.Ok() : Results.NotFound();
    }
}
