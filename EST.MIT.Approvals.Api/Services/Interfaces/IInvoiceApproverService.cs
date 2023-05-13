using Approvals.Api.Models;

namespace EST.MIT.Approvals.Api.Services.Interfaces;

public interface IInvoiceApproverService
{
    Task<ReturnResult<IEnumerable<InvoiceApprover>>> GetApproversForInvoiceBySchemeAndAmountAsync(string invoiceScheme, decimal invoiceAmount);
}
