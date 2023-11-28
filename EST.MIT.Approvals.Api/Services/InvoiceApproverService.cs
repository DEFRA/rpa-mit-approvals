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

    public async Task<ReturnResult<bool>> ConfirmApproverForInvoiceBySchemeAsync(string approverEmailAddress, string schemeCode)
    {
        var returnValue = new ReturnResult<bool>();

        try
        {
            if (string.IsNullOrWhiteSpace(approverEmailAddress))
            {
                returnValue.Message = "Approver is required";
                return returnValue;
            }

            if (string.IsNullOrWhiteSpace(schemeCode))
            {
                returnValue.Message = "Scheme is required";
                return returnValue;
            }

            var approver = await this._approverRepository.GetApproverByEmailAddressAndSchemeAsync(approverEmailAddress, schemeCode);

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
