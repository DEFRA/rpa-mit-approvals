using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class ApproverEntity : BaseEntity
{
    public string EmailAddress { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;

    public List<SchemeEntity> Schemes { get; set; } = new();

    public ApproverEntity(string emailAddress, string firstName, string lastName)
    {
        this.EmailAddress = emailAddress;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public ApproverEntity()
    {
    }
}
