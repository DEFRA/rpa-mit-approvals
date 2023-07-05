using Approvals.Api.Models;

namespace EST.MIT.Approvals.Api.Services.Interfaces;

public interface IInvoiceApproverService
{
    Task<ReturnResult<bool>> ConfirmApproverForInvoiceBySchemeAsync(string approverEmailAddress, string schemeCode);
}
