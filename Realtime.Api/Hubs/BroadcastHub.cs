using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Clients;
using Realtime.Api.Models;
using System;
using System.Threading.Tasks;

namespace Realtime.Api.Hubs
{
    /// <summary>
    /// Hub para interação com Broadcasts.
    /// </summary>
    public class BroadcastHub : Hub<IBroadcastClient>
    {
        public const string HubEndpoint = "/broadcastHub";
        private readonly ILogger<BroadcastHub> logger;

        public BroadcastHub(ILogger<BroadcastHub> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task NotifyNewBroadcast(BroadcastMessage broadcast)
        {
            await Clients.All.ReceiveNewBroadcast(broadcast);
        }

        public async Task NotifyDeletedBroadcast(BroadcastMessage broadcast)
        {
            await Clients.All.ReceiveDeletedBroadcast(broadcast);
        }

        public async Task NotifyUpdatedBroadcast(BroadcastMessage broadcast)
        {
            await Clients.All.ReceiveUpdatedBroadcast(broadcast);
        }

    }
}
