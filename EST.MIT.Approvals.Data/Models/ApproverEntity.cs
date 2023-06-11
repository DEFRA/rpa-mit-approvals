using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public class ApproverEntity : BaseEntity
{
    public string EmailAddress { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;

    public ICollection<SchemeGradeEntity> SchemeGrades { get; set; } = default!;


    public ApproverEntity(string emailAddress, string firstName, string lastName)
    {
        EmailAddress = emailAddress;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public ApproverEntity()
    {
        
    }
}
