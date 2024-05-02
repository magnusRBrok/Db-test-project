using DB_Test_API.Controllers;
using DB_Test_API.Models;
using DB_Test_API.Services;
using Db_test_project.DTOs.Requests.Create;
using Db_test_project.DTOs.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DB_Test_API.Tests;


[TestFixture]
class AccountControllerTest
{
    private AccountController _controller;
    private IAccountService _accountService;

    [SetUp]
    public void Setup()
    {
        _accountService = Substitute.For<IAccountService>();
        _controller = new AccountController(_accountService)
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

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Value, Is.EqualTo(expectedAmount));
        });
    }

    [Test]
    public void GetAccountBalance_WithInvalidId_ShouldReturn404()
    {
        _accountService.GetAccountBalance(1).Returns(100);
        var response = _controller.GetAccountBalance(0);
        
        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void CreateAccount_WithValidId_ShouldReturnResponse()
    {
        var mockAccount = new Account { Id = 1, CustomerId = 1, Balance = 0 };
        _accountService.CreateAccount(1).Returns(mockAccount);

        var response = _controller.CreateAccount(new CreateAccountDto { CustomerId = 1});
        var result = ((OkObjectResult)response.Result).Value as CreateAccountResponse;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(mockAccount.Id));
            Assert.That(result.Balance, Is.EqualTo(mockAccount.Balance));
            Assert.That(result.CustomerId, Is.EqualTo(mockAccount.CustomerId));
        });
    }

    [Test]
    public void CreateAccount_WithinvalidId_ShouldReturn400()
    {
        var response = _controller.CreateAccount(new CreateAccountDto { CustomerId = 0 });

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void CreateAccount_WithUnknownCustomerId_ShouldReturn404()
    {
        _accountService.CreateAccount(1).ReturnsNull();
        var response = _controller.CreateAccount(new CreateAccountDto { CustomerId = 13 });

        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }
}
