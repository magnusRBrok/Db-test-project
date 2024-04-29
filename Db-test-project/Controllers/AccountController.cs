using DB_Test_API.Services;
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

    [HttpGet(Name = "GetAccountBalance")]
    public double GetAccountBalance(int id)
    {
        return 0;
    }
}
