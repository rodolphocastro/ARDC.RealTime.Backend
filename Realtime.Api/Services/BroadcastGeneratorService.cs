using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Broadcast.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Realtime.Api.Services
{
    /// <summary>
    /// HostedService para a geração de Broadcasts.
    /// </summary>
    public class BroadcastGeneratorService : IHostedService, IDisposable
    {
        private readonly TimeSpan runDelay = TimeSpan.FromMinutes(2);
        private readonly ILogger<BroadcastGeneratorService> logger;
        private readonly IMediator mediator;
        private Timer timer;

        public BroadcastGeneratorService(ILogger<BroadcastGeneratorService> logger, IMediator mediator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Broadcast Generator está iniciando");
            logger.LogInformation($"Configurado para executar a cada {runDelay.TotalMinutes} minutos");

            timer = new Timer(async (s) => await RunAsync(s), null, TimeSpan.Zero, runDelay);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Broadcast Generator está finalizando");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private Task RunAsync(object state)
        {
            logger.LogInformation("Gerando novo broadcast");
            var createTask = mediator.Send(new CreateBroadcast("Titulo", "Conteudo!"));
            return createTask;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
