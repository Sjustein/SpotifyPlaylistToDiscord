using SpotifyPlaylistToDiscord.Resources.Enums;

namespace SpotifyPlaylistToDiscord.Resources.Extensions
{
    public static class StringExtensions
    {
        public static string OSPath(this string Path, string OldValue, string NewValue)
            => Path.Replace(OldValue, NewValue).OSPath();

        public static string OSPath(this string Path)
        {
            if (Storage.OS == OS.Unix)
            {
                //The path needs to have / slashes
                return Path.Replace('|', '/');
            }
            else
            {
                //The path needs to have \ slashes
                return Path.Replace('|', '\\');
            }
        }
    }
}
