using Approvals.Api.Endpoints;
using Approvals.Api.Test;
using FluentAssertions;

namespace Approvals.Api.Tests.Endpoints;

public class InvoiceApprovalPostEndpointsTest
{
    [Fact]
    public void InvoiceApprovalEndpoint_ValidateApprover_ShouldReturn200()
    {
        var result = InvoiceApprovalEndpoints.ValidateApprover();

        Assert.NotNull(result);

        // Assert that the status code is 200
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }
}