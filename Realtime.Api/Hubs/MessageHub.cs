using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Clients;
using Realtime.Api.Handlers.Messages.Commands;
using Realtime.Api.Handlers.Messages.Queries;
using Realtime.Api.Models;
using System;
using System.Threading.Tasks;

namespace Realtime.Api.Hubs
{
    /// <summary>
    /// Hub para interação com Mensagens.
    /// </summary>
    public class MessageHub : Hub<IMessageClient>
    {
        public const string HubEndpoint = "/messageHub";
        private readonly ILogger<MessageHub> logger;
        private readonly IMediator mediator;

        public MessageHub(ILogger<MessageHub> logger, IMediator mediator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task NotifyNewMessage(UserMessage message)
        {
            await Clients.All.ReceiveNewMessage(message);
        }

        public async Task NotifyDeletedMessage(Guid messageId)
        {
            await Clients.All.ReceiveDeletedMessage(messageId);
        }

        public async Task CreateMessage(string content, string username)
        {
            try
            {
                var result = await mediator.Send(new CreateMessage(username, content));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao criar nova mensagem");
                throw;
            }
        }

        public async Task DeleteMessage(Guid id)
        {
            try
            {
                var existingMessage = await mediator.Send(new GetMessage(id));
                if (existingMessage is null)
                {
                    throw new InvalidOperationException("A mensagem não existe");
                }

                await mediator.Send(new DeleteMessage(existingMessage));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao deletar mensagem");
                throw;
            }
        }
    }
}
