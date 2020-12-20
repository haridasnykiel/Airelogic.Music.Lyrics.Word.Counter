using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class MusicInfoApiClient : IMusicInfoApiClient
    {
        HttpClient _client;

        public MusicInfoApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Artist> GetArtistAsync(string artistName)
        {
            var artistData = await _client.GetAsync($"artist/?query=artist:{artistName}");

            if(artistData.IsSuccessStatusCode) 
            {
                var stream = await artistData.Content.ReadAsStreamAsync();
                var artistsResponse = await JsonSerializer.DeserializeAsync<ArtistsResponse>(stream, 
                                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return artistsResponse.Artists.FirstOrDefault();
            }

            return null;
        }

        public async Task<WorksResponse> GetWorksAsync(string artistId, int limit, int offset)
        {
            var worksData = await _client.GetAsync($"work?artist={artistId}&limit={limit}&offset={offset}");

            if(worksData.IsSuccessStatusCode) {
                var stream = await worksData.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<WorksResponse>(stream, 
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return null;
        }
    }
}