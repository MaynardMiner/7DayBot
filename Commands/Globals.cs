using DSharpPlus;
using Newtonsoft.Json;

namespace Commands.Globals
{
    public static class Global
    {
        public static volatile DiscordClient cl;
    }

    public static class Config
    {
        public static volatile string AppName;

        public static volatile bool NeedsEnter;

        public static volatile string Ip;

        public static volatile int Port;

        public static string AppPath;
    }

    public class ConfigJson
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("Prefix")]
        public string CommandPrefix { get; set; }

        [JsonProperty("Application Name")]
        public string AppName { get; set; }

        [JsonProperty("Tap Enter To Close")]
        public bool NeedsEnter { get; set; }

        [JsonProperty("Ip Address")]
        public string Ip { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }

        [JsonProperty("Application Shortcut Path")]
        public string AppPath { get; set; }
    }
}
