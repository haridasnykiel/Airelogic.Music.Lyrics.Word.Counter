using System.Collections.Generic;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public interface IMusicInfoApiClient
    {
        Task<Artist> GetArtistAsync(string artistName);
        Task<WorksResponse> GetWorksAsync(string artistId, int limit, int offset);
    }
}