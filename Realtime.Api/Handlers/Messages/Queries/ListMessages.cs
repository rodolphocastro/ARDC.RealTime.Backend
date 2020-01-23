using MediatR;
using Realtime.Api.Models;
using System.Collections.Generic;

namespace Realtime.Api.Handlers.Messages.Queries
{
    /// <summary>
    /// Query para listar todas as mensagens.
    /// </summary>
    public class ListMessages : IRequest<IEnumerable<UserMessage>>
    {
    }
}
