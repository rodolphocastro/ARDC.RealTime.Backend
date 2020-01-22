using Bogus;
using Realtime.Api.Handlers.Broadcast.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions para registrar Fakers na Injeção de Dependência.
    /// </summary>
    public static class FakerServicesExtensions
    {
        /// <summary>
        /// Adiciona os Fakers relacionados a broadcasts.
        /// </summary>
        /// <param name="services"></param>
        public static void AddBroadcastFakers(this IServiceCollection services)
        {
            var createBroadcastFaker = new Faker<CreateBroadcast>()
                .CustomInstantiator(f => new CreateBroadcast(f.Hacker.Phrase(), f.Lorem.Paragraph()));

            services.AddSingleton(createBroadcastFaker);
        }
    }
}
