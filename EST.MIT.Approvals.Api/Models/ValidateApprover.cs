using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Approvals.Api.Models;

[ExcludeFromCodeCoverage]
public class ValidateApprover
{
    [JsonProperty("approvalGroup")]
    public string ApprovalGroup { get; init; } = default!;

    [JsonProperty("approverEmailAddress")]
    public string ApproverEmailAddress { get; init; } = default!;
}