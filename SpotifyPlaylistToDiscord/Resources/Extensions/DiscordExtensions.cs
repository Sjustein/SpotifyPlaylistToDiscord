using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace SpotifyPlaylistToDiscord.Resources.Extensions
{
    public static class DiscordExtensions
    {
        public static async Task<IMessage> SendErrorAsync(this ISocketMessageChannel Channel, string Message)
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription(Message);
            Embed.WithColor(255, 0, 51);

            return await Channel.SendMessageAsync("", false, Embed.Build());
        }

        public static async Task<IMessage> SendSuccesAsync(this ISocketMessageChannel Channel, string Message)
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription(Message);
            Embed.WithColor(75, 181, 67);

            return await Channel.SendMessageAsync("", false, Embed.Build());
        }

        public static async Task<IMessage> SendAsync(this ISocketMessageChannel Channel, string Message)
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription(Message);

            return await Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
