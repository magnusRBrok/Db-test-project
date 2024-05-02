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
class PaymentServiceTest
{
    private PaymentService _paymentService;
    private IAccountLookupService _accountLookupService;
    private static List<Account> _accounts;

    [SetUp]
    public void Setup()
    {
        _accountLookupService = Substitute.For<IAccountLookupService>();
        _paymentService = new PaymentService(_accountLookupService);
        _accounts =
        [
            new Account { Id = 1, Balance = 100.5, CustomerId = 1 },
            new Account { Id = 2, Balance = 0, CustomerId = 2 },
        ];

}

    [Test]
    public void Withdraw_WithInvalidId_ShouldReturnNull()
    {
        var response = _paymentService.Withdraw(0, 200.5 );

        Assert.That(response, Is.Null);
    }

    [Test]
    public void Withdraw_WithValidId_ShouldReturnNull()
    {
        _accountLookupService.GetAccount(1).Returns(_accounts[0]);

        var account = _accountLookupService.GetAccount(1);

        Assert.That(account?.Balance, Is.EqualTo(100.5));

        var response = _paymentService.Withdraw(1, 200.5);
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(account.Balance, Is.EqualTo(-100));
            Assert.That(response, Is.TypeOf<Transaction>());
            Assert.That(response?.Timestamp, Is.TypeOf<DateTime>());
            Assert.That(response?.Timestamp, Is.LessThan(DateTime.UtcNow));
        });
    }

    [Test]
    public void Deposit_WithInvalidId_ShouldReturnNull()
    {
        var response = _paymentService.Deposit(0, 200.5);
        Assert.That(response, Is.Null);
    }

    [Test]
    public void Deposit_WithValidId_ShouldReturnNull()
    {
        _accountLookupService.GetAccount(1).Returns(_accounts[0]);

        var account = _accountLookupService.GetAccount(1);

        Assert.That(account?.Balance, Is.EqualTo(100.5));

        var response = _paymentService.Deposit(1, 200.5);
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(account.Balance, Is.EqualTo(301));
            Assert.That(response, Is.TypeOf<Transaction>());
            Assert.That(response?.Timestamp, Is.TypeOf<DateTime>());
            Assert.That(response?.Timestamp, Is.LessThan(DateTime.UtcNow));
        });
    }
}
