using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using MetaBrainz.MusicBrainz;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class MusicApiClient
    {
        HttpClient client;

        public MusicApiClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://musicbrainz.org/ws/2/")
            };
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Artist> GetArtistAsync(string artistName)
        {
            var artistData = await client.GetAsync($"artist/?query=artist:{artistName}");
            var stream = await artistData.Content.ReadAsStreamAsync();
            var artistList = await JsonSerializer.DeserializeAsync<ArtistList>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return artistList.Artists[0];
        }
    }
}