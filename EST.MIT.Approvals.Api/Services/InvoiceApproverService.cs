using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Data.Repositories.Interfaces;
using EST.MIT.Approvals.Api.Services.Interfaces;
using System;

namespace Approvals.Api.Services;

public class InvoiceApproverService : IInvoiceApproverService
{
    private readonly ISchemeRepository _schemeRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IApproverRepository _approverRepository;
    private readonly ISchemeGradeApproverRepository _schemeGradeApproverRepository;
    private readonly ILogger<InvoiceApproverService> _logger;

    public InvoiceApproverService(
        ISchemeRepository schemeRepository,
        IGradeRepository gradeRepository,
        IApproverRepository approverRepository,
        ISchemeGradeApproverRepository schemeGradeApproverRepository,
        ILogger<InvoiceApproverService> logger)
    {
        _schemeRepository = schemeRepository;
        _gradeRepository = gradeRepository;
        _approverRepository = approverRepository;
        _schemeGradeApproverRepository = schemeGradeApproverRepository;
        _logger = logger;
    }

    public async Task<ReturnResult<IEnumerable<InvoiceApprover>>> GetApproversForInvoiceBySchemeAndAmountAsync(string invoiceScheme, decimal invoiceAmount)
    {
        var returnValue = new ReturnResult<IEnumerable<InvoiceApprover>>();

        try
        {
            var scheme = await this._schemeRepository.GetByCodeAsync(invoiceScheme);
            var grade = await this._gradeRepository.GetByApprovalLimit(invoiceAmount);

            if (scheme == null)
            {
                returnValue.Message = "Unable to find matching scheme";
                return returnValue;
            }

            if (grade == null)
            {
                returnValue.Message = "Unable to find matching grade";
                return returnValue;
            }

            var schemeGradeApprovers = await this._schemeGradeApproverRepository.GetAllBySchemeAndGrade(scheme.Id, grade.Id);
            var schemeGradeApproverEntities = schemeGradeApprovers.ToList();

            if (!schemeGradeApproverEntities.Any())
            {
                returnValue.Message = "Unable to find matching scheme and grade approvers";
                return returnValue;
            }

            var approvers = await this._approverRepository.GetApproversByIdsAsync(schemeGradeApproverEntities.Select(x => x.ApproverId));

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
