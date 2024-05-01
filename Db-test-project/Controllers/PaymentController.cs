using DB_Test_API.Models;
using Db_test_project.DTOs.Requests.Create;
using Db_test_project.DTOs.Responses;
using Db_test_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Db_test_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Deposit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Transaction> Deposit(PaymentDTO paymentDTO)
        {
            // invalid request data
            if (paymentDTO == null || paymentDTO.AccountId == 0 || paymentDTO.Amount <= 0)
            {
                return BadRequest();
            }
            var transaction = _paymentService.Deposit(paymentDTO.AccountId, paymentDTO.Amount);
            if (transaction == null)
            {
                return NotFound(); // Account was not found in DB
            }
            return Ok(transaction);
        }

        [HttpPost("Withdraw")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Transaction> Withdraw(PaymentDTO paymentDTO)
        {
            // invalid request data
            if (paymentDTO == null || paymentDTO.AccountId == 0 || paymentDTO.Amount <= 0) 
            {
                return BadRequest();
            }
            var transaction = _paymentService.Withdraw(paymentDTO.AccountId, paymentDTO.Amount);
            if (transaction == null)
            {
                return NotFound(); // Account was not found in DB
            }
            return Ok(transaction);
        }
    }
}
