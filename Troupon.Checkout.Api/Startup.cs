using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Infra.oAuthService;
using Infra.Persistence.EntityFramework.Extensions;
using Infra.Persistence.SqlServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Troupon.Checkout.Api.DependencyInjectionExtensions;
using Troupon.Checkout.Core.Application;
using Troupon.Checkout.Infra.Persistence;

namespace Troupon.Checkout.Api
{
  public class Startup
  {
    public Startup(
      IConfiguration configuration,
      IWebHostEnvironment hostEnvironment)
    {
      Configuration = configuration;
      HostEnvironment = hostEnvironment;
      AuthSettings = new OAuthSettings();
    }

    private IOAuthSettings AuthSettings { get; }
    private IConfiguration Configuration { get; }
    private IWebHostEnvironment HostEnvironment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(
      IServiceCollection services)
    {      
      Configuration.GetSection($"Auth:{Configuration.GetValue<string>("Auth:DefaultAuthProvider")}")
        .Bind(AuthSettings);
      services.AddScoped<IAuthService>(service => new AuthService(AuthSettings));

      services.AddAuthorization(
        options =>
        {
          //options.AddPolicy("crm-api-backend", policy => policy.RequireClaim("crm-api-backend", "[crm-api-backend]"));
        });

      services.AddAutoMapper(
        typeof(AutomapperProfile));

      services.AddMediator();
      services.AddSqlServerPersistence<CheckoutDbContext>(
        Configuration,
        "mainDatabaseConnStr",
        Assembly.GetExecutingAssembly()
          .GetName()
          .Name);
      
      services.AddEfReadRepository<CheckoutDbContext>();
      services.AddEfWriteRepository<CheckoutDbContext>();
      //services.AddOpenApi(AuthSettings);
      services.AddControllers();
      services.AddOpenApi(Configuration);
      services.AddMetrics();
      services.AddFluentValidaton();
      services.AddMemoryCache();      

      services.Configure<MvcOptions>(o =>
      {
        o.Filters.Add(new ProducesAttribute("application/json", "application/xml"));
        o.Filters.Add(new ConsumesAttribute("application/json", "application/xml"));
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
      IApplicationBuilder app,
      IApiVersionDescriptionProvider apiVersionDescriptionProvider,
      IWebHostEnvironment env,
      IDbContextFactory<CheckoutDbContext> dbContextFactory)
    {
      //if (env.IsDevelopment())
      //{
      //    app.UseDeveloperExceptionPage();
      //}
      app.UseExceptionHandler("/error");

      app.UseHttpsRedirection();
      app.UseSerilogRequestLogging();

      // app.UsePathBase("/graphql");

      //catalogDbContext.Database.EnsureDeleted();
      //var catalogDbContext = dbContextFactory.CreateDbContext();
      //catalogDbContext.Database.Migrate();

      // app.UsePlayground();

      app.UseSwagger();      
      app.UseSwaggerUI(
        c =>
        {
          foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
          {
            c.SwaggerEndpoint($"/swagger/{description.ApiVersion}/swagger.json", $"Troupon Checkout Specification{description.ApiVersion}");
            c.RoutePrefix = string.Empty;
          }

          c.RoutePrefix = string.Empty;
        });
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(
        endpoints =>
        {
          endpoints.MapControllers();
        });
    }
  }
}
