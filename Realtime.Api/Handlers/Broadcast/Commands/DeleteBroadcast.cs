using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Broadcast.Commands
{
    /// <summary>
    /// Command para deletar um broadcast do sistema.
    /// </summary>
    public class DeleteBroadcast : IRequest
    {
        public DeleteBroadcast(Guid broadcastId)
        {
            BroadcastId = broadcastId;
        }

        public DeleteBroadcast(BroadcastMessage broadcastMessage) : this(broadcastMessage.MessageId)
        {

        }

        /// <summary>
        /// ID do Broadcast a ser deletado.
        /// </summary>
        public Guid BroadcastId { get; }
    }
}
