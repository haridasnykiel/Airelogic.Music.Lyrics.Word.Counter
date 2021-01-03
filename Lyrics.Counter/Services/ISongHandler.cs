using System.Collections.Generic;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public interface ISongHandler
    {
        Task<WorksResponse> GetArtistWorksAsync(string artistId);
        Task<IEnumerable<Song>> GetAllSongLyricsAsync(string artistName, IEnumerable<Work> workBatch);
    }
}