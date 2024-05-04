
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
            return CreateTransaction(account, Math.Abs(amount)); ;
        }

        public Transaction? Withdraw(int accountId, double amount)
        {
            var account = _accountLookupService.GetAccount(accountId);
            if (account == null)
            {
                return null;
            }
            account.Balance -= Math.Abs(amount);
            return CreateTransaction(account, Math.Abs(amount) * (-1));;
        }

        private Transaction CreateTransaction(Account account, double amount)
        {
            // Add transaction to Database before return
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                AccountId = account.Id,
                Timestamp = DateTime.UtcNow,
            };
            if (account.Transactions != null)
            {
                account.Transactions = account.Transactions.Append(transaction);
            }
            else
            {
                account.Transactions = [transaction];
            }
            return transaction;

        }

        public List<Transaction>? GetTenLatestTransactions(int accountId)
        {
            var account = _accountLookupService.GetAccount(accountId);
            if (account == null)
            {
                return null;
            }
            if (account.Transactions == null)
            {
                return new List<Transaction> {};
            }
            var transactions = account.Transactions?.ToList();
            transactions?.Sort((x, y) => DateTime.Compare(y.Timestamp, x.Timestamp));
            return transactions?.Count > 10 ? transactions.Slice(0, 10) : transactions;

        }
    }
}
