using Realtime.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Stores
{
    /// <summary>
    /// Métodos necessários para uma store de Mensagens de Usuários.
    /// </summary>
    public interface IMessageStore
    {
        /// <summary>
        /// Adiciona uma nova mensagem à store.
        /// </summary>
        /// <param name="newMessage">mensagem a ser adicionada</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        Task AddMessage(UserMessage newMessage, CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem todas as mensagens da store.
        /// </summary>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        /// <returns>um enumeravel de mensagens</returns>
        ValueTask<IEnumerable<UserMessage>> ListMessages(CancellationToken cancellationToken = default);

        /// <summary>
        /// Obtem uma mensagem específica.
        /// </summary>
        /// <param name="id">id da mensagem</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        /// <returns>a mensagem encontrada</returns>
        ValueTask<UserMessage> GetMessage(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deleta uma mensagem específica.
        /// </summary>
        /// <param name="id">id da mensagem</param>
        /// <param name="cancellationToken">token para controle de cancelamento</param>
        Task DeleteMessage(Guid id, CancellationToken cancellationToken = default);

    }
}
