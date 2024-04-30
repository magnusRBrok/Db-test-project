using DB_Test_API.Models;
using DB_Test_API.Services;

namespace Db_test_project.Services
{
    /// <summary>
    /// This service would usually use a Database ClientService to work with the underlying data
    /// For simplicity the service now works with simple in-memory data, but could easily be updated to call database instead
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountLookupService _accountLookupService;
        public AccountService(IAccountLookupService accountLookupService)
        {
            _accountLookupService = accountLookupService;
        }

        public void CreateAccount(int customerId)
        {
            //var newAccount = new Account()
            //{
            //    Id = MRa,
            //    Balance = 0,
            //    CustomerId = customerId
            //};
            //accounts.Add(newAccount);
        }

        public double? GetAccountBalance(int accountId)
        {
            return _accountLookupService.GetAccount(accountId)?.Balance ?? null;
        }

        public IEnumerable<Transaction> GetTransactions(int AccountId)
        {
            throw new NotImplementedException();
        }
    }
}
