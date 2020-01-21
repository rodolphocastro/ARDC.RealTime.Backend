using Microsoft.Extensions.Configuration;
using Realtime.Api.Settings;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions para o registro de settings.
    /// </summary>
    public static class SettingsExtensions
    {
        /// <summary>
        /// Registra as configurações de Cors da aplicação.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">configuração do aplicativo</param>
        public static void AddCorsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            ICorsSettings defaultSettings = configuration.GetSection(CorsSettings.DefaultCorsSettings).Get<CorsSettings>();
            ICorsSettings signalRSettings = configuration.GetSection(CorsSettings.SignalRCorsSettings).Get<CorsSettings>();
            services.AddCors(setup =>
            {
                setup.AddDefaultPolicy(builder => builder.WithOrigins(defaultSettings.AllowedOrigins)
                    .WithMethods(defaultSettings.AllowedMethods)
                    .AllowAnyHeader());

                setup.AddPolicy(CorsSettings.SignalRCorsSettings,
                    builder => builder.WithOrigins(signalRSettings.AllowedOrigins)
                    .WithMethods(signalRSettings.AllowedMethods)
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}
