using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class ApprovalGroupEntityTests
{
    [Fact]
    public void SchemeEntity_SetProperties_ShouldSetCorrectly()
    {
        var entity = new ApprovalGroupEntity
        {
            Code = "AP1"
        };

        Assert.Equal("AP1", entity.Code);
    }

}