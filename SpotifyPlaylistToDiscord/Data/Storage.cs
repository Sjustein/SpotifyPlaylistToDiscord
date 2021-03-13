using System;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;

using SpotifyPlaylistToDiscord.Core;
using SpotifyPlaylistToDiscord.Prerequisites;
using SpotifyPlaylistToDiscord.Resources.Enums;
using SpotifyPlaylistToDiscord.Resources.Models;
using SpotifyPlaylistToDiscord.Resources.Extensions;

namespace SpotifyPlaylistToDiscord
{
    public static class Storage
    {
        // Constants
        public const bool Debugging = true;

        public static OS OS { get; private set; }
        public static string RootDir { get; private set; }
        public static string Token { get; private set; }

        // Configuration file
        public static string Prefix { get; private set; }
        public static string SpotifyClientID { get; private set; }
        public static string SpotifyClientSecret { get; private set; }
        public static string YoutubeKey { get; private set; }

        public static void Load()
        {
            //Load the configuration file into this static class
            SetOS(Environment.OSVersion.Platform);
            RootDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Replace("Run|netcoreapp3.1".OSPath(), "");

            if (!RequiredFiles.EnsureExist())
                Logging.LogErrorAndExit("CONFIGURATION", "Couldn't ensure that the right folder structure exists! Exiting...", 2);

            string ConfText = File.ReadAllText(RootDir + $"|Assets|Configuration|Configuration.json".OSPath());
            try
            {
                Configuration Conf = JsonConvert.DeserializeObject<Configuration>(ConfText);
                Token = Conf.Token;
                Prefix = Conf.Prefix;
                SpotifyClientID = Conf.SpotifyClientID;
                SpotifyClientSecret = Conf.SpotifyClientSecret;
                YoutubeKey = Conf.YoutubeKey;
            }
            catch (Exception ex)
            {
                Logging.LogError("CONFIGURATION", $"Couldn't load the configuration file: {ex.Message}", ex.StackTrace);
            }
        }

        private static void SetOS(PlatformID Platform)
        {
            if (Platform.ToString() == "Win32NT")
                OS = OS.Windows;
            else
                OS = OS.Unix;
        }
    }
}
