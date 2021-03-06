﻿using Microsoft.Extensions.Logging;
using Realtime.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Stores.InMemory
{
    /// <summary>
    /// Implementação da IMessageStore utilizando HashSet<T>.
    /// </summary>
    public class MessageHashStore : IMessageStore
    {
        private readonly ICollection<UserMessage> messageSet;
        private readonly ILogger<MessageHashStore> logger;

        public MessageHashStore(ILogger<MessageHashStore> logger)
        {
            this.logger = logger;
            messageSet = new HashSet<UserMessage>();
        }

        public Task AddMessage(UserMessage newMessage, CancellationToken cancellationToken = default)
        {
            if (newMessage is null)
            {
                throw new ArgumentNullException(nameof(newMessage));
            }

            if (messageSet.Contains(newMessage))
            {
                throw new InvalidOperationException("A mensagem já está cadastrada");
            }

            messageSet.Add(newMessage);

            return Task.CompletedTask;
        }

        public Task DeleteMessage(Guid id, CancellationToken cancellationToken = default)
        {
            return DeleteMessageInternalAsync(id, cancellationToken);
        }

        private async Task DeleteMessageInternalAsync(Guid id, CancellationToken cancellationToken)
        {
            var existingMessage = await GetMessage(id, cancellationToken);
            if (existingMessage is null)
            {
                throw new InvalidOperationException("A mensagem não existe");
            }

            messageSet.Remove(existingMessage);
        }

        public ValueTask<UserMessage> GetMessage(Guid id, CancellationToken cancellationToken = default)
        {
            var existingMessage = messageSet.Where(m => m.MessageId == id).SingleOrDefault();
            return new ValueTask<UserMessage>(existingMessage);
        }

        public ValueTask<IEnumerable<UserMessage>> ListMessages(CancellationToken cancellationToken = default)
        {
            var messages = messageSet.OrderByDescending(m => m.DthProcessamento).ToList();
            return new ValueTask<IEnumerable<UserMessage>>(messages);
        }
    }
}
