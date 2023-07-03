using System.Diagnostics.CodeAnalysis;

namespace Approvals.Api.Models;

[ExcludeFromCodeCoverage]
public class ValidationSettings
{
    public string AllowedEmailDomains { get; init; } = default!;
}