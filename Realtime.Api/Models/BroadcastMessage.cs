using System;

namespace Realtime.Api.Models
{
    /// <summary>
    /// Mensagem de aviso para todos os usuários do Aplicativo.
    /// </summary>
    public class BroadcastMessage
    {
        /// <summary>
        /// Identificador único da mensagem.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Título da mensagem.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Conteúdo da mensagem.
        /// </summary>
        public string Content { get; set; }
    }
}
