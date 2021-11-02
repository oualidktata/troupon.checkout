using AutoMapper;
using Troupon.Core.Domain.Ordering.Dtos;
using Troupon.Core.Domain.Ordering.Entities.Order;

namespace Troupon.Infra.Persistence.Ordering
{
  public class AutomapperProfile : Profile
  {
    public AutomapperProfile()
    {
      CreateMap<Order, OrderDto>();
    }
  }
}
