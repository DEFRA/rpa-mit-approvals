using Newtonsoft.Json;

namespace Approvals.Api.Models;

public class InvoiceApprover
{
    [JsonProperty("id")]
    public string Id { get; init; } = default!;
    
    [JsonProperty("emailAddress")]
    public string EmailAddress { get; init; } = default!;
}
