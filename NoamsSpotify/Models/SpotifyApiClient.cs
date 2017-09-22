using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NoamsSpotify.Models
{
    public class SpotifyApiClient
    {

        private const string ClientId = "996d0037680544c987287a9b0470fdbb";
        private const string ClientSecret = "5a3c92099a324b8f9e45d77e919fec13";

        protected const string BaseUrl = "https://api.spotify.com/";
        private HttpClient GetDefaultClient()
        {
            var authHandler = new SpotifyAuthClientCredentialsHttpMessageHandler(
                ClientId,
                ClientSecret,
                new HttpClientHandler());

            var client = new HttpClient(authHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            return client;
        }

        public async Task<SearchArtistResponse> SearchArtistsAsync(string artistName, int? limit = null, int? offset = null)
        {
            var client = GetDefaultClient();

            var url = new Url("/v1/search");
            url = url.SetQueryParam("q", artistName);
            url = url.SetQueryParam("type", "artist");

            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            if (offset != null)
                url = url.SetQueryParam("offset", offset);

            var response = await client.GetStringAsync(url);

            var artistResponse = JsonConvert.DeserializeObject<SearchArtistResponse>(response);


            return artistResponse;
        }

        public async Task<List<string>> SearchArtistsNameAsync(string artistName, int? limit = null, int? offset = null)
        {
            var client = GetDefaultClient();

            var url = new Url("/v1/search");
            url = url.SetQueryParam("q", artistName);
            url = url.SetQueryParam("type", "artist");

            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            if (offset != null)
                url = url.SetQueryParam("offset", offset);

            var response = await client.GetStringAsync(url);

            var artistResponse = JsonConvert.DeserializeObject<SearchArtistResponse>(response);

            var artistsName = artistResponse.Artists.Items.ToList();
            List<string> artistNames = new List<string>();

            foreach (var item in artistsName)
            {
                artistNames.Add(item.Name);
            }
            return artistNames;
        }

        public async Task<List<string>> GetArtistsGangers(List<string> artistsName, int? limit = null, int? offset = null) //ToDo see if i can get artists by ganer
        {
            List<string> genrsList = new List<string>();
            var client = GetDefaultClient();

            var url = new Url("/v1/search");
            foreach (var artist in artistsName)
            {


                url = url.SetQueryParam("q", artist);
                url = url.SetQueryParam("type", "artist");

                if (limit != null)
                    url = url.SetQueryParam("limit", limit);

                if (offset != null)
                    url = url.SetQueryParam("offset", offset);

                var response = await client.GetStringAsync(url);

                var artistResponse = JsonConvert.DeserializeObject<SearchArtistResponse>(response);
                var artists = artistResponse.Artists.Items.ToList();
                foreach (var item in artists)
                {
                    foreach (var genre in item.Genres)
                    {
                        genrsList.Add(genre.ToString());
                    }
                }

            }
            return genrsList;
        }

        public async Task<List<string>> GetRelatedArtists(List<string> artistsName, int? limit = 3, int? offset = null) //ToDo see if i can get artists by ganer
        {

            List<string> relatedArtistsList = new List<string>();
            var client = GetDefaultClient();

            foreach (var artistId in artistsName)
            {
                var artist = await SearchArtistsAsync(artistId);

                var url = new Url("/v1/artists/" + artist.Artists.Items[0].Id + "/related-artists");


                url = url.SetQueryParam("type", "artist");

                if (limit != null)
                    url = url.SetQueryParam("limit", limit);

                if (offset != null)
                    url = url.SetQueryParam("offset", offset);

                var response = await client.GetStringAsync(url);

                var artistResponse = JsonConvert.DeserializeObject<RelatesArtists>(response);
                // var artists = artistResponse.Artists.   Name.ToList();
                foreach (var index in artistResponse.Artists)
                {

                    relatedArtistsList.Add(index. Name.ToString());

                }


            }
            return relatedArtistsList;
        }

        public async Task<RootObject> GetResult(List<string> genrs, List<string> relatedArtists, string popularity, string limit) //ToDo see if i can get artists by ganer
        {
            string artistsString = null;
            string genrsString = null;
            var url = new Url("/v1/recommendations");

            if (relatedArtists != null)
            {
                foreach (var artist in relatedArtists)
                {
                    var artisId = await SearchArtistsAsync(artist);
                    artistsString += artisId.Artists.Items[0].Id + ",";
                }
                artistsString = artistsString.Remove(artistsString.Length - 1);
                url = url.SetQueryParam("seed_artists", artistsString);
            }
            else
            {
                url = url.SetQueryParam("seed_artists", "");

            }

            if (genrs != null)
            {
                foreach (var gener in genrs)
                {
                    genrsString += gener + ",";
                }
                genrsString = genrsString.Remove(genrsString.Length - 1);
                url = url.SetQueryParam("seed_genres", genrsString);
            }
            else
            {
                url = url.SetQueryParam("seed_genres", "");

            }


            var client = GetDefaultClient();
            if (popularity != null)
                url = url.SetQueryParam("max_popularity", popularity);
            url = url.SetQueryParam("market", "SE");

            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            var response = await client.GetStringAsync(url);
            RootObject artistResponse = JsonConvert.DeserializeObject<RootObject>(response);

            return artistResponse;
        }
    }
}
