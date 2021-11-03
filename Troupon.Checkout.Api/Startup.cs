using System.Reflection;
using Infra.Api.DependencyInjection;
using Infra.Authorization.Policies;
using Infra.MediatR;
using Infra.OAuth.Controllers.DependencyInjection;
using Infra.OAuth.DependencyInjection;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Troupon.Catalog.Api.DependencyInjectionExtensions;
using Troupon.Checkout.Api.ToMoveOrRemove;
using Troupon.Checkout.Core.Application.Commands;
using Troupon.Checkout.Infra.Persistence;

namespace Troupon.Checkout.Api
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
      services.AddOAuthGenericAuthentication(Configuration).AddOAuthM2MAuthFlow();

      services.AddControllers().AddNewtonsoftJson();
      services.AddOAuthController();

      services.AddAuthorization(options =>
      {
        options.AddPolicy(TenantPolicy.Key, pb => pb.AddTenantPolicy("pwc"));
        options.AddPolicy(AdminOnlyPolicy.Key, pb => pb.AddAdminOnlyPolicy());
      });

      services.AddPolicyHandlers();

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

      services.AddPwcApiBehaviour();
    }

    public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider, IDbContextFactory<CheckoutDbContext> dbContextFactory)
    {
      var checkoutDbContext = dbContextFactory.CreateDbContext();
      checkoutDbContext.Database.Migrate();

      app.UseExceptionHandler("/error");

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      app.UseSwagger();
      app.ConfigureSwaggerUI(apiVersionDescriptionProvider);

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
