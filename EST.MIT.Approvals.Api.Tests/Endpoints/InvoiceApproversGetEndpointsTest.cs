using Approvals.Api.Endpoints;
using Approvals.Api.Models;
using Approvals.Api.Services;
using Approvals.Api.Test;
using FluentAssertions;
using NSubstitute;

namespace Approvals.Api.Tests.Endpoints;

public class InvoiceApproversGetEndpointsTest
{
    private readonly IInvoiceApproverService _invoiceApproverServiceMock =
        Substitute.For<IInvoiceApproverService>();

    [Fact]
    public async Task InvoiceApproversGetEndpoint_GetApproversForInvoiceBySchemeAndAmount_ShouldReturn200AndPayload()
    {
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;

        var expectedApprover = new InvoiceApprover()
        {
            Id = 1,
            FirstName = "UnitTest",
            LastName = "Approver",
            EmailAddress = "UnitTestApprover@defra.gov.uk"
        };

        this._invoiceApproverServiceMock.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount)
            .Returns(new ReturnResult<IEnumerable<InvoiceApprover>>()
            {
                Data = new List<InvoiceApprover>() { expectedApprover },
                IsSuccess = true,
            });

        var result = await InvoiceApproversGetEndpoints.GetApproversForInvoiceBySchemeAndAmountAsync(this._invoiceApproverServiceMock, invoiceScheme, invoiceAmount);

        Assert.NotNull(result);

        // Assert that the status code is 200
        result.GetOkObjectResultStatusCode().Should().Be(200);

        // Assert that the correct payload is coming back
        var returnedPayload = result.GetOkObjectResultValue<IEnumerable<InvoiceApprover>>() as List<InvoiceApprover>;

        Assert.NotNull(returnedPayload);
        Assert.Equal(1, returnedPayload?.Count());
        Assert.Equal(expectedApprover.Id, returnedPayload[0]?.Id);
        Assert.Equal(expectedApprover.FirstName, returnedPayload[0]?.FirstName);
        Assert.Equal(expectedApprover.LastName, returnedPayload[0]?.LastName);
        Assert.Equal(expectedApprover.EmailAddress, returnedPayload[0]?.EmailAddress);
    }

    [Fact]
    public async Task InvoiceApproversGetEndpoint_GetApproversForInvoiceBySchemeAndAmount_ShouldReturn404()
    {
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;

        this._invoiceApproverServiceMock.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount)
            .Returns(new ReturnResult<IEnumerable<InvoiceApprover>>()
            {
                Data = new List<InvoiceApprover>() { },
                IsSuccess = true,
            });

        var result = await InvoiceApproversGetEndpoints.GetApproversForInvoiceBySchemeAndAmountAsync(this._invoiceApproverServiceMock, invoiceScheme, invoiceAmount);

        Assert.NotNull(result);
        result.GetNotFoundResultStatusCode().Should().Be(404);
    }

    [Fact]
    public async Task InvoiceApproversGetEndpoint_GetApproversForInvoiceBySchemeAndAmount_ShouldReturn400AndMessage()
    {
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;

        var expectedMessage = "UnitTest: Oops something went wrong.";

        this._invoiceApproverServiceMock.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount)
            .Returns(new ReturnResult<IEnumerable<InvoiceApprover>>()
            {
                Message = expectedMessage,
                IsSuccess = false,
            });

        var result = await InvoiceApproversGetEndpoints.GetApproversForInvoiceBySchemeAndAmountAsync(this._invoiceApproverServiceMock, invoiceScheme, invoiceAmount);

        Assert.NotNull(result);

        result.GetBadRequestStatusCode().Should().Be(400);
        var actualMessage = result.GetBadRequestResultValue<string>();
        Assert.Contains(expectedMessage, actualMessage);
    }
}