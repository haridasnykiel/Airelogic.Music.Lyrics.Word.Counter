using System.Collections.Generic;
using System.Linq;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Helpers
{
    public static class SongHelpers
    {
        public static List<Song> ConvertWorks(List<Work> works)
        {
            var filteredWorks = works.Where(w => w.Type == "Song");
            var songs = new List<Song>();
            songs.AddRange(filteredWorks.Select(w => new Song { Title = w.Title }));
            return songs;
        }
    }
}