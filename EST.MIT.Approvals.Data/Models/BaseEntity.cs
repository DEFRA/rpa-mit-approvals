using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data.Models;

[ExcludeFromCodeCoverage]
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        this.CreatedOn = DateTime.UtcNow;
        this.IsDeleted = false;
    }

    [Key]
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }
}
