using MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions para registrar os Handlers.
    /// </summary>
    public static class HandlerServiceExtensions
    {
        /// <summary>
        /// Adiciona serviços para os Command e Query Handlers do projeto.
        /// </summary>
        /// <param name="services"></param>
        public static void AddMediatRHandlers(this IServiceCollection services)
        {
            var types = new[]
            {
                typeof(Realtime.Api.Handlers.Broadcast.CommandHandler),
                typeof(Realtime.Api.Handlers.Broadcast.QueryHandler),
                typeof(Realtime.Api.Handlers.Messages.CommandHandler),
                typeof(Realtime.Api.Handlers.Messages.QueryHandler)
            };

            services.AddMediatR(types);
        }
    }
}
