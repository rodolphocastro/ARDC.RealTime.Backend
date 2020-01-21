using Realtime.Api.Models;
using System.Threading.Tasks;

namespace Realtime.Api.Clients
{
    /// <summary>
    /// Métodos para Clients dos Broadcasts.
    /// </summary>
    public interface IBroadcastClient
    {
        /// <summary>
        /// Recebe um novo broadcast criado no sistema.
        /// </summary>
        /// <param name="broadcastMessage">o novo broadcast</param>
        Task ReceiveNewBroadcast(BroadcastMessage broadcastMessage);

        /// <summary>
        /// Recebe um broadcast deletado do sistema.
        /// </summary>
        /// <param name="broadcastMessage">o broadcast deletado</param>
        Task ReceiveDeletedBroadcast(BroadcastMessage broadcastMessage);

        /// <summary>
        /// Recebe um broadcast atualizado no sistema.
        /// </summary>
        /// <param name="broadcastMessage">o broadcast atualizado</param>
        Task ReceiveUpdatedBroadcast(BroadcastMessage broadcastMessage);

    }
}
