using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Broadcast.Queries;
using Realtime.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Realtime.Api.Controllers
{
    /// <summary>
    /// Controller para acesso público dos Broadcasts.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BroadcastsController : ControllerBase
    {
        private readonly ILogger<BroadcastsController> logger;
        private readonly IMediator mediator;

        public BroadcastsController(ILogger<BroadcastsController> logger, IMediator mediator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Busca todos os broadcasts cadastrados no sistema.
        /// </summary>
        /// <returns>uma lista de broadcasts</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BroadcastMessage>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await mediator.Send(new ListBroadcasts());
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar broadcasts");
                throw;
            }
        }

        /// <summary>
        /// Busca um broadcast específico no sistema.
        /// </summary>
        /// <param name="id">id do broadcast buscado</param>
        /// <returns>o broadcast encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BroadcastMessage))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var result = await mediator.Send(new GetBroadcast(id));
                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar um broadcast específico");
                throw;
            }
        }

        /// <summary>
        /// Busca o broadcast mais recente no sistema.
        /// </summary>
        /// <returns>o broadcast encontrado</returns>
        [HttpGet("latest")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BroadcastMessage))]
        public async Task<IActionResult> GetLatest()
        {
            try
            {
                var result = await mediator.Send(new GetLatestBroadcast());
                if (result is null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar o broadcast mais recente");
                throw;
            }
        }
    }
}
