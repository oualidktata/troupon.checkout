using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Troupon.Api.Ordering.Convention;
using Troupon.Core.Application.Ordering.Commands;
using Troupon.Core.Application.Ordering.DTOs;

namespace Troupon.Api.Ordering.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiVersion("1.0")]
  [ApiConventionType(typeof(PwcApiConventions))]
  public class OrderController : PwcBaseController
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
    public async Task<ActionResult<OrderPlacedDto>> PostAsync([FromBody] PlaceOrderAsAGuestCommand model)
    {
      return Ok(await Mediator.Send(model));
    }
  }
}
