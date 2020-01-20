using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Messages.Commands
{
    /// <summary>
    /// Command para deletar uma mensagem.
    /// </summary>
    public class DeleteMessage : IRequest
    {
        public DeleteMessage(Guid messsageId)
        {
            MesssageId = messsageId;
        }

        public DeleteMessage(UserMessage userMessage) : this(userMessage.MessageId)
        {

        }

        /// <summary>
        /// ID da mensagem a ser deletada.
        /// </summary>
        public Guid MesssageId { get; }
    }
}
