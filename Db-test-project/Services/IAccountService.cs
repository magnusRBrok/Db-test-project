using DB_Test_API.Models;

namespace DB_Test_API.Services;

public interface IAccountService
{
    public double? GetAccountBalance(int accountId);
    public List<Transaction>? GetTransactions(int accountId);
    public Account? CreateAccount (int customerId);
}
