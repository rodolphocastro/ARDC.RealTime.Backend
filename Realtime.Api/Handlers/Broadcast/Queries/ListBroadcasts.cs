using MediatR;
using Realtime.Api.Models;
using System.Collections.Generic;

namespace Realtime.Api.Handlers.Broadcast.Queries
{
    /// <summary>
    /// Query para listar todos os broadcasts.
    /// </summary>
    public class ListBroadcasts : IRequest<IEnumerable<BroadcastMessage>>
    {
        public ListBroadcasts()
        {

        }
    }
}
