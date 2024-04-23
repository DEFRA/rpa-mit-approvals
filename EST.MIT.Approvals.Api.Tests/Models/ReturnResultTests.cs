using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Approvals.Api.Models;
using Xunit;

namespace EST.MIT.Approvals.Api.Tests.Models;

public class ReturnResultTests
{
    [Fact]
    public void ReturnResultT_Properties_ShouldSetAndGetValues()
    {
        var result = new ReturnResult<string>
        {
            IsSuccess = true,
            Message = "Test message",
            Data = "Test data"
        };

        Assert.True(result.IsSuccess);
        Assert.Equal("Test message", result.Message);
        Assert.Equal("Test data", result.Data);
    }

    [Fact]
    public void ReturnResult_Properties_ShouldSetAndGetValues()
    {
        var result = new ReturnResult
        {
            IsSuccess = true,
            Message = "Test message"
        };

        Assert.True(result.IsSuccess);
        Assert.Equal("Test message", result.Message);
    }
}