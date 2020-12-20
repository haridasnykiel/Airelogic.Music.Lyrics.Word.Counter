using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class LyricsApiClient : ILyricsApiClient
    {
        HttpClient _client;
        public LyricsApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<LyricsResponse> GetLyricsAsync(string artist, string songTitle) 
        {
            var response = await _client.GetAsync($"{artist}/{songTitle}");

            if(response.IsSuccessStatusCode) 
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<LyricsResponse>(stream,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }   

            return null;
        }

    }
}