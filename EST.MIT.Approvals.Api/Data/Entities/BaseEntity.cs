namespace Approvals.Api.Data.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        this.CreatedOn = DateTime.UtcNow;
        this.IsDeleted = false;
    }

    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int ModifiedByUserId { get; set; }

    public bool IsDeleted { get; set; }
}
