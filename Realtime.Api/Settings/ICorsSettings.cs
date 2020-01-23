namespace Realtime.Api.Settings
{
    /// <summary>
    /// Configurações do Cors para a aplicação.
    /// </summary>
    public interface ICorsSettings
    {
        /// <summary>
        /// Verbos HTTP permitidos.
        /// </summary>
        public string[] AllowedMethods { get; set; }

        /// <summary>
        /// Origens permitidas.
        /// </summary>
        public string[] AllowedOrigins { get; set; }
    }
}
