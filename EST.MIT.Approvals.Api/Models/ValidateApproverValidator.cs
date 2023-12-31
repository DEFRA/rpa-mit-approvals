﻿using FluentValidation;
using Microsoft.Extensions.Options;

namespace Approvals.Api.Models;

public class ValidateApproverValidator : AbstractValidator<ValidateApprover>
{
    public ValidateApproverValidator(IOptions<ValidationSettings> validationSettings)
    {
        RuleFor(x => x.ApprovalGroup).NotEmpty();

        var domains = validationSettings.Value.AllowedEmailDomains;
        var escapedDomains = domains.Replace(".", "\\.");
        var pattern = @"^[a-z0-9._%+-]+@(" + escapedDomains + ")$";

        RuleFor(x => x.ApproverEmailAddress).NotEmpty().EmailAddress().Matches(pattern);
    }
}