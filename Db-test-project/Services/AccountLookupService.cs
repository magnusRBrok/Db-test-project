using DB_Test_API.Models;

namespace Db_test_project.Services
{
    public class AccountLookupService : IAccountLookupService
    {
        private static List<Account> accounts =
        [
            new Account { Id = 1, Balance = 100.5, CustomerId = 1 },
            new Account { Id = 2, Balance = 0, CustomerId = 2 },
        ];
        public Account? GetAccount(int id)
        {
            var account = accounts.Find(a => a.Id == id);
            return account ?? null;
        }
    }
}
