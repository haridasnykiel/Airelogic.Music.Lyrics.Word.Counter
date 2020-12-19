using System.Collections.Generic;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public interface IMusicApiClient
    {
        Task<Artist> GetArtistAsync(string artistName);
        Task<List<Song>> GetSongTitlesAsync(string artistId);
    }
}