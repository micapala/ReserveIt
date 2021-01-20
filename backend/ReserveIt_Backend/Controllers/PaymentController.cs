using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Dtos.Payment;
using ReserveIt_Backend.Services.Interfaces;

namespace ReserveIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController: BaseController
    {

        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [HttpPost("updatePaymentStatus")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult updatePaymentStatus([FromForm] IFormCollection data)
        {
            _paymentService.Update(data["control"], float.Parse(data["operation_amount"].ToString().Replace('.',',')));

            return Ok();
        }

        [HttpGet("getPaymentLink/{controlString}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public IActionResult getPaymentLink(string controlString)
        {
            var  paymentLink = _paymentService.GetPaymentLink(controlString);

            return Ok(paymentLink);
        }

    }
 
}
