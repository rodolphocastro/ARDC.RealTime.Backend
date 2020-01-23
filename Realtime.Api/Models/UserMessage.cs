using System;

namespace Realtime.Api.Models
{
    /// <summary>
    /// Mensagem publicada por um usuário para outros usuários.
    /// </summary>
    public class UserMessage
    {
        /// <summary>
        /// Identificador único da mensagem.
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Conteúdo da mensagem.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Data e Hora em que a mensagem foi processada pelo servidor.
        /// </summary>
        public DateTimeOffset? DthProcessamento { get; set; }
    }
}
