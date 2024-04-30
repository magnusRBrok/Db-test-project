using DB_Test_API.Models;

namespace DB_Test_API.Services;

public interface IAccountService
{
    public double? GetAccountBalance(int accountId);
    public IEnumerable<Transaction> GetTransactions(int AccountId);
    public void CreateAccount (int customerId);
}
