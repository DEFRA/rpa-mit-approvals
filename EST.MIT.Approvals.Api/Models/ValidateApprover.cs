using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Approvals.Api.Models;

[ExcludeFromCodeCoverage]
public class ValidateApprover
{
    [JsonProperty("scheme")]
    public string Scheme { get; init; } = default!;

    [JsonProperty("approverEmailAddress")]
    public string ApproverEmailAddress { get; init; } = default!;
}