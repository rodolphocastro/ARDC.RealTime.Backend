using Realtime.Api.Models;
using System;
using System.Threading.Tasks;

namespace Realtime.Api.Clients
{
    /// <summary>
    /// Métodos para Clients das Mensagens.
    /// </summary>
    public interface IMessageClient
    {
        /// <summary>
        /// Recebe uma nova mensagem enviada para o sistema.
        /// </summary>
        /// <param name="message">a mensagem criada</param>
        Task ReceiveNewMessage(UserMessage message);

        /// <summary>
        /// Recebe uma mensagem deletada do sistema.
        /// </summary>
        /// <param name="messageId">id da mensagem deletada</param>
        Task ReceiveDeletedMessage(Guid messageId);

        /// <summary>
        /// Cria uma nova mensagem no sistema.
        /// </summary>
        /// <param name="content">conteudo da mensagem</param>
        /// <param name="username">nome do usuário</param>
        Task SendMessage(string content, string username);

        /// <summary>
        /// Deleta uma mensagem do sistema.
        /// </summary>
        /// <param name="messageId">id da mensagem</param>
        Task DeleteMessage(Guid messageId);
    }
}
