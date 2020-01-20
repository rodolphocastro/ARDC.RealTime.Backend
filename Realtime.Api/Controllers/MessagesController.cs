using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Realtime.Api.Handlers.Messages.Commands;
using Realtime.Api.Handlers.Messages.Queries;
using Realtime.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Realtime.Api.Controllers
{
    /// <summary>
    /// Controller para acesso público a mensagens.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> logger;
        private readonly IMediator mediator;

        public MessagesController(ILogger<MessagesController> logger, IMediator mediator)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Busca todas as mensagens cadastradas no sistema.
        /// </summary>
        /// <returns>uma lista de mensagens</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserMessage>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await mediator.Send(new ListMessages());
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao listar mensagens");
                throw;
            }
        }

        /// <summary>
        /// Busca uma mensagem específica no sistema.
        /// </summary>
        /// <param name="id">id da mensagem a ser buscada</param>
        /// <returns>a mensagem encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserMessage))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var result = await mediator.Send(new GetMessage(id));
                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar mensagem específica");
                throw;
            }
        }

        /// <summary>
        /// Cria uma nova mensagem no sistema.
        /// </summary>
        /// <param name="newMessage">a mensagem a ser criada</param>
        /// <returns>a mensagem criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserMessage))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserMessage newMessage)
        {
            try
            {
                var result = await mediator.Send(new CreateMessage(newMessage));
                return CreatedAtAction(nameof(GetById), new { result.MessageId }, result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao criar nova mensagem");
                throw;
            }
        }

        /// <summary>
        /// Deleta uma mensagem do sistema.
        /// </summary>
        /// <param name="id">id da mensagem a ser removida</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await mediator.Send(new DeleteMessage(id));
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                // Operação inválida, Mensagem não encontrada
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao deletar mensagem");
                throw;
            }
        }
    }
}
