using DB_Test_API.Models;
using DB_Test_API.Services;
using Db_test_project.Controllers;
using Db_test_project.DTOs.Requests.Create;
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

    [SetUp]
    public void Setup()
    {
        _paymentService = Substitute.For<IPaymentService>();
        _controller = new PaymentController(_paymentService)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public void Withdraw_WithInvalidId_ShouldReturn400()
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
        _paymentService.Withdraw(13, 200).ReturnsNull();
        var response = _controller.Withdraw(new PaymentDTO { AccountId = 13, Amount = 200 });

        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Withdraw_WithCorrectInput_ShouldReturn201()
    {
        var paymentDTO = new PaymentDTO { AccountId = 1, Amount = 200 };
        _paymentService.Withdraw(paymentDTO.AccountId, paymentDTO.Amount).Returns(new Transaction
        {
            Id = Guid.NewGuid(),
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


    [Test]
    public void Deposit_WithInvalidId_ShouldReturn400()
    {
        var response = _controller.Deposit(new PaymentDTO { AccountId = 0, Amount = 200.5 });

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Deposit_WithAmountZero_ShouldReturn400()
    {
        var response = _controller.Deposit(new PaymentDTO { AccountId = 1, Amount = 0 });

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Deposit_WithNegativeAmount_ShouldReturn400()
    {
        var response = _controller.Deposit(new PaymentDTO { AccountId = 0, Amount = -200 });

        Assert.That(response.Result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void Deposit_WithUnkownAccount_ShouldReturn404()
    {
        _paymentService.Deposit(13, 200).ReturnsNull();
        var response = _controller.Deposit(new PaymentDTO { AccountId = 13, Amount = 200 });

        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void Deposit_WithCorrectInput_ShouldReturn201()
    {
        var paymentDTO = new PaymentDTO { AccountId = 1, Amount = 200 };
        _paymentService.Deposit(paymentDTO.AccountId, paymentDTO.Amount).Returns(new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = 1,
            Amount = 200,
            Timestamp = DateTime.UtcNow,
        });
        var response = _controller.Deposit(paymentDTO);
        var result = ((OkObjectResult)response.Result).Value as Transaction;

        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.TypeOf<Guid>());
            Assert.That(result.Amount, Is.EqualTo(paymentDTO.Amount)); // Deposit transaction should be negative
        });

    }

    [Test]
    public void GetTenLatestTransactions_WithinvalidId_ShouldReturn404()
    {
        var response = _controller.Get10LastestTransactions(0);

        Assert.That(response.Result, Is.TypeOf<NotFoundResult>());
    }
}
