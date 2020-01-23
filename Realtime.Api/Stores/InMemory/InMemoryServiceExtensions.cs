using Realtime.Api.Stores;
using Realtime.Api.Stores.InMemory;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions para registrar as Stores InMemory.
    /// </summary>
    public static class InMemoryServiceExtensions
    {
        /// <summary>
        /// Adiciona serviços para as stores utilizando HashSets.
        /// </summary>
        /// <param name="services"></param>
        public static void AddHashStores(this IServiceCollection services)
        {
            services.AddSingleton<IBroadcastStore, BroadcastHashStore>();
            services.AddSingleton<IMessageStore, MessageHashStore>();
        }
    }
}
