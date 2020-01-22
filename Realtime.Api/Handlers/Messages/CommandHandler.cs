using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Clients;
using Realtime.Api.Handlers.Messages.Commands;
using Realtime.Api.Hubs;
using Realtime.Api.Models;
using Realtime.Api.Stores;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Handlers.Messages
{
    /// <summary>
    /// Handler para os Commands de Mensagens.
    /// </summary>
    public class CommandHandler : IRequestHandler<CreateMessage, UserMessage>, IRequestHandler<DeleteMessage>
    {
        private readonly ILogger<CommandHandler> logger;
        private readonly IMessageStore messageStore;
        private readonly IHubContext<MessageHub, IMessageClient> hubContext;

        public CommandHandler(ILogger<CommandHandler> logger, IMessageStore messageStore, IHubContext<MessageHub, IMessageClient> hubContext)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.messageStore = messageStore ?? throw new ArgumentNullException(nameof(messageStore));
            this.hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public Task<UserMessage> Handle(CreateMessage request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var newMessage = new UserMessage
            {
                MessageId = Guid.NewGuid(),
                DthProcessamento = DateTimeOffset.Now,
                Username = request.Username,
                Content = request.Content
            };

            return CreateMessageInternalAsync(newMessage, cancellationToken);
        }

        public Task<Unit> Handle(DeleteMessage request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return DeleteMessageInternalAsync(request.MesssageId, cancellationToken);
        }

        private async Task<Unit> DeleteMessageInternalAsync(Guid messsageId, CancellationToken cancellationToken)
        {
            try
            {
                await messageStore.DeleteMessage(messsageId, cancellationToken);
                await hubContext.Clients.All.ReceiveDeletedMessage(messsageId);
                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<UserMessage> CreateMessageInternalAsync(UserMessage newMessage, CancellationToken cancellationToken)
        {
            try
            {
                await messageStore.AddMessage(newMessage, cancellationToken);
                await hubContext.Clients.All.ReceiveNewMessage(newMessage);
                return newMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
