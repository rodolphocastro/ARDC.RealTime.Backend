namespace Realtime.Api.Settings
{
    public class CorsSettings : ICorsSettings
    {
        public const string DefaultCorsSettings = "DefaultCors";
        public const string SignalRCorsSettings = "SignalrCors";

        public string[] AllowedMethods { get; set; }
        public string[] AllowedOrigins { get; set; }
    }
}
