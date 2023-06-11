using EST.MIT.Approvals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EST.MIT.Approvals.Api.Tests.Data.Entities;

public class SchemeGradeApprovalEntityTests
{
    [Fact]
    public void SchemeGradeApprovalEntity_SetProperties_ShouldSetCorrectly()
    {
        var entity = new SchemeGradeApprovalEntity
        {
            Id = 1,
            SchemeGrade = new SchemeGradeEntity()
            {
                Grade = new GradeEntity()
                {
                    Id = 2,
                    Code = "G2",
                    Name = "Grade 2",
                    Description = "This is the description for Grade 2",
                },
                Scheme = new SchemeEntity()
                {
                    Id = 1,
                    Code = "S1",
                    Name = "Scheme 1",
                    Description = "This is the description for Scheme 1",
                }
            },
        };

        Assert.Equal(1, entity.Id);
        Assert.Equal(2, entity.SchemeGrade.Grade.Id);
        Assert.Equal("G2", entity.SchemeGrade.Grade.Code);
        Assert.Equal("Grade 2", entity.SchemeGrade.Grade.Name);
        Assert.Equal("This is the description for Grade 2", entity.SchemeGrade.Grade.Description);
        Assert.Equal(1, entity.SchemeGrade.Scheme.Id);
        Assert.Equal("S1", entity.SchemeGrade.Scheme.Code);
        Assert.Equal("Scheme 1", entity.SchemeGrade.Scheme.Name);
        Assert.Equal("This is the description for Scheme 1", entity.SchemeGrade.Scheme.Description);
        
    }

}