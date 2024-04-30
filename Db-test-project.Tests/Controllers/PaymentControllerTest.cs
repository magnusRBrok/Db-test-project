using DB_Test_API.Controllers;
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
}
