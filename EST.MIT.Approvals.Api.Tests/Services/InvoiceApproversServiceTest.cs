using Approvals.Api.Data.Entities;
using Approvals.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;

namespace Approvals.Api.Tests.Services;

public class InvoiceApproversServiceTest
{
    private readonly InvoiceApproverService _serviceToTest;

    private readonly Mock<ISchemeRepository> _schemeRepositoryMock;
    private readonly Mock<IGradeRepository> _gradeRepositoryMock;
    private readonly Mock<IApproverRepository> _approverRepositoryMock;
    private readonly Mock<ISchemeGradeApproverRepository> _schemeGradeApproverRepositoryMock;
    private readonly Mock<ILogger<InvoiceApproverService>> _loggerMock;

    private readonly SchemeEntity _scheme;
    private readonly GradeEntity _grade;
    private readonly ApproverEntity _approver;
    private readonly SchemeGradeApproverEntity _schemeGradeApprover;

    public InvoiceApproversServiceTest()
    {
        this._schemeRepositoryMock = new Mock<ISchemeRepository>();
        this._gradeRepositoryMock = new Mock<IGradeRepository>();
        this._approverRepositoryMock = new Mock<IApproverRepository>();
        this._schemeGradeApproverRepositoryMock = new Mock<ISchemeGradeApproverRepository>();
        this._loggerMock = new Mock<ILogger<InvoiceApproverService>>();

        this._scheme = new SchemeEntity()
        {
            Id = 1,
            Code = "A1",
        };

        this._schemeRepositoryMock.Setup(repository => repository.GetByCodeAsync(It.IsAny<string>()))
            .ReturnsAsync(() => this._scheme);

        this._grade = new GradeEntity()
        {
            Id = 1,
            ApprovalLimit = 1000,
        };

        this._gradeRepositoryMock.Setup(repository => repository.GetByApprovalLimit(It.IsAny<decimal>()))
            .ReturnsAsync(() => this._grade);

        this._approver = new ApproverEntity()
        {
            Id = 1,
            EmailAddress = "ApproverOne@defra.gov.uk",
            FirstName = "Approver",
            LastName = "One",
        };

        this._approverRepositoryMock.Setup(repository => repository.GetApproversByIdsAsync(It.IsAny<IEnumerable<int>>()))
            .ReturnsAsync(() => new List<ApproverEntity>() { this._approver });

        this._schemeGradeApprover = new SchemeGradeApproverEntity()
        {
            Id = 1,
            SchemeId = 1,
            GradeId = 1,
            ApproverId = 1,
        };

        this._schemeGradeApproverRepositoryMock.Setup(repository => repository.GetAllBySchemeAndGrade(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(() => new List<SchemeGradeApproverEntity>() { this._schemeGradeApprover });

        this._serviceToTest = new InvoiceApproverService(this._schemeRepositoryMock.Object, this._gradeRepositoryMock.Object, this._approverRepositoryMock.Object, this._schemeGradeApproverRepositoryMock.Object, this._loggerMock.Object);
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

        var serviceToTest = new InvoiceApproverService(schemeRepositoryMock.Object, this._gradeRepositoryMock.Object, this._approverRepositoryMock.Object, this._schemeGradeApproverRepositoryMock.Object, this._loggerMock.Object);

        // Act
        var result = await serviceToTest.GetApproversForInvoiceBySchemeAndAmountAsync(invoiceScheme, invoiceAmount);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Equal("Unit Test Exception", result.Message);
    }




}