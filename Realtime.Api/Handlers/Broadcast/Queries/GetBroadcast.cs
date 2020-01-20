using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Broadcast.Queries
{
    /// <summary>
    /// Query para consultar um broadcast específico.
    /// </summary>
    public class GetBroadcast : IRequest<BroadcastMessage>
    {
        public GetBroadcast(Guid broadcastId)
        {
            BroadcastId = broadcastId;
        }

        /// <summary>
        /// ID do Broadcast a ser recuperado.
        /// </summary>
        public Guid BroadcastId { get; }
    }
}
