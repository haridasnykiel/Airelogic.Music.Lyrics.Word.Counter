using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class SongHandler : ISongHandler
    {
        IMusicInfoApiClient _musicInfoClient;
        ILyricsApiClient _lyricsApiClient;
        public SongHandler(IMusicInfoApiClient musicInfoClient, ILyricsApiClient lyricsApiClient)
        {
            _musicInfoClient = musicInfoClient;
            _lyricsApiClient = lyricsApiClient;
        }

        public async Task<WorksResponse> GetArtistWorksAsync(string artistId)
        {
            int limit = 100;
            int offset = 0;
            WorksResponse reponse = new WorksResponse();
            WorksResponse worksBatch;

            do
            {
                worksBatch = await _musicInfoClient.GetWorksAsync(artistId, limit, offset);
                var allSongs = worksBatch.Works.Where(w => w.Type == "Song");
                reponse.Works.AddRange(allSongs);
                offset += limit;
            } while (worksBatch.Works.Count == limit);

            return reponse;
        }


        public async Task<IEnumerable<Song>> GetAllSongLyrics(string artistName, IEnumerable<Work> works)
        {
            var songsTasks = new List<Task<Song>>();

            foreach (var work in works)
            {
                var task = GetSongLyrics(artistName, work);
                songsTasks.Add(task);
            }

            var songs = await Task.WhenAll(songsTasks);

            return songs.Where(s => s != null);
        }

        private async Task<Song> GetSongLyrics(string artistName, Work work)
        {

            var lyrics = await _lyricsApiClient.GetLyricsAsync(artistName, work.Title);

            if (lyrics == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(lyrics.Lyrics))
            {
                return null;
            }

            var song = new Song();
            song.AddTitle(work.Title);
            song.AddLyrics(lyrics.Lyrics);

            return song;
        }


    }
}