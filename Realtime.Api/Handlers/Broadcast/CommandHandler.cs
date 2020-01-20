using MediatR;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Broadcast.Commands;
using Realtime.Api.Models;
using Realtime.Api.Stores;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Handlers.Broadcast
{
    /// <summary>
    /// Handler para os comandos de Broadcasts.
    /// </summary>
    public class CommandHandler : IRequestHandler<CreateBroadcast, BroadcastMessage>, IRequestHandler<DeleteBroadcast>, IRequestHandler<UpdateBroadcast>
    {
        private readonly ILogger<CommandHandler> logger;
        private readonly IBroadcastStore broadcastStore;

        public CommandHandler(ILogger<CommandHandler> logger, IBroadcastStore broadcastStore)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.broadcastStore = broadcastStore ?? throw new ArgumentNullException(nameof(broadcastStore));
        }

        public Task<BroadcastMessage> Handle(CreateBroadcast request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var newBroadcast = new BroadcastMessage
            {
                MessageId = Guid.NewGuid(),
                DthCriacao = DateTimeOffset.Now,
                Title = request.Title,
                Content = request.Content
            };

            return CreateBroadcastInternalAsync(newBroadcast, cancellationToken);
        }

        public Task<Unit> Handle(DeleteBroadcast request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return DeleteBroadcastInternalAsync(request.BroadcastId, cancellationToken);
        }

        public Task<Unit> Handle(UpdateBroadcast request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return UpdateBroadcastInternalAsync(request.BroadcastId, request.Title, request.Content, cancellationToken);
        }

        private async Task<Unit> UpdateBroadcastInternalAsync(Guid broadcastId, string title, string content, CancellationToken cancellationToken)
        {
            try
            {
                var existingBroadcast = await broadcastStore.GetBroadcast(broadcastId, cancellationToken);
                if (existingBroadcast is null)
                {
                    throw new ArgumentException("O broadcast não existe", nameof(broadcastId));
                }

                existingBroadcast.Title = title;
                existingBroadcast.Content = content;
                await broadcastStore.UpdateBroadcast(broadcastId, existingBroadcast, cancellationToken);
                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Unit> DeleteBroadcastInternalAsync(Guid broadcastId, CancellationToken cancellationToken)
        {
            try
            {
                await broadcastStore.DeleteBroadcast(broadcastId, cancellationToken);
                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<BroadcastMessage> CreateBroadcastInternalAsync(BroadcastMessage newBroadcast, CancellationToken cancellationToken)
        {
            try
            {
                await broadcastStore.AddBroadcast(newBroadcast, cancellationToken);
                return newBroadcast;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
