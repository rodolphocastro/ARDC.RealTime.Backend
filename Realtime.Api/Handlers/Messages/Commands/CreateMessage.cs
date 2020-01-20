using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Messages.Commands
{
    /// <summary>
    /// Command para criar uma nova mensagem.
    /// </summary>
    public class CreateMessage : IRequest<UserMessage>
    {
        /// <summary>
        /// Nome do usuário enviando a mensagem.
        /// </summary>
        public string Username { get; }
        /// <summary>
        /// Conteúdo da mensagem.
        /// </summary>
        public string Content { get; }

        public CreateMessage(string username, string content)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("O nome do usuário é obrigatório para uma mensagem", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("O conteúdo é obrigatório para uma mensagem", nameof(content));
            }

            Username = username;
            Content = content;
        }

        public CreateMessage(UserMessage newMessage) : this(newMessage.Username, newMessage.Content)
        {

        }
    }
}
