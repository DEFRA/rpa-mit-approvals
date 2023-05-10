using Approvals.Api.Data.Repositories;
using Approvals.Api.Models;

namespace Approvals.Api.Services;

public class InvoiceApproverService : IInvoiceApproverService
{
    private readonly IApproverRepository _approverRepository;
    private readonly ILogger<InvoiceApproverService> _logger;

    public InvoiceApproverService(IApproverRepository approverRepository, ILogger<InvoiceApproverService> logger)
    {
        _approverRepository = approverRepository;
        _logger = logger;
    }

    public async Task<ReturnResult<IEnumerable<InvoiceApprover>>> GetApproversForInvoiceBySchemeAndAmountAsync(string invoiceScheme, decimal invoiceAmount)
    {
        var returnValue = new ReturnResult<IEnumerable<InvoiceApprover>>();

        try
        {
            var approvers = await this._approverRepository.GetApproversByGradeAsync(0);
            returnValue.Data = approvers.Select(x => new InvoiceApprover()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailAddress = x.EmailAddress,
            });
            returnValue.IsSuccess = true;
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "Unable to get approvers for invoice");
            returnValue.Message = exception.Message;
        }

        return returnValue;
    }
}
