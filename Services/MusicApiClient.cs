using System;
using System.Net.Http;
using System.Threading.Tasks;
using MetaBrainz.MusicBrainz;

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
        }

        public async Task<string> SearchArtistDataAsync(string artistName)
        {
            var artistData = await client.GetAsync("artist/cc197bad-dc9c-440d-a5b5-d52ba2e14234");
            var data = await artistData.Content.ReadAsStringAsync();
            return data;
        }
    }
}