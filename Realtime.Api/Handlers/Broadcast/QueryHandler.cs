using MediatR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Broadcast.Queries;
using Realtime.Api.Models;
using Realtime.Api.Stores;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Handlers.Broadcast
{
    /// <summary>
    /// Handler para as consultas de broadcasts.
    /// </summary>
    public class QueryHandler : IRequestHandler<GetBroadcast, BroadcastMessage>, IRequestHandler<GetLatestBroadcast, BroadcastMessage>, IRequestHandler<ListBroadcasts, IEnumerable<BroadcastMessage>>
    {
        private readonly ILogger<QueryHandler> logger;
        private readonly IBroadcastStore broadcastStore;

        public QueryHandler(ILogger<QueryHandler> logger, IBroadcastStore broadcastStore)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.broadcastStore = broadcastStore ?? throw new ArgumentNullException(nameof(broadcastStore));
        }

        public Task<BroadcastMessage> Handle(GetBroadcast request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetBroadcastInternalAsync(request.BroadcastId, cancellationToken);
        }

        public Task<BroadcastMessage> Handle(GetLatestBroadcast request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetLatestBroadcastInternalAsync(cancellationToken);
        }

        public Task<IEnumerable<BroadcastMessage>> Handle(ListBroadcasts request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ListBroadcastsInternalAsync(cancellationToken);
        }

        private async Task<IEnumerable<BroadcastMessage>> ListBroadcastsInternalAsync(CancellationToken cancellationToken) => await broadcastStore.ListBroadcasts(cancellationToken);

        private async Task<BroadcastMessage> GetLatestBroadcastInternalAsync(CancellationToken cancellationToken) => await broadcastStore.GetLatestBroadcast(cancellationToken);

        private async Task<BroadcastMessage> GetBroadcastInternalAsync(Guid broadcastId, CancellationToken cancellationToken)
        {
            var existingBroadcast = await broadcastStore.GetBroadcast(broadcastId, cancellationToken);
            return existingBroadcast;
        }

    }
}
