using DB_Test_API.Models;
using DB_Test_API.Services;
using Db_test_project.DTOs.Create;
using Db_test_project.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("GetBalance/{accountId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<double>? GetAccountBalance(int accountId)
    {
        var result = _accountService.GetAccountBalance(accountId);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet()]
    [Route("GetLatestTransactions/{accountId}")]
    public IEnumerable<Transaction> GetLastestTransactions(int accountId)
    {
        if (accountId == 0)
        {
            return [];
        }
        return null;
        //return _accountService.GetTransactions(accountId)?.Balance ?? null;
    }

    [HttpPost()]
    [Route("CreateAccount")]
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
