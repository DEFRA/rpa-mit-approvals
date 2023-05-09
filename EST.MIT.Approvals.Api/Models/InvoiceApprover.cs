using Newtonsoft.Json;

namespace Approvals.Api.Models;

public class InvoiceApprover
{
    [JsonProperty("id")]
    public int Id { get; init; } = default!;

    [JsonProperty("emailAddress")]
    public string EmailAddress { get; init; } = default!;

    [JsonProperty("firstName")]
    public string FirstName { get; init; } = default!;

    [JsonProperty("lastName")]
    public string LastName { get; init; } = default!;
}
