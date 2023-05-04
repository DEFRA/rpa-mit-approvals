using Approvals.Api.Endpoints;
using Approvals.Api.Test;
using FluentAssertions;

namespace Approvals.Api.Tests.Endpoints;

public class HealthCheckGetEndpointsTest
{
    [Fact]
    public void HealthCheckGetEndpoint_Ping_ShouldReturn200AndMessage()
    {
        var result = HealthCheckGetEndpoints.HealthCheckPing();

        Assert.NotNull(result);
        
        // Assert that the status code is 200
        result.GetOkObjectResultStatusCode().Should().Be(200);

        // Assert that the result is an OkObjectResult that contains the expected message
        var expectedMessage = $"Approvals.Api Endpoint up and running @";
        var actualMessage = result.GetOkObjectResultValue<string>();
        Assert.Contains(expectedMessage, actualMessage);
    }
}