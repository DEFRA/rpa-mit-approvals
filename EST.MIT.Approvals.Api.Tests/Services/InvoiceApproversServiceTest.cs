using Approvals.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Approvals.Api.Models;

namespace Approvals.Api.Tests.Services;

public class InvoiceApproversServiceTest
{
    private readonly InvoiceApproverService _serviceToTest;
    private readonly Mock<ILogger<InvoiceApproverService>> _loggerMock;

    public InvoiceApproversServiceTest()
    {
        _loggerMock = new Mock<ILogger<InvoiceApproverService>>();

        this._serviceToTest = new InvoiceApproverService(_loggerMock.Object);
    }

    [Fact]
    public async Task InvoiceApproversService_GetApproversForInvoiceBySchemeAndAmount_ShouldReturnSuccessAndPayload()
    {
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;

        var expectedPayload = new List<InvoiceApprover>()
        {
            new InvoiceApprover()
            {
                Id = 1,
                EmailAddress = "ApproverOne@defra.gov.uk",
                FirstName = "Approver",
                LastName = "One,"
            },
            new InvoiceApprover()
            {
                Id = 2,
                EmailAddress = "ApproverTwo@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Two,"
            },
            new InvoiceApprover()
            {
                Id = 1,
                EmailAddress = "ApproverThree@defra.gov.uk",
                FirstName = "Approver",
                LastName = "Three,"
            }
        };

        var result = await _serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        Assert.NotNull(result);

        Assert.True(result.IsSuccess);
        var returnedPayload = result.Data.ToList();

        Assert.NotNull(returnedPayload);
        Assert.Equal(expectedPayload.Count, returnedPayload?.Count());
        Assert.Equal(expectedPayload[0].Id, returnedPayload[0]?.Id);
        Assert.Equal(expectedPayload[0].FirstName, returnedPayload[0]?.FirstName);
        Assert.Equal(expectedPayload[0].LastName, returnedPayload[0]?.LastName);
        Assert.Equal(expectedPayload[0].EmailAddress, returnedPayload[0]?.EmailAddress);
        Assert.Equal(expectedPayload[1].Id, returnedPayload[1].Id);
        Assert.Equal(expectedPayload[1].FirstName, returnedPayload[1]?.FirstName);
        Assert.Equal(expectedPayload[1].LastName, returnedPayload[1]?.LastName);
        Assert.Equal(expectedPayload[1].EmailAddress, returnedPayload[1]?.EmailAddress);
        Assert.Equal(expectedPayload[2].Id, returnedPayload[2].Id);
        Assert.Equal(expectedPayload[2].FirstName, returnedPayload[2]?.FirstName);
        Assert.Equal(expectedPayload[2].LastName, returnedPayload[2]?.LastName);
        Assert.Equal(expectedPayload[2].EmailAddress, returnedPayload[2]?.EmailAddress);
    }
}