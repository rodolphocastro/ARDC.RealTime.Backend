using MediatR;
using Realtime.Api.Models;

namespace Realtime.Api.Handlers.Broadcast.Queries
{
    /// <summary>
    /// Query para recuperar o último broadcast publicado.
    /// </summary>
    public class GetLatestBroadcast : IRequest<BroadcastMessage>
    {
        public GetLatestBroadcast()
        {

        }
    }
}
