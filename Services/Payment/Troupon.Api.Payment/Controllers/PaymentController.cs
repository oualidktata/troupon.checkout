using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Troupon.Shared.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Troupon.Api.Payment.Controllers
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
