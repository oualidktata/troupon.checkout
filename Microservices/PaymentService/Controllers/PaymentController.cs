using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PaymentController : ControllerBase
  {
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentController(IPublishEndpoint publishEndpoint)
    {
      _publishEndpoint = publishEndpoint;
    }

    // GET: api/<PaymentController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<PaymentController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<PaymentController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentConfirmation paymentConfirmation)
    {
      await _publishEndpoint.Publish<PaymentConfirmation>(paymentConfirmation);
      return Ok();
    }

    // PUT api/<PaymentController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PaymentController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}