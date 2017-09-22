using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoamsSpotify.Models
{
    public class SearchArtistResponse
    {
        [JsonProperty("artists")]
        public SearchArtistCollection Artists { get; set; }
    }

    public class SearchArtistCollection
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public IList<Artist> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Artist
    {
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("genres")]
        public IList<object> Genres { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public IList<Image> Images { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string spotify { get; set; }
    }

    public class Image
    {

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }

    public class Album
    {
        [JsonProperty("album_type")]
        public string Album_type { get; set; }
        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }
        [JsonProperty("available_markets")]
        public List<string> Available_markets { get; set; }
        [JsonProperty("eternal_urls")]
        public ExternalUrls Eternal_urls { get; set; }
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class ExternalIds
    {
        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }

    public class Track
    {
        [JsonProperty("album")]
        public Album Album { get; set; }
        [JsonProperty("artists")]
        public List<Artist> Artists { get; set; }
        [JsonProperty("available_markets")]
        public List<string> Available_markets { get; set; }
        [JsonProperty("disc_number")]
        public int Disc_number { get; set; }
        [JsonProperty("duration_ms")]
        public int Duration_ms { get; set; }
        [JsonProperty("@explicit")]
        public bool @explicit { get; set; }
        [JsonProperty("external_ids")]
        public ExternalIds External_ids { get; set; }
        [JsonProperty("external_urls")]
        public ExternalUrls External_urls { get; set; }
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("popularity")]
        public int Popularity { get; set; }
        [JsonProperty("preview_url")]
        public string Preview_url { get; set; }
        [JsonProperty("track_number")]
        public int Track_number { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
    public class RootObject
    {
        [JsonProperty("tracks")]
        public List<Track> Tracks { get; set; }
    }
    public class RelatesArtists
    {
        [JsonProperty("artists")]
        public IList<Artist> Artists { get; set; }
    }
}