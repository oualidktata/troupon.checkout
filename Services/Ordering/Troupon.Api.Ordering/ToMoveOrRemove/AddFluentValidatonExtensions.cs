using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

<<<<<<< HEAD:Services/Ordering/Troupon.Api.Ordering/DependencyInjectionExtensions/AddFluentValidatonExtensions.cs
namespace Troupon.Api.Ordering.DependencyInjectionExtensions
=======
namespace Troupon.Checkout.Api.ToMoveOrRemove
>>>>>>> dev:Services/Ordering/Troupon.Api.Ordering/ToMoveOrRemove/AddFluentValidatonExtensions.cs
{
  public static class AddFluentValidatonExtensions
  {
    public static IServiceCollection AddFluentValidaton(
      this IServiceCollection services)
    {
      services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

      return services;
    }
  }
}
