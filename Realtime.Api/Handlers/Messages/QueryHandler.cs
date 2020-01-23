using MediatR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Messages.Queries;
using Realtime.Api.Models;
using Realtime.Api.Stores;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Handlers.Messages
{
    /// <summary>
    /// Handler para as queries de mensagens.
    /// </summary>
    public class QueryHandler : IRequestHandler<GetMessage, UserMessage>, IRequestHandler<ListMessages, IEnumerable<UserMessage>>
    {
        private readonly ILogger<QueryHandler> logger;
        private readonly IMessageStore messageStore;

        public QueryHandler(ILogger<QueryHandler> logger, IMessageStore messageStore)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.messageStore = messageStore ?? throw new ArgumentNullException(nameof(messageStore));
        }

        public Task<UserMessage> Handle(GetMessage request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return GetMessageInternalAsync(request.MessageId, cancellationToken);
        }

        public Task<IEnumerable<UserMessage>> Handle(ListMessages request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return ListMessagesInternalAsync(cancellationToken);
        }

        private async Task<IEnumerable<UserMessage>> ListMessagesInternalAsync(CancellationToken cancellationToken) => await messageStore.ListMessages(cancellationToken);

        private async Task<UserMessage> GetMessageInternalAsync(Guid messageId, CancellationToken cancellationToken) => await messageStore.GetMessage(messageId, cancellationToken);
    }
}
