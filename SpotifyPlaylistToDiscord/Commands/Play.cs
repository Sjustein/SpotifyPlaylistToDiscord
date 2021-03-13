using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Victoria;
using Victoria.Enums;

using RestSharp;

using Newtonsoft.Json;

using SpotifyPlaylistToDiscord.Resources;

namespace SpotifyPlaylistToDiscord.Commands
{
    public class Play : ModuleBase<SocketCommandContext>
    {
        public LavaPlayer Player = null;

        [Command("test")]
        public async Task PlayCommand([Remainder] string SearchTerm = null)
        {
            IVoiceChannel Channel = (Context.User as IGuildUser).VoiceChannel;
            if (Channel == null)
            {
                await Context.Channel.SendMessageAsync(":x: You are not in a voice channel! Join one and hit the play button again :notes:");
                return;
            }

            if (SearchTerm == null)
            {
                await Context.Channel.SendMessageAsync(":x: Please enter a search term! The command should be used like: !play <search_term> :mag:");
                return;
            }

            try
            {
                PlayAsync(Context.User as SocketGuildUser, SearchTerm);
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(ex.Message);
                await Context.Channel.SendMessageAsync(ex.StackTrace);
            }

        }

        [Command("plays"), Summary("Load a spotify playlist")]
        public async Task Plays([Remainder]string Remaind = null)
        {

        }

        private async Task PlayAsync(SocketGuildUser User, string VideoTitle)
        {
            LavaPlayer Player = null;
            var VoiceChannel = User.VoiceChannel;

            if (!AudioStore.LNode.HasPlayer(Context.Guild))
            {
                try
                {
                    await AudioStore.LNode.JoinAsync(VoiceChannel, Context.Channel as ITextChannel);
                }
                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return;
                }
            }

            Player = AudioStore.LNode.GetPlayer(Context.Guild);

            var Search = await AudioStore.LNode.SearchYouTubeAsync(VideoTitle);

            switch (Search.LoadStatus)
            {
                case LoadStatus.LoadFailed:
                    await Context.Channel.SendMessageAsync($":x: Something went wrong with downloading the video from youtube! Please try it again!");
                    return;
                case LoadStatus.NoMatches:
                    await Context.Channel.SendMessageAsync($":x: Could not find that song!");
                    return;
            }


            if (Search.LoadStatus == LoadStatus.TrackLoaded || Search.LoadStatus == LoadStatus.SearchResult)
                await EnqueueSong(Player, Search.Tracks[0]);
            else if (Search.LoadStatus == LoadStatus.PlaylistLoaded)
                foreach (var Track in Search.Tracks)
                    await EnqueueSong(Player, Track);
        }

        private async Task EnqueueSong(LavaPlayer Player, LavaTrack Track)
        {
            //Check if the player is playing
            if (Player.Track != null && Player.PlayerState == PlayerState.Playing || Player.PlayerState == PlayerState.Paused)
            {
                //The player is currently playing a song
                Player.Queue.Enqueue(Track);

                await Context.Channel.SendMessageAsync($":alarm_clock: Put {Track.Title} in the queue! Use =skip to skip the current song!");
                return;
            }
            else
            {
                await Player.PlayAsync(Track);
                await Player.UpdateVolumeAsync(60);
                await Context.Channel.SendMessageAsync($":white_check_mark: Playing {Track.Title}");
            }
        }
    }
}
