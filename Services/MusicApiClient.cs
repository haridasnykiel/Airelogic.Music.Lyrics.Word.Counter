using System;
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

        public async Task<Artist> SearchArtistAsync(string artistName)
        {
            var artistData = await client.GetAsync("artist/cc197bad-dc9c-440d-a5b5-d52ba2e14234");
            var stream = await artistData.Content.ReadAsStreamAsync();
            var artist = await JsonSerializer.DeserializeAsync<Artist>(stream);
            return artist;
        }
    }
}