using Approvals.Api.Models;

namespace Approvals.Api.Services;

public class InvoiceApproverService : IInvoiceApproverService
{
    private readonly ILogger<InvoiceApproverService> _logger;

    public InvoiceApproverService(ILogger<InvoiceApproverService> logger)
    {
        _logger = logger;
    }

    public async Task<ReturnResult<IEnumerable<InvoiceApprover>>> GetApproversForInvoiceBySchemeAndAmountAsync(string invoiceScheme, decimal invoiceAmount)
    {
        var returnValue = new ReturnResult<IEnumerable<InvoiceApprover>>();

        try
        {

            returnValue.Data = new List<InvoiceApprover>()
            {
                new InvoiceApprover()
                {
                    Id = 1,
                    EmailAddress = "ApproverOne@defra.gov.uk",
                    FirstName = "Approver",
                    LastName = "One,"
                },
                new InvoiceApprover()
                {
                    Id = 2,
                    EmailAddress = "ApproverTwo@defra.gov.uk",
                    FirstName = "Approver",
                    LastName = "Two,"
                },
                new InvoiceApprover()
                {
                    Id = 1,
                    EmailAddress = "ApproverThree@defra.gov.uk",
                    FirstName = "Approver",
                    LastName = "Three,"
                }
            };
            returnValue.IsSuccess = true;
        }
        catch (Exception exception)
        {
            this._logger.LogError(exception, exception.Message);
            returnValue.Message = exception.Message;
        }

        return returnValue;
    }
}
