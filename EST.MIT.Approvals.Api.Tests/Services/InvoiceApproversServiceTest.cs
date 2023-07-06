using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services;
using EST.MIT.Approvals.Data.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace EST.MIT.Approvals.Api.Tests.Services;

public class InvoiceApproversServiceTest
{
    private readonly InvoiceApproverService _serviceToTest;

    private readonly Mock<IApproverRepository> _approverRepositoryMock;
    private readonly Mock<ILogger<InvoiceApproverService>> _loggerMock;

    private readonly SchemeEntity _scheme;
    private readonly ApproverEntity _approver;

    public InvoiceApproversServiceTest()
    {
        this._approverRepositoryMock = new Mock<IApproverRepository>();
        this._loggerMock = new Mock<ILogger<InvoiceApproverService>>();

        this._scheme = new SchemeEntity()
        {
            Id = 1,
            Code = "S1",
        };


        this._approver = new ApproverEntity()
        {
            Id = 1,
            EmailAddress = "ApproverOne@defra.gov.uk",
            FirstName = "Approver",
            LastName = "One",
            Schemes =
            {
                this._scheme
            }
        };

        this._approverRepositoryMock.Setup(repository => repository.GetApproverByEmailAddressAndSchemeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(() => this._approver);

        this._serviceToTest = new InvoiceApproverService(this._approverRepositoryMock.Object, this._loggerMock.Object);
    }

    [Fact]
    public async Task InvoiceApproversService_ConfirmApproverForInvoiceBySchemeAsync_ShouldReturnSuccessAndPayload()
    {
        var invoiceScheme = "ABC";
        var invoiceApprover = "ApproverOne@defra.gov.uk";

        var result = await _serviceToTest.ConfirmApproverForInvoiceBySchemeAsync(invoiceApprover, invoiceScheme);

        Assert.NotNull(result);

        Assert.True(result.IsSuccess);
        var returnedPayload = result.Data;

        Assert.True(returnedPayload);
    }

    [Fact]
    public async Task InvoiceApproversService_ConfirmApproverForInvoiceBySchemeAsync_ShouldReturnFailure_WhenApproverEmailAddressIsEmpty()
    {
        // Arrange
        var invoiceScheme = "ABC";
        var invoiceApprover = "";

        var serviceToTest = new InvoiceApproverService(this._approverRepositoryMock.Object, this._loggerMock.Object);

        // Act
        var result = await serviceToTest.ConfirmApproverForInvoiceBySchemeAsync(invoiceApprover, invoiceScheme);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.False(result.Data);
        Assert.Equal("Approver is required", result.Message);
    }

    [Fact]
    public async Task InvoiceApproversService_ConfirmApproverForInvoiceBySchemeAsync_ShouldReturnFailure_WhenSchemeIsEmpty()
    {
        // Arrange
        var invoiceScheme = "";
        var invoiceApprover = "ApproverOne@defra.gov.uk";

        var serviceToTest = new InvoiceApproverService(this._approverRepositoryMock.Object, this._loggerMock.Object);

        // Act
        var result = await serviceToTest.ConfirmApproverForInvoiceBySchemeAsync(invoiceApprover, invoiceScheme);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.False(result.Data);
        Assert.Equal("Scheme is required", result.Message);
    }

    [Fact]
    public async Task InvoiceApproversService_ConfirmApproverForInvoiceBySchemeAsync_ShouldReturnFailure_OnException()
    {
        // Arrange
        var invoiceScheme = "ABC";
        var invoiceApprover = "ApproverOne@defra.gov.uk";

        // Mock logger to throw an exception
        var approverRepositoryMock = new Mock<IApproverRepository>();
        approverRepositoryMock.Setup(repository => repository.GetApproverByEmailAddressAndSchemeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception("Unit Test Exception"));

        var serviceToTest = new InvoiceApproverService(approverRepositoryMock.Object, this._loggerMock.Object);

        // Act
        var result = await serviceToTest.ConfirmApproverForInvoiceBySchemeAsync(invoiceApprover, invoiceScheme);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.False(result.Data);
        Assert.Equal("Unit Test Exception", result.Message);
    }

    [Fact]
    public async Task InvoiceApproverService_ConfirmApproverForInvoiceBySchemeAsync_ShouldReturnNoData_WhenApproverNotFound()
    {
        // Arrange
        var invoiceScheme = "ABC";
        var invoiceApprover = "ApproverOne@defra.gov.uk";

        _approverRepositoryMock.Setup(repository => repository.GetApproverByEmailAddressAndSchemeAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(() => null);

        // Act
        var result = await _serviceToTest.ConfirmApproverForInvoiceBySchemeAsync(invoiceApprover, invoiceScheme);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.Data);
    }
}