using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyPlaylistToDiscord.Resources
{
    public class YoutubeResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public PageInfo PageInfo { get; set; }
        public List<YoutubeItem> Items { get; set; }
    }

    public class PageInfo
    {
        public int TotalResults { get; set; }
        public int ResultsPerPage { get; set; }
    }

    public class YoutubeItem
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public YoutubeSnippet Snippet { get; set; }
    }

    public class YoutubeSnippet
    {
        public DateTime PublishedAt { get; set; }
        public string ChannelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ThumbnailCollection Thumbnails { get; set; }
        public string ChannelTitle { get; set; }
        public List<string> Tags { get; set; }
        public string CategoryId { get; set; }
        public string LiveBroadcastContent { get; set; }
        public LocalizedData Localized { get; set; }
    }

    public class ThumbnailCollection
    {
        public YoutubeThumbnail Default { get; set; }
        public YoutubeThumbnail Medium { get; set; }
        public YoutubeThumbnail High { get; set; }
        public YoutubeThumbnail Standard { get; set; }
        public YoutubeThumbnail Maxres { get; set; }
    }

    public class YoutubeThumbnail
    {
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class LocalizedData
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
