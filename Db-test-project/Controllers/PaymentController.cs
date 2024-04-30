using DB_Test_API.Models;
using Db_test_project.DTOs.Requests.Create;
using Db_test_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db_test_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _PaymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _PaymentService = paymentService;
        }

        [HttpPost("Deposit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Transaction> Deposit(PaymentDTO paymentDTO)
        {
            return null;
        }

        [HttpPost("Withdraw")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Transaction> Withdraw(PaymentDTO paymentDTO)
        {
            return null;
        }
    }
}
