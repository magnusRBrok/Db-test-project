using DB_Test_API.Controllers;
using DB_Test_API.Models;
using DB_Test_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace DB_Test_API.Tests;


[TestFixture]
class AccountControllerTest
{
    private AccountController _controller;
    private IAccountService _accountService;
    private ILogger<AccountController> _logger;

    [SetUp]
    public void Setup()
    {
        _accountService = Substitute.For<IAccountService>();
        _logger = Substitute.For<ILogger<AccountController>>();
        _controller = new AccountController(_logger, _accountService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public void GetAccountBalance_WithValidId_ShouldReturnBalance()
    {
        var expectedAmount = 100.5;
        _accountService.GetAccountBalance(1).Returns(expectedAmount);

        var response = _controller.GetAccountBalance(1);
        var result = (OkObjectResult)response.Result;

        Assert.That(response.Result, Is.TypeOf<OkObjectResult>());
        Assert.That(result.Value, Is.EqualTo(expectedAmount));
    }

    [Test]
    public void GetAccountBalance_WithInvalidId_ShouldReturn404()
    {
        _accountService.GetAccountBalance(1).Returns(100);
        var response = _controller.GetAccountBalance(0);
        
        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void GetLatestTransactions_WithValidId_ShouldReturnTransactions()
    {
        Transaction[] transactions = {
            new() { AccountId = 1, Amount = 10.4},
            new() { AccountId = 1, Amount = 0.4},
            new() { AccountId = 1, Amount = 100.4},
            new() { AccountId = 1, Amount = 1000},
        };
        _accountService.GetTransactions(1).Returns(transactions);

        var response = _controller.GetLastestTransactions(1);
        var result = (OkObjectResult)response.Result as IEnumerable<Transaction>;

        Assert.That(response.Result, Is.TypeOf<OkObjectResult>());
        Assert.That(result.First().Amount, Is.EqualTo(transactions[0].Amount));
    }

    [Test]
    public void GetLatestTransactions_WithinvalidId_ShouldReturn404()
    {
        var response = _controller.GetLastestTransactions(0);

        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }

}
