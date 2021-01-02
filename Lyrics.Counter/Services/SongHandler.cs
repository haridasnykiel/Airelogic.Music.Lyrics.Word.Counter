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

        public async Task<List<Song>> GetSongsAsync(Artist artist)
        {
            int limit = 100;
            int offset = 0;
            var songs = new List<Song>();
            WorksResponse worksResponse;

            do
            {
                worksResponse = await _musicInfoClient.GetWorksAsync(artist.Id, limit, offset);
                var workBatch = worksResponse.Works.Where(w => w.Type == "Song");
                var songsWithLyrics = await AddLyrics(artist.Name, workBatch);
                songs.AddRange(songsWithLyrics);
                offset += limit;
            } while (worksResponse.Works.Count == limit);

            return songs;
        }


        private async Task<List<Song>> AddLyrics(string artistName, IEnumerable<Work> workBatch)
        {
            var songs = new List<Song>();

            foreach (var work in workBatch)
            {
                var lyrics = await _lyricsApiClient.GetLyricsAsync(artistName, work.Title);
                if(lyrics == null) 
                {
                    continue;
                }
                
                if (!string.IsNullOrEmpty(lyrics.Lyrics))
                {
                    var song = new Song();
                    song.AddTitle(work.Title);
                    song.AddLyrics(lyrics.Lyrics);
                    songs.Add(song);
                }
            }
            return songs;
        }


    }
}