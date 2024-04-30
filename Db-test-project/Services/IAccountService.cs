namespace DB_Test_API.Services;

public interface IAccountService
{
    public double? GetAccountBalance(int accountId);
    public void CreateAccount (int customerId);
}
