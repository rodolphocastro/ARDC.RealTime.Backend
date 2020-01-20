using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Broadcast.Commands
{
    /// <summary>
    /// Command para atualizar um Broadcast existente.
    /// </summary>
    public class UpdateBroadcast : IRequest
    {
        public UpdateBroadcast(Guid broadcastId, string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("O título é obrigatório para um broadcast", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("O conteúdo é obrigatório para um broadcast", nameof(content));
            }

            BroadcastId = broadcastId;
            Title = title;
            Content = content;
        }

        public UpdateBroadcast(BroadcastMessage updatedBroadcastMessage) : this(updatedBroadcastMessage.MessageId, updatedBroadcastMessage.Title, updatedBroadcastMessage.Content)
        {

        }

        /// <summary>
        /// ID do broadcast a ser atualizado.
        /// </summary>
        public Guid BroadcastId { get; }

        /// <summary>
        /// Novo título do broadcast.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Novo conteúdo do broadcast.
        /// </summary>
        public string Content { get; }
    }
}
