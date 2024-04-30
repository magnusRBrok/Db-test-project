using DB_Test_API.Models;

namespace Db_test_project.Services;

public interface IPaymentService
{
    public Transaction? Deposit(int accountId, double amount);
    public Transaction? Withdraw(int accountId, double amount);
}
