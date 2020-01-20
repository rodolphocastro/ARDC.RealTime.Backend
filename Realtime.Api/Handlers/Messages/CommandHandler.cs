using MediatR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Messages.Commands;
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

        public CommandHandler(ILogger<CommandHandler> logger, IMessageStore messageStore)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.messageStore = messageStore ?? throw new ArgumentNullException(nameof(messageStore));
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
                return newMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
