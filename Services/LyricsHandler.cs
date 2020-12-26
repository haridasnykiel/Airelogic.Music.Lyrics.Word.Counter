using System.Collections.Generic;
using System.Linq;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class LyricsHandler : ILyricsHandler
    {
        public double GetLyricsAverage(List<Song> songs) 
        {
            return songs
                .Select(s => GetSongWordCount(s.Lyrics))
                .Average();
        }

        private int GetSongWordCount(string lyrics)
        {
            var lines = lyrics.Split("\n");
            int lyricCount = 0;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                lyricCount += line.Split(" ").Count();
            }

            return lyricCount;
        }
    }
}