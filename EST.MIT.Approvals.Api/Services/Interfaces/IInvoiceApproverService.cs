using Approvals.Api.Models;

namespace EST.MIT.Approvals.Api.Services.Interfaces;

public interface IInvoiceApproverService
{
    Task<ReturnResult<bool>> ConfirmApproverForInvoiceByApprovalGroupAsync(string approverEmailAddress, string schemeCode);
}
