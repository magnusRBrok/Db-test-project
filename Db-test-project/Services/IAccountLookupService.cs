using DB_Test_API.Models;

namespace Db_test_project.Services
{
    public interface IAccountLookupService
    {
        public Account? GetAccount(int id);
        public void CreateAccount(Account account);

    }
}
