using DB_Test_API.Models;

namespace Db_test_project.Services
{
    // This service works as the Database lookup (mock service)
    public class AccountLookupService : IAccountLookupService
    {
        // 12 transactions (we should only get 10 latest
        private static List<Transaction> transactions = [
            new() { AccountId = 1, Amount = 1.1, Timestamp = new DateTime(2024, 1, 1)},
            new() { AccountId = 1, Amount = 2.1, Timestamp = new DateTime(2024, 1, 13)},
            new() { AccountId = 1, Amount = 404, Timestamp = new DateTime(2022, 1, 1)},// to old
            new() { AccountId = 1, Amount = 4.1, Timestamp = new DateTime(2024, 1, 19)}, // newest
            new() { AccountId = 1, Amount = 22.1, Timestamp = new DateTime(2024, 1, 12)},
            new() { AccountId = 1, Amount = 404, Timestamp = new DateTime(2020, 1, 1)}, // to old
            new() { AccountId = 1, Amount = 1131, Timestamp = new DateTime(2024, 1, 6)},
            new() { AccountId = 1, Amount = 146.1, Timestamp = new DateTime(2024, 1, 8)},
            new() { AccountId = 1, Amount = 1094.1, Timestamp = new DateTime(2024, 1, 9)},
            new() { AccountId = 1, Amount = 1949.1, Timestamp = new DateTime(2024, 1, 10)},
            new() { AccountId = 1, Amount = 109.1, Timestamp = new DateTime(2024, 1, 4)},
            new() { AccountId = 1, Amount = 1231.1, Timestamp = new DateTime(2024, 1, 4)},
        ];

        private static List<Account> accounts =
        [
            new Account { Id = 1, Balance = 100.5, CustomerId = 1, Transactions = transactions },
            new Account { Id = 2, Balance = 0, CustomerId = 2 },
        ];
        public Account? GetAccount(int id)
        {
            var account = accounts.Find(a => a.Id == id);
            return account ?? null;
        }

        public void CreateAccount(Account account)
        {
            account.Id = accounts.Count+1; // dummy code to increment id's
            accounts.Add(account);
        }
    }
}
