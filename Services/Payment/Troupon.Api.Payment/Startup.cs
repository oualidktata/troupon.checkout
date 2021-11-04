using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Troupon.Api.Payment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentService", Version = "v1" });
            });
            //services.AddMassTransit(config =>
            //{
            //  config.UsingRabbitMq((ctx, cfg) =>
            //  {
            //    cfg.Host("amqp://guest:guest@localhost:5672");
            //  });
            //});
            //services.AddMassTransitHostedService();
            services.AddMassTransit(config =>
            {
              config.AddConsumer<OrderConsumer>();
              config.UsingRabbitMq((ctx, cfg) =>
              {
                cfg.Host("amqp://guest:guest@localhost:5672");
                cfg.ReceiveEndpoint("payment-queue", c =>
                {
                  c.ConfigureConsumer<OrderConsumer>(ctx);
                });
              });
            });
            services.AddMassTransitHostedService();
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
