using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Broadcast.Commands
{
    /// <summary>
    /// Command para criar um novo broadcast no sistema.
    /// </summary>
    public class CreateBroadcast : IRequest<BroadcastMessage>
    {
        public CreateBroadcast(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("O título é obrigatório", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("O conteúdo é obrigatório", nameof(content));
            }

            Title = title;
            Content = content;
        }

        public CreateBroadcast(BroadcastMessage broadcast) : this(broadcast.Title, broadcast.Content) { }

        /// <summary>
        /// Título do Broadcast.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Conteúdo do Broadcast.
        /// </summary>
        public string Content { get; }
    }
}
