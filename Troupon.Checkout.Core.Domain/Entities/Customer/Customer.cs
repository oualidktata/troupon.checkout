using Infra.DomainDrivenDesign.Base;

namespace Troupon.Core.Domain.Ordering.Entities.Customer
{
  public class Customer : AggregateRoot
  {
    public string Name { get; set; }
  }
}
