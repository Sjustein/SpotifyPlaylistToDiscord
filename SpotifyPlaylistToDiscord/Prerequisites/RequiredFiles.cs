using System;
using System.IO;

using Newtonsoft.Json;

using SpotifyPlaylistToDiscord.Core;
using SpotifyPlaylistToDiscord.Resources.Models;
using SpotifyPlaylistToDiscord.Resources.Extensions;

namespace SpotifyPlaylistToDiscord.Prerequisites
{
    public static class RequiredFiles
    {
        //Ensure that all required files exist
        public static bool EnsureExist()
        {
            try
            {
                //Check the configuration file
                if (!Directory.Exists(Storage.RootDir + "Assets"))
                    Directory.CreateDirectory(Storage.RootDir + "Assets");

                if (!Directory.Exists(Storage.RootDir + "Assets|Configuration".OSPath()))
                    Directory.CreateDirectory(Storage.RootDir + "Assets|Configuration".OSPath());

                if (!File.Exists(Storage.RootDir + "Assets|Configuration|Configuration.json".OSPath()))
                    File.WriteAllText(Storage.RootDir + "Assets|Configuration|Configuration.json".OSPath(), JsonConvert.SerializeObject(new Configuration()));

                return true;
            }
            catch (Exception ex)
            {
                Logging.LogError("PREREQUISITES", $"Error while ensuring that all required files and folders exist {ex.Message}", ex.StackTrace);
                return false;
            }
        }
    }
}
