using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using MetaBrainz.MusicBrainz;
using Music.Lyrics.Word.Counter.Helpers;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class MusicApiClient : IMusicApiClient
    {
        HttpClient _client;

        public MusicApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Artist> GetArtistAsync(string artistName)
        {
            var artistData = await _client.GetAsync($"artist/?query=artist:{artistName}");
            var stream = await artistData.Content.ReadAsStreamAsync();
            var artistsResponse = await JsonSerializer.DeserializeAsync<ArtistsResponse>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return artistsResponse.Artists.FirstOrDefault();
        }

        public async Task<List<Song>> GetSongTitlesAsync(string artistId)
        {
            int limit = 100;
            int offset = 0;
            var songs = new List<Song>();
            var response = await GetWorksAsync(artistId, limit, offset);

            songs.AddRange(SongHelpers.ConvertWorks(response.Works));

            while (response.Works.Count == limit)
            {
                offset += limit;
                response = await GetWorksAsync(artistId, limit, offset);
                songs.AddRange(SongHelpers.ConvertWorks(response.Works));
            };

            return songs;
        }

        private async Task<WorksResponse> GetWorksAsync(string artistId, int limit, int offset)
        {
            var worksData = await _client.GetAsync($"work?artist={artistId}&limit={limit}&offset={offset}");
            var stream = await worksData.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<WorksResponse>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}