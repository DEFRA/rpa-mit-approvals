using Approvals.Api.Models;

namespace Approvals.Api.Services;

public interface IInvoiceApproverService
{
    Task<ReturnResult<IEnumerable<InvoiceApprover>>> GetApproversForInvoiceBySchemeAndAmountAsync(string invoiceScheme, decimal invoiceAmount);
}
