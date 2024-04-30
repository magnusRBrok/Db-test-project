
using DB_Test_API.Models;

namespace Db_test_project.Services
{
    /// <summary>
    /// This service would usually use a Database ClientService to work with the underlying data
    /// For simplicity the service now works with simple in-memory data, but could easily be updated to call database instead
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IAccountLookupService _accountLookupService;

        Transaction? IPaymentService.Deposit(int accountId, double amount)
        {
            throw new NotImplementedException();
        }

        Transaction? IPaymentService.Withdraw(int accountId, double amount)
        {
            throw new NotImplementedException();
        }
    }
}
