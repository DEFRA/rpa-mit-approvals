using Approvals.Api.Endpoints;
using Approvals.Api.Models;
using Approvals.Api.Test;
using EST.MIT.Approvals.Api.Services.Interfaces;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Approvals.Api.Tests.Endpoints;

public class InvoiceApprovalPostEndpointsTest
{
    private readonly IInvoiceApproverService _invoiceApproverServiceMock =
        Substitute.For<IInvoiceApproverService>();

    private readonly IOptions<ValidationSettings> _validationSettingsMock =
        Substitute.For<IOptions<ValidationSettings>>();

    private readonly IValidator<ValidateApprover> _validatorMock =
        Substitute.For<IValidator<ValidateApprover>>();

    public InvoiceApprovalPostEndpointsTest()
    {
        this._validationSettingsMock.Value
            .Returns(new ValidationSettings()
            {
                AllowedEmailDomains = "defra.gov.uk|rpa.gov.uk"
            });

        var validationSuccess = new ValidationResult();

       this._validatorMock.ValidateAsync(Arg.Any<ValidateApprover>(), default)
           .Returns(Task.FromResult(validationSuccess));
    }

    [Fact]
    public async Task InvoiceApprovalEndpoint_ValidateApprover_ShouldReturn200()
    {
        var payload = new ValidateApprover()
        {
            Scheme = "SC",
            ApproverEmailAddress = "unittest@defra.gov.uk"
        };

        var result = await InvoiceApprovalEndpoints.ValidateApproverAsync(this._invoiceApproverServiceMock, this._validatorMock, payload);

        Assert.NotNull(result);

        // Assert that the status code is 200
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async Task InvoiceApprovalEndpoint_ValidateApprover_ShouldReturnBadRequest()
    {
        var validationFailure = new ValidationFailure("ApproverEmailAddress", "Error message for ApproverEmailAddress");
        var validationFailures = new List<ValidationFailure> { validationFailure };
        var validationFailureResult = new ValidationResult(validationFailures);

        this._validatorMock.ValidateAsync(Arg.Any<ValidateApprover>(), default)
            .Returns(Task.FromResult(validationFailureResult));

        var payload = new ValidateApprover()
        {
            Scheme = "SC",
            ApproverEmailAddress = "unittest@defra.gov.uk"
        };

        var result = await InvoiceApprovalEndpoints.ValidateApproverAsync(this._invoiceApproverServiceMock, this._validatorMock, payload);

        Assert.NotNull(result);

        result.GetBadRequestResultValue<HttpValidationProblemDetails>().Should().NotBeNull();
        result?.GetBadRequestResultValue<HttpValidationProblemDetails>()?.Errors.Should().ContainKey("ApproverEmailAddress");
    }
}