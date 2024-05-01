using DB_Test_API.Controllers;
using DB_Test_API.Models;
using DB_Test_API.Services;
using Db_test_project.Controllers;
using Db_test_project.DTOs.Requests.Create;
using Db_test_project.DTOs.Responses;
using Db_test_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace DB_Test_API.Tests;


[TestFixture]
class PaymentControllerTest
{
    private PaymentController _controller;
    private IPaymentService _paymentService;
    private IAccountLookupService _accountLookupService;

    [SetUp]
    public void Setup()
    {
        _paymentService = Substitute.For<IPaymentService>();
        _accountLookupService = Substitute.For<IAccountLookupService>();
        _controller = new PaymentController(_paymentService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public void Withdraw_WithinvalidId_ShouldReturn400()
    {
        var response = _controller.Withdraw(new PaymentDTO { AccountId = 0, Amount = 200.5 });

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Withdraw_WithAmountZero_ShouldReturn400()
    {
        var response = _controller.Withdraw(new PaymentDTO { AccountId = 1, Amount = 0});

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Withdraw_WithNegativeAmount_ShouldReturn400()
    {
        var response = _controller.Withdraw(new PaymentDTO { AccountId = 0, Amount = -200 });

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Withdraw_WithUnkownAccount_ShouldReturn404()
    {
        _accountLookupService.GetAccount(13).ReturnsNull();
        var response = _controller.Withdraw(new PaymentDTO { AccountId = 13, Amount = 200 });

        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Withdraw_WithCorrectInput_ShouldReturn201()
    {
        var mockAccount = new Account { Id = 1, CustomerId = 1, Balance = 0 };
        var paymentDTO = new PaymentDTO { AccountId = 1, Amount = 200 };
        _accountLookupService.GetAccount(1).Returns(mockAccount);
        _paymentService.Withdraw(paymentDTO.AccountId, paymentDTO.Amount).Returns(new Transaction
        {
            Id = new Guid(),
            AccountId = 1,
            Amount = -200,
            Timestamp = DateTime.UtcNow,
        });
        var response = _controller.Withdraw(paymentDTO);
        var result = ((OkObjectResult)response.Result).Value as Transaction;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.TypeOf<Guid>());
            Assert.That(result.Amount, Is.EqualTo(paymentDTO.Amount*(-1))); // withdraw transaction should be negative
        });

    }
}
