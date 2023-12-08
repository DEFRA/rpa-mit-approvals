using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services.Interfaces;

namespace EST.MIT.Approvals.Api.Services;

public class InvoiceApproverService : IInvoiceApproverService
{
    private readonly IApproverRepository _approverRepository;
    private readonly ILogger<InvoiceApproverService> _logger;

    public InvoiceApproverService(
        IApproverRepository approverRepository,
        ILogger<InvoiceApproverService> logger)
    {
        _approverRepository = approverRepository;
        _logger = logger;
    }

    public async Task<ReturnResult<bool>> ConfirmApproverForInvoiceByApprovalGroupAsync(string approverEmailAddress, string approvalGroupCode)
    {
        var returnValue = new ReturnResult<bool>();

        try
        {
            if (string.IsNullOrWhiteSpace(approverEmailAddress))
            {
                returnValue.Message = "Approver is required";
                return returnValue;
            }

            if (string.IsNullOrWhiteSpace(approvalGroupCode))
            {
                returnValue.Message = "Approval Group is required";
                return returnValue;
            }

            var approver = await this._approverRepository.GetApproverByEmailAddressAndApprovalGroupAsync(approverEmailAddress, approvalGroupCode);

            returnValue.Data = approver != null;
            returnValue.IsSuccess = true;
            return returnValue;
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, "Unable to confirm approver for invoice");
            returnValue.Message = exception.Message;
        }

        
        return returnValue;
    }
}
