using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services.Interfaces;

namespace EST.MIT.Approvals.Api.Services;

public class InvoiceApproverService : IInvoiceApproverService
{
    private readonly ISchemeRepository _schemeRepository;
    private readonly IApproverRepository _approverRepository;
    private readonly ISchemeApprovalGradeRepository _schemeApprovalGradeRepository;
    private readonly ILogger<InvoiceApproverService> _logger;

    public InvoiceApproverService(
        ISchemeRepository schemeRepository,
        IApproverRepository approverRepository,
        ISchemeApprovalGradeRepository schemeApprovalGradeApproverRepository,
        ILogger<InvoiceApproverService> logger)
    {
        _schemeRepository = schemeRepository;
        _approverRepository = approverRepository;
        _schemeApprovalGradeRepository = schemeApprovalGradeApproverRepository;
        _logger = logger;
    }

    public async Task<ReturnResult<IEnumerable<InvoiceApprover>>> GetApproversForInvoiceBySchemeAndAmountAsync(string invoiceScheme, decimal invoiceAmount)
    {
        var returnValue = new ReturnResult<IEnumerable<InvoiceApprover>>();

        try
        {
            var scheme = await this._schemeRepository.GetByCodeAsync(invoiceScheme);

            if (scheme == null)
            {
                returnValue.Message = "Unable to find matching scheme";
                return returnValue;
            }


            var schemeApprovalGrades = await this._schemeApprovalGradeRepository.GetAllBySchemeAndApprovalLimit(scheme.Id, invoiceAmount);
            var schemeApprovalGradeEntities = schemeApprovalGrades.ToList();

            if (!schemeApprovalGradeEntities.Any())
            {
                returnValue.Message = "Unable to find matching scheme and approval grades";
                return returnValue;
            }

            var approvers = await this._approverRepository.GetApproversBySchemeAndGradeAsync(schemeApprovalGradeEntities.Select(x => x.SchemeGrade.Id));

            var approverEntities = approvers.ToList();
            if (!approverEntities.Any())
            {
                returnValue.Message = "Unable to find matching approvers";
                return returnValue;
            }

            returnValue.Data = approverEntities.Select(x => new InvoiceApprover()
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
