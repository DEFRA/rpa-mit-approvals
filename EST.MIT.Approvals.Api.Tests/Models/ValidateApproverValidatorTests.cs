using Approvals.Api.Models;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;

namespace EST.MIT.Approvals.Api.Tests.Models;

public class ValidateApproverValidatorTests
{
    private readonly ValidateApproverValidator _validator;

    public ValidateApproverValidatorTests()
    {
        var settings = new ValidationSettings { AllowedEmailDomains = "domain1.co.uk|domain2.co.uk" };
        var options = Options.Create(settings);
        _validator = new ValidateApproverValidator(options);
    }

    [Fact]
    public void ShouldHaveErrorWhenSchemeIsEmpty()
    {
        var model = new ValidateApprover { Scheme = string.Empty, ApproverEmailAddress = "test@domain1.co.uk" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Scheme);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenSchemeIsSpecified()
    {
        var model = new ValidateApprover { Scheme = "scheme", ApproverEmailAddress = "test@domain1.co.uk" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Scheme);
    }

    [Fact]
    public void ShouldHaveErrorWhenApproverEmailAddressIsEmpty()
    {
        var model = new ValidateApprover { Scheme = "scheme", ApproverEmailAddress = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ApproverEmailAddress);
    }

    [Fact]
    public void ShouldHaveErrorWhenApproverEmailAddressIsNotValid()
    {
        var model = new ValidateApprover { Scheme = "scheme", ApproverEmailAddress = "notavalidemail" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ApproverEmailAddress);
    }

    [Fact]
    public void ShouldHaveErrorWhenApproverEmailAddressIsNotInAllowedDomains()
    {
        var model = new ValidateApprover { Scheme = "scheme", ApproverEmailAddress = "test@otherdomain.com" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ApproverEmailAddress);
    }

    [Fact]
    public void ShouldNotHaveErrorWhenApproverEmailAddressIsInAllowedDomains()
    {
        var model = new ValidateApprover { Scheme = "scheme", ApproverEmailAddress = "test@domain1.co.uk" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ApproverEmailAddress);
    }
}