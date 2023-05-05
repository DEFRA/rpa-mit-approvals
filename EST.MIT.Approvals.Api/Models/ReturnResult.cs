namespace Approvals.Api.Models;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    public T Data { get; set; }
}

public class ReturnResult
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }
}
