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
    [Test]
    public void GetTenLatestTransactions_WithValidId_ShouldReturn10TransactionsOrdered()
    {
        // 12 transactions (we should only get 10 latest
        var transactions = new List<Transaction>() {
            new() { AccountId = 1, Amount = 1.1, Timestamp = new DateTime(2024, 1, 1)},
            new() { AccountId = 1, Amount = 2.1, Timestamp = new DateTime(2024, 1, 13)},
            new() { AccountId = 1, Amount = 404, Timestamp = new DateTime(2022, 1, 1)},// to old
            new() { AccountId = 1, Amount = 4.1, Timestamp = new DateTime(2024, 1, 19)}, // newest
            new() { AccountId = 1, Amount = 22.1, Timestamp = new DateTime(2024, 1, 12)},
            new() { AccountId = 1, Amount = 404, Timestamp = new DateTime(2020, 1, 1)}, // to old
            new() { AccountId = 1, Amount = 1131, Timestamp = new DateTime(2024, 1, 6)},
            new() { AccountId = 1, Amount = 146.1, Timestamp = new DateTime(2024, 1, 8)},
            new() { AccountId = 1, Amount = 1094.1, Timestamp = new DateTime(2024, 1, 9)},
            new() { AccountId = 1, Amount = 1949.1, Timestamp = new DateTime(2024, 1, 10)},
            new() { AccountId = 1, Amount = 109.1, Timestamp = new DateTime(2024, 1, 4)},
            new() { AccountId = 1, Amount = 1231.1, Timestamp = new DateTime(2024, 1, 4)},
        };
        _accountLookupService.GetAccount(1).Returns(new Account()
        {
            Id = 1,
            Transactions = transactions
        });

        var result = _paymentService.GetTenLatestTransactions(1);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(10));
            Assert.That(result.First().Amount, Is.EqualTo(transactions.ElementAt(3).Amount));
            Assert.That(result.Find(e => e.Amount == 404d), Is.Null);
        });
    }
}
