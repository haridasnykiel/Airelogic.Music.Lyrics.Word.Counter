using System.Collections.Generic;
using System.Linq;
using Music.Lyrics.Word.Counter.Extensions;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public class LyricsHandler : ILyricsHandler
    {
        public double GetLyricsAverage(List<Song> songs) 
        {
            return songs
                .Select(s => GetSongWordCount(s.Lyrics))
                .Average()
                .Truncate(decimals: 2);
        }

        public int GetMaximumLyrics(List<Song> songs) 
        {
            return songs.Max(s => GetSongWordCount(s.Lyrics));
        }

        public int GetMinimumLyrics(List<Song> songs) 
        {
            return songs.Min(s => GetSongWordCount(s.Lyrics));
        }

        public double GetStandardDeviationOfLyrics(List<Song> songs) 
        {
            return songs
                .Select(s => GetSongWordCount(s.Lyrics))
                .StandardDeviation()
                .Truncate(decimals: 2);
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