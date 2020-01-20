using Realtime.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Stores
{
    /// <summary>
    /// Métodos necessários para Stores de Broadcast.
    /// </summary>
    public interface IBroadcastStore
    {
        /// <summary>
        /// Adiciona um novo broadcast à store.
        /// </summary>
        /// <param name="newBroadcast">broadcast a ser adicionado</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        Task AddBroadcast(BroadcastMessage newBroadcast, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem todos os broadcasts da store.
        /// </summary>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        /// <returns>um enumerável de broadcasts</returns>
        ValueTask<IEnumerable<BroadcastMessage>> ListBroadcasts(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem um broadcast específico.
        /// </summary>
        /// <param name="id">id do broadcast</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        /// <returns>o broadcast encontrado</returns>
        ValueTask<BroadcastMessage> GetBroadcast(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem o broadcast mais recente.
        /// </summary>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        /// <returns>o ultimo broadcast</returns>
        ValueTask<BroadcastMessage> GetLatestBroadcast(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deleta um broadcast específico.
        /// </summary>
        /// <param name="id">id do broadcast</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        Task DeleteBroadcast(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Atualiza um broadcast.
        /// </summary>
        /// <param name="id">id do broadcast</param>
        /// <param name="updatedBroadcast">novos valores do broadcast</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        Task UpdateBroadcast(Guid id, BroadcastMessage updatedBroadcast, CancellationToken cancellationToken = default);
    }
}
