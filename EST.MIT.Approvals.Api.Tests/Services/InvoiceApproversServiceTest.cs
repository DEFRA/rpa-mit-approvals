using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services;
using EST.MIT.Approvals.Data.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace EST.MIT.Approvals.Api.Tests.Services;

public class InvoiceApproversServiceTest
{
    private readonly InvoiceApproverService _serviceToTest;

    private readonly Mock<ISchemeRepository> _schemeRepositoryMock;
    private readonly Mock<IApproverRepository> _approverRepositoryMock;
    private readonly Mock<ISchemeApprovalGradeRepository> _schemeGradeApprovalRepositoryMock;
    private readonly Mock<ILogger<InvoiceApproverService>> _loggerMock;

    private readonly SchemeEntity _scheme;
    private readonly GradeEntity _grade;
    private readonly SchemeGradeEntity _schemeGrade;
    private readonly ApproverEntity _approver;
    private readonly SchemeApprovalGradeEntity _schemeApprovalGrade;

    public InvoiceApproversServiceTest()
    {
        this._schemeRepositoryMock = new Mock<ISchemeRepository>();
        this._approverRepositoryMock = new Mock<IApproverRepository>();
        this._schemeGradeApprovalRepositoryMock = new Mock<ISchemeApprovalGradeRepository>();
        this._loggerMock = new Mock<ILogger<InvoiceApproverService>>();

        this._scheme = new SchemeEntity()
        {
            Id = 1,
            Code = "S1",
        };

        this._schemeRepositoryMock.Setup(repository => repository.GetByCodeAsync(It.IsAny<string>()))
            .ReturnsAsync(() => this._scheme);

        this._grade = new GradeEntity()
        {
            Id = 1,
            Code = "G1",
        };

        this._schemeGrade = new SchemeGradeEntity()
        {
            Id = 1,
            Grade = this._grade,
            Scheme = this._scheme,
        };

        this._approver = new ApproverEntity()
        {
            Id = 1,
            EmailAddress = "ApproverOne@defra.gov.uk",
            FirstName = "Approver",
            LastName = "One",
        };

        this._approverRepositoryMock.Setup(repository => repository.GetApproversBySchemeAndGradeAsync(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync(() => new List<ApproverEntity>() { this._approver });

        this._schemeApprovalGrade = new SchemeApprovalGradeEntity()
        {
            Id = 1,
            SchemeGrade = _schemeGrade
        };

        this._schemeGradeApprovalRepositoryMock.Setup(repository => repository.GetAllBySchemeAndApprovalLimit(It.IsAny<int>(), It.IsAny<decimal>()))
            .ReturnsAsync(() => new List<SchemeApprovalGradeEntity>() { this._schemeApprovalGrade });

        this._serviceToTest = new InvoiceApproverService(this._schemeRepositoryMock.Object, this._approverRepositoryMock.Object, this._schemeGradeApprovalRepositoryMock.Object, this._loggerMock.Object);
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
                LastName = "One"
            },
        };

        var result = await _serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        Assert.NotNull(result);

        Assert.True(result.IsSuccess);
        var returnedPayload = result.Data.ToList();

        Assert.NotNull(returnedPayload);
        Assert.Equal(expectedPayload.Count, returnedPayload?.Count);

        if (returnedPayload != null && returnedPayload.Count == 1)
        {
            Assert.Equal(expectedPayload[0].Id, returnedPayload[0]?.Id);
            Assert.Equal(expectedPayload[0].FirstName, returnedPayload[0]?.FirstName);
            Assert.Equal(expectedPayload[0].LastName, returnedPayload[0]?.LastName);
            Assert.Equal(expectedPayload[0].EmailAddress, returnedPayload[0]?.EmailAddress);
        }
        else
        {
            Assert.Fail("Expected returned payload to be not null and have items");
        }
    }

    [Fact]
    public async Task InvoiceApproversService_GetApproversForInvoiceBySchemeAndAmount_ShouldReturnFailure_OnException()
    {
        // Arrange
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;

        // Mock logger to throw an exception
        var schemeRepositoryMock = new Mock<ISchemeRepository>();
        schemeRepositoryMock.Setup(repository => repository.GetByCodeAsync(It.IsAny<string>()))
            .Throws(new Exception("Unit Test Exception"));

        var serviceToTest = new InvoiceApproverService(schemeRepositoryMock.Object, this._approverRepositoryMock.Object, this._schemeGradeApprovalRepositoryMock.Object, this._loggerMock.Object);

        // Act
        var result = await serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal("Unit Test Exception", result.Message);
    }

    [Fact]
    public async Task GetApproversForInvoiceBySchemeAndAmount_ShouldReturnFailure_WhenSchemeNotFound()
    {
        // Arrange
        var invoiceScheme = "XYZ";
        var invoiceAmount = 2000M;

        _schemeRepositoryMock.Setup(repository => repository.GetByCodeAsync(It.IsAny<string>()))
            .ReturnsAsync((SchemeEntity)null);

        // Act
        var result = await _serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal("Unable to find matching scheme", result.Message);
    }

    [Fact]
    public async Task GetApproversForInvoiceBySchemeAndAmount_ShouldReturnFailure_WhenNoSchemeSchemeApprovalGradesFound()
    {
        // Arrange
        var invoiceScheme = "XYZ";
        var invoiceAmount = 2000M;

        _schemeGradeApprovalRepositoryMock.Setup(repository => repository.GetAllBySchemeAndApprovalLimit(It.IsAny<int>(), It.IsAny<decimal>()))
            .ReturnsAsync(new List<SchemeApprovalGradeEntity>());

        // Act
        var result = await _serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal("Unable to find matching scheme and approval grades", result.Message);
    }


    [Fact]
    public async Task GetApproversForInvoiceBySchemeAndAmount_ShouldReturnFailure_WhenApproverNotFound()
    {
        // Arrange
        var invoiceScheme = "ABC";
        var invoiceAmount = 2000M;

        _approverRepositoryMock.Setup(repository => repository.GetApproversBySchemeAndGradeAsync(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync(new List<ApproverEntity>());

        // Act
        var result = await _serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal("Unable to find matching approvers", result.Message);
    }
}