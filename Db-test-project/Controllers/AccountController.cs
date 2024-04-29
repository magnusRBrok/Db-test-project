using DB_Test_API.Services;
using Db_test_project.DTOs.Create;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace DB_Test_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet(Name = "GetAccountBalance")]
    public double? GetAccountBalance(int accountId)
    {
        if (accountId == 0)
        {
            return null;
        }
        return _accountService.GetAccount(accountId)?.Balance ?? null;
    }

    [HttpGet(Name = "GetLatestTransactions")]
    public IEnumerable<Transaction> GetLastestTransactions(int accountId)
    {
        if (accountId == 0)
        {
            return [];
        }
        return null;
        //return _accountService.GetTransactions(accountId)?.Balance ?? null;
    }

    [HttpGet(Name = "GetLatestTransactions")]
    public IActionResult CreateAccount(CreateAccountDto accountDto)
    {
        if (accountDto == null || accountDto.CustomerId == 0)
        {
            return BadRequest();
        }
        return null;
        //return _accountService.GetTransactions(accountId)?.Balance ?? null;
    }

}
