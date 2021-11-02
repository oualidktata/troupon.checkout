using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Troupon.Core.Domain.Ordering.Entities.Common;
using Troupon.Core.Domain.Ordering.Entities.Customer;
using Troupon.Core.Domain.Ordering.Entities.Order;

namespace Troupon.Infra.Persistence.Ordering
{
  public class CheckoutDbContext : DbContext
  {
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public CheckoutDbContext(
      DbContextOptions<CheckoutDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(
      ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("Troupon.Checkout");
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
