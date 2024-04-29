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
    public void GetAccountBalanceTest_WithValidId_ShouldReturnBalance()
    {
        var testAccount = new Account { Id = 1, Balance = 100.5 };
        _accountService.GetAccount(1).Returns(testAccount);

        var result = _controller.GetAccountBalance(1);

        Assert.That(result, Is.EqualTo(testAccount.Balance));
    }

}
