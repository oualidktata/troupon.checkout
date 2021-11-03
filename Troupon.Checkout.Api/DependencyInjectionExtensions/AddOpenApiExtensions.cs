using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Infra.oAuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Troupon.Checkout.Api.Authentication;

namespace Troupon.Checkout.Api.DependencyInjectionExtensions
{
  public static class AddOpenApiExtensions
  {
    public static IServiceCollection AddOpenApi(
      this IServiceCollection services,
      IConfiguration configuration)
    {
      var apiKeySettings = new OAuthSettings();
      configuration.GetSection("Auth:KeyCloackProvider")
        .Bind(apiKeySettings);

      services.AddApiVersioning(cfg =>
      {
        cfg.AssumeDefaultVersionWhenUnspecified = true;
        cfg.DefaultApiVersion = new ApiVersion(1, 0);
      });

      services.AddVersionedApiExplorer();

      services.AddSwaggerGen(
        setup =>
        {
          setup.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
              Version = "v1",
              Title = "Troupon Checkout API",
              Description = "A simple API to manage checkout for Troupon",
              TermsOfService = new Uri("https://example.com/terms"),

              Contact = new OpenApiContact
              {
                Name = "Oualid Ktata",
                Email = string.Empty,
                Url = new Uri("https://github.com/oualidktata/"),
              },
              License = new OpenApiLicense
              {
                Name = "Use under LICX",
                Url = new Uri("https://example.com/license"),
              }
            });
          var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
          var xmlPath = Path.Combine(
            AppContext.BaseDirectory,
            xmlFile);
          setup.IncludeXmlComments(xmlPath);

          var descriptionProvider =
             services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
          foreach (var description in descriptionProvider.ApiVersionDescriptions)
          {
            ConfigureSwaggerGenPerVersion(setup, description);
          }

          setup.DocInclusionPredicate((version, apiDescription) =>
          {
            decimal versionMajor = 1;
            var result = decimal.TryParse(
                version,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out versionMajor);
            var major = Math.Truncate(versionMajor);

            var values = apiDescription.RelativePath.Split('/').Skip(2);

            apiDescription.RelativePath = $"api/v{major}/{string.Join("/", values)}";

            var versionParam = apiDescription.ParameterDescriptions.SingleOrDefault(p => p.Name == "version");
            if (versionParam != null)
            {
              apiDescription.ParameterDescriptions.Remove(versionParam);
            }

            return true;
          });


          #region Auth2 filters and Security

          //setup.SchemaFilter<SchemaFilter>();

          setup.MapType<FileContentResult>(() => new OpenApiSchema {Type = "string", Format = "binary"});
          setup.MapType<IFormFile>(() => new OpenApiSchema {Type = "string", Format = "binary"});

          //setup.DocumentFilter<SecurityRequirementDocumentFilter>();
          setup.AddBearerAuthentication(apiKeySettings); //Uncomment to enableAuth

          #endregion
        });

      services.Configure<ApiBehaviorOptions>(
        options =>
        {
          options.InvalidModelStateResponseFactory = actionContext =>
          {
            var actionExecutingContext =
              actionContext as ActionExecutingContext;

            if (actionContext.ModelState.ErrorCount > 0
                && actionExecutingContext?.ActionArguments.Count ==
                actionContext.ActionDescriptor.Parameters.Count)
            {
              return new UnprocessableEntityObjectResult(actionContext.ModelState);
            }

            return new BadRequestObjectResult(actionContext.ModelState);
          };
        });

      return services;
    }

    private static void ConfigureSwaggerGenPerVersion(SwaggerGenOptions setup, ApiVersionDescription description)
    {
      setup.SwaggerDoc(description.GroupName, new OpenApiInfo
      {
        Title = $"Troupon.Checkout Api Specification {description.ApiVersion}",
        Description = "Api specification",
        Version = description.ApiVersion.ToString(),
        Contact = new OpenApiContact()
        {
          Email = "oualid.ktata@gmail.com",
          Name = "Oualid Ktata",
        },
        License = new OpenApiLicense()
        {
          Name = "OKT",
          Url = new Uri("https://opensource.org/licenses/MIT"),
        },
      });
    }
  }
}
