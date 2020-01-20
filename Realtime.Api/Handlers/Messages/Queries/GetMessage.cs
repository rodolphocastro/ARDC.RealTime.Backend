using MediatR;
using Realtime.Api.Models;
using System;

namespace Realtime.Api.Handlers.Messages.Queries
{
    /// <summary>
    /// Query para recuperar uma mensagem específica.
    /// </summary>
    public class GetMessage : IRequest<UserMessage>
    {
        public GetMessage(Guid messageId)
        {
            MessageId = messageId;
        }

        /// <summary>
        /// ID da mensagem.
        /// </summary>
        public Guid MessageId { get; }
    }
}
