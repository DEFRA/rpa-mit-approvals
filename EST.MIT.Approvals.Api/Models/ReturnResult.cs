namespace Approvals.Api.Models;

public class ReturnResult<T>
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = default!;

    public T Data { get; set; } = default!;
}

public class ReturnResult
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; } = default!;
}
