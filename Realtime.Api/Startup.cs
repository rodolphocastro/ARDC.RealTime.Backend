using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Realtime.Api.Hubs;
using Realtime.Api.Settings;
using System;

namespace Realtime.Api
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
            services.AddHashStores();
            services.AddMediatRHandlers();
            services.AddCorsConfigurations(Configuration);
            services.AddSignalR();
            services.AddControllers();
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "API para a Demo Realtime",
                    Title = "RealTime API",
                    Version = "v1", // TODO: Buscar versão do Assembly
                    Contact = new OpenApiContact
                    {
                        Name = "Rodolpho Alves",
                        Url = new Uri("https://github.com/rodolphocastro")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(CorsSettings.DefaultCorsSettings);
                endpoints.MapHub<BroadcastHub>(BroadcastHub.HubEndpoint).RequireCors(CorsSettings.SignalRCorsSettings);
                endpoints.MapHub<MessageHub>(MessageHub.HubEndpoint).RequireCors(CorsSettings.SignalRCorsSettings);
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Realtime API");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
