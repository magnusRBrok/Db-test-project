using DB_Test_API.Models;
using DB_Test_API.Services;

namespace Db_test_project.Services
{
    public class AccountService : IAccountService
    {
        private static List<Account> accounts = new List<Account>()
        {
            new Account { Id = 1, Balance = 100.5, CustomerId = 1 },
            new Account { Id = 2, Balance = 0, CustomerId = 2 },
        };
        public void CreateAccount(Account account)
        {

            throw new NotImplementedException();
        }

        public Account? GetAccount(int accountId)
        {
           var account = accounts.Find(a => a.Id == accountId);
           return account ?? null;
        }
    }
}
