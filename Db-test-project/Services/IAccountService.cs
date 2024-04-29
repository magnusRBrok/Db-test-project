using DB_Test_API.Models;

namespace DB_Test_API.Services;

public interface IAccountService
{
    public Account GetAccount(int accountId);

}
