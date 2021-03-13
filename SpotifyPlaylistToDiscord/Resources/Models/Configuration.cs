using Newtonsoft.Json;
namespace SpotifyPlaylistToDiscord.Resources.Models
{
    public class Configuration
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("spotifyclientid")]
        public string SpotifyClientID { get; set; }
        [JsonProperty("spotifyclientsecret")]
        public string SpotifyClientSecret { get; set; }
        [JsonProperty("youtubekey")]
        public string YoutubeKey { get; set; }
    }
}
