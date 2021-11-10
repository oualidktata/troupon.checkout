using System.Reflection;
using Infra.Api.DependencyInjection;
using Infra.MediatR;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Troupon.Api.Ordering.DependencyInjectionExtensions;
using Troupon.Infrastructure.Ordering;
using Troupon.Catalog.Api.DependencyInjectionExtensions;
using Troupon.Checkout.Api.ToMoveOrRemove;
using Troupon.Checkout.Core.Application.Commands;
using Troupon.Checkout.Infra.Persistence;

namespace Troupon.Api.Ordering
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers().AddNewtonsoftJson();

      services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

      services.AddMediator(typeof(PlaceOrderCommand).Assembly);
      services.AddSqlServerPersistence<CheckoutDbContext>(Configuration, "mainDatabaseConnStr", Assembly.GetExecutingAssembly());

      services.AddEfReadRepository<CheckoutDbContext>();
      services.AddEfWriteRepository<CheckoutDbContext>();
      services.AddOpenApi(Assembly.GetExecutingAssembly());
      services.AddMetrics();
      services.AddFluentValidaton();
      services.AddMemoryCache();

      services.Configure<MvcOptions>(o =>
      {
        o.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
        o.Filters.Add(new ConsumesAttribute("application/json", "application/xml"));
      });

      services.AddMassTransitConfiguration(Configuration);
      services.AddPwcApiBehaviour();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
     IApplicationBuilder app,
     IApiVersionDescriptionProvider apiVersionDescriptionProvider,
     IWebHostEnvironment env,
     IDbContextFactory<CheckoutDbContext> dbContextFactory)
    {
      var checkoutDbContext = dbContextFactory.CreateDbContext();
      checkoutDbContext.Database.Migrate();

      app.UseExceptionHandler("/error");

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      app.UseSwagger();
      app.ConfigureSwaggerUI(apiVersionDescriptionProvider);

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
