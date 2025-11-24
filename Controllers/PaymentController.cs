using Ecommerce.DTOs;
using Ecommerce.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pagamento")]
        public IActionResult ProcessPayment([FromBody] PaymentRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data.");
            }

            var response = _paymentService.ProcessPayment(request);

            if (response.Status == "refused")
            {
                return StatusCode(402, response.Message);
            }

            return Ok(response);
        }
    }
}
