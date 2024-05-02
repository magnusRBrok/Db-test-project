using DB_Test_API.Models;
using DB_Test_API.Services;
using Db_test_project.DTOs.Requests.Create;
using Db_test_project.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DB_Test_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
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

    [HttpPost("CreateAccount")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CreateAccountResponse> CreateAccount(CreateAccountDto accountDto)
    {
        if (accountDto == null || accountDto.CustomerId == 0) // invalid request
        {
            return BadRequest();
        }
        var createdAccount = _accountService.CreateAccount(accountDto.CustomerId);
        if (createdAccount == null)
        {
            return NotFound(); // customer was not found in DB
        }
        return Ok(new CreateAccountResponse // customer was created
        {
            Balance = createdAccount.Balance,
            CustomerId = createdAccount.CustomerId,
            Id = createdAccount.Id,
        });
    }

}
