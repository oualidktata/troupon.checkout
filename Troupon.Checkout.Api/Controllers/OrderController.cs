using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Troupon.Checkout.Api.Conventions;
using Troupon.Checkout.Core.Application.DTOs;

namespace Troupon.Checkout.Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class OrderController : BaseController
  {
    public OrderController(
      IMediator mediator,
      IMapper mapper) : base(
      mediator,
      mapper)
    {
    }

    [SwaggerOperation(
     Description = "Place a new order",
     OperationId = "place-order")]
    [HttpPost]
    public Task<OrderPlacedDto> Post([FromBody] PlaceOrderAsGuestDto model)
    {
      return Task.FromResult(new OrderPlacedDto());      
    }
  }
}
