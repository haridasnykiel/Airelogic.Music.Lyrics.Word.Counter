using System.Collections.Generic;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public interface ILyricsApiClient
    {
        Task<LyricsResponse> GetLyricsAsync(string artist, string songTitle);
    }
}