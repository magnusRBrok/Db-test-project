
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

        public PaymentService(IAccountLookupService accountLookupService)
        {
            _accountLookupService = accountLookupService;
        }

        public Transaction? Deposit(int accountId, double amount)
        {
            var account = _accountLookupService.GetAccount(accountId);
            if (account == null)
            {
                return null;
            }
            account.Balance += Math.Abs(amount);
            return CreateTransaction(accountId, Math.Abs(amount));
        }

        public Transaction? Withdraw(int accountId, double amount)
        {
            var account = _accountLookupService.GetAccount(accountId);
            if (account == null)
            {
                return null;
            }
            account.Balance -= Math.Abs(amount);
            return CreateTransaction(accountId, Math.Abs(amount)*(-1));
        }

        private Transaction CreateTransaction(int accountId, double amount)
        {
            // Add transaction to Database before return
            return new Transaction()
            {
                Id = new Guid(),
                Amount = amount,
                AccountId = accountId,
                Timestamp = DateTime.UtcNow,
            };
        }
    }
}
