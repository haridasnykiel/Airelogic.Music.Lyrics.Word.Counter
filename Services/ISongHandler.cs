using System.Collections.Generic;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public interface ISongHandler
    {
        Task<List<Song>> GetSongsAsync(Artist artist);
    }
}