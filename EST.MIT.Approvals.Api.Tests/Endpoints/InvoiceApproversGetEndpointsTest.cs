using Approvals.Api.Endpoints;
using Approvals.Api.Test;
using FluentAssertions;

namespace Approvals.Api.Tests.Endpoints;

public class InvoiceApproversGetEndpointsTest
{
    [Fact]
    public void InvoiceApproversGetEndpoint_InvoiceApprovers_ShouldReturn200AndMessage()
    {
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;
        var result = InvoiceApproversGetEndpoints.GetApproversForInvoiceBySchemeAndAmount(invoiceScheme, invoiceAmount);

        Assert.NotNull(result);

        // Assert that the status code is 200
        result.GetOkObjectResultStatusCode().Should().Be(200);

        // Assert that the result is an OkObjectResult that contains the expected message
        var expectedMessage = $"GET /approvals/invoiceapprovers called with invoiceScheme: {invoiceScheme} and invoiceAmount: {invoiceAmount}";
        var actualMessage = result.GetOkObjectResultValue<string>();
        Assert.Equal(expectedMessage, actualMessage);
    }
}