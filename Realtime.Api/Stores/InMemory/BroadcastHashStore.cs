using Microsoft.Extensions.Logging;
using Realtime.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Stores.InMemory
{
    /// <summary>
    /// Implementação da IBroadcastStore utilizando HashSet<T>.
    /// </summary>
    public class BroadcastHashStore : IBroadcastStore
    {
        private readonly ICollection<BroadcastMessage> broadcastSet;
        private readonly ILogger<BroadcastHashStore> logger;

        public BroadcastHashStore(ILogger<BroadcastHashStore> logger)
        {
            this.logger = logger;
            broadcastSet = new HashSet<BroadcastMessage>();
        }

        public Task AddBroadcast(BroadcastMessage newBroadcast, CancellationToken cancellationToken = default)
        {
            if (newBroadcast is null)
            {
                throw new ArgumentNullException(nameof(newBroadcast));
            }

            if (broadcastSet.Contains(newBroadcast))
            {
                throw new InvalidOperationException("O broadcast já está cadastrado");
            }

            broadcastSet.Add(newBroadcast);

            return Task.CompletedTask;
        }

        public Task DeleteBroadcast(Guid id, CancellationToken cancellationToken = default)
        {
            return DeleteBroadcastInternalAsync(id, cancellationToken);
        }

        private async Task DeleteBroadcastInternalAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingBroadcast = await GetBroadcast(id, cancellationToken);
            if (existingBroadcast is null)
            {
                throw new InvalidOperationException("O broadcast não existe");
            }

            broadcastSet.Remove(existingBroadcast);
        }

        public ValueTask<BroadcastMessage> GetBroadcast(Guid id, CancellationToken cancellationToken = default)
        {
            var existingBroadcast = broadcastSet.Where(b => b.MessageId == id).SingleOrDefault();
            return new ValueTask<BroadcastMessage>(existingBroadcast);
        }

        public ValueTask<BroadcastMessage> GetLatestBroadcast(CancellationToken cancellationToken = default)
        {
            var latestBroadcast = broadcastSet.OrderByDescending(b => b.DthCriacao).FirstOrDefault();
            return new ValueTask<BroadcastMessage>(latestBroadcast);
        }

        public ValueTask<IEnumerable<BroadcastMessage>> ListBroadcasts(CancellationToken cancellationToken = default)
        {
            return new ValueTask<IEnumerable<BroadcastMessage>>(broadcastSet.ToList());
        }

        public Task UpdateBroadcast(Guid id, BroadcastMessage updatedBroadcast, CancellationToken cancellationToken = default)
        {
            if (updatedBroadcast is null)
            {
                throw new ArgumentNullException(nameof(updatedBroadcast));
            }

            updatedBroadcast.MessageId = id;

            return UpdateBroadcastInternalAsync(updatedBroadcast, cancellationToken);
        }

        private async Task UpdateBroadcastInternalAsync(BroadcastMessage updatedBroadcast, CancellationToken cancellationToken)
        {
            var existingBroadcast = await GetBroadcast(updatedBroadcast.MessageId, cancellationToken);
            if (existingBroadcast is null)
            {
                throw new InvalidOperationException("O broadcast não existe");
            }

            broadcastSet.Remove(existingBroadcast);
            broadcastSet.Add(updatedBroadcast);
        }
    }
}
