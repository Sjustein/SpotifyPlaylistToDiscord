using System.IO;

using Discord.Audio;

using Victoria;

namespace SpotifyPlaylistToDiscord
{
    public static class AudioStore
    {
        public static AudioOutStream DiscordStream;
        public static Stream FfmpegOut;
        public static LavaNode LNode;
        public static LavaConfig LConfig;
    }
}
