using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using SpotifyAPI.Web;

using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;

using Victoria;

using SpotifyPlaylistToDiscord.Core;
using SpotifyPlaylistToDiscord.Resources.Extensions;

namespace SpotifyPlaylistToDiscord
{
    class Program
    {
        SpotifyClient SpotifyClient;
        DiscordSocketClient Client;
        CommandService Commands;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            //var Config = SpotifyClientConfig
            //                .CreateDefault()
            //                .WithAuthenticator(new ClientCredentialsAuthenticator(Storage.SpotifyClientID, Storage.SpotifyClientSecret));

            //SpotifyClient = new SpotifyClient(Config);

            Storage.Load();
            Logging.LogSucces("Bot", $"bot development version");

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                ConnectionTimeout = 10000,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                HandlerTimeout = null,
                LogLevel = Storage.Debugging ? LogSeverity.Debug : LogSeverity.Info
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                IgnoreExtraArgs = true,
                LogLevel = Storage.Debugging ? LogSeverity.Debug : LogSeverity.Info
            });

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;
            Client.MessageReceived += Client_MessageReceived;

            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            await Client.LoginAsync(TokenType.Bot, Storage.Token);
            await Client.StartAsync();

            //var Christmas = await SpotifyClient.Playlists.Get("");

            //foreach (var Track in Christmas.Tracks.Items)
            //{
            //    FullTrack T = Track.Track as FullTrack;
            //    Console.Write("!play " + T.Name + " " + T.Artists[0].Name + "|");
            //}

            await Task.Delay(-1);
        }

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            var Message = arg as SocketUserMessage;
            var Context = new SocketCommandContext(this.Client, Message);
            var ArgPos = 0;

            if (Message.HasStringPrefix("!", ref ArgPos))
            {
                try
                {
                    IResult Res = await Commands.ExecuteAsync(Context, ArgPos, null);

                    if (!Res.IsSuccess)
                        Console.WriteLine($"Executing the command went wrong with error: {Res.Error} - {Res.ErrorReason}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not process the message as command: {ex.Message} - {ex.StackTrace}");
                }
            }
        }

        private async Task Client_Log(LogMessage Message)
        {
            string Addition = Storage.Debugging && Message.Exception != null ? " | Exception: " + Message.Exception.Message : "";
            Logging.Log(Message.Source, Message.Message + Addition, (Message.Exception == null) ? null : Message.Exception.StackTrace);
        }

        private async Task Client_Ready()
        {
            Logging.LogSucces("INFO", $"The bot has started and is logged in as user '{Client.CurrentUser}'");

            try
            {
                // Configure lavalink
                AudioStore.LConfig = new LavaConfig();
                AudioStore.LConfig.SelfDeaf = false;
                AudioStore.LNode = new LavaNode(Client, AudioStore.LConfig);

                AudioStore.LNode.OnLog += Client_Log;
                AudioStore.LNode.OnTrackEnded += LNode_OnTrackEnded;

                if (!AudioStore.LNode.IsConnected)
                    await AudioStore.LNode.ConnectAsync();

                Logging.LogSucces("LAVALINK", "Setup of the lavalink connection as succesful");
            } catch (Exception ex)
            {
                Logging.LogError("LAVALINK", $"Could not setup the lavalink connection", ex.StackTrace);
            }

            Logging.LogSucces("INFO", $"The bot has started and is logged in as user '{Client.CurrentUser}'");
        }

        private async Task LNode_OnTrackEnded(Victoria.EventArgs.TrackEndedEventArgs arg)
        {
            // CHeck if there is a next track to be played
            if (!arg.Reason.ShouldPlayNext())
                return;

            // Get the current player
            var Player = arg.Player;
            if (!Player.Queue.TryDequeue(out var Queueable))
            {
                await Player.TextChannel.SendMessageAsync(":warning: Queue completed! Please add more songs to it, or I will go silent... forever...");
                return;
            }

            if (!(Queueable is LavaTrack Track))
            {
                await Player.TextChannel.SendMessageAsync(":x: The next queue item is not a song: " + Queueable.Title);
                return;
            }

            // Play the next song
            await Player.PlayAsync(Queueable);
            await Player.UpdateVolumeAsync(60);
            await Player.TextChannel.SendMessageAsync($":white_check_mark: Playing {Queueable.Title}");
        }
    }
}