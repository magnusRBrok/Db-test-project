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
        private static List<Account> accounts = new List<Account>()
        {
            new Account { Id = 1, Balance = 100.5, CustomerId = 1 },
            new Account { Id = 2, Balance = 0, CustomerId = 2 },
        };
        public void CreateAccount(int customerId)
        {
            var newAccount = new Account()
            {
                Id = accounts.Count + 1,
                Balance = 0,
                CustomerId = customerId
            };
            accounts.Add(newAccount);
        }

        public Account? GetAccount(int accountId)
        {
           var account = accounts.Find(a => a.Id == accountId);
           return account ?? null;
        }
    }
}
