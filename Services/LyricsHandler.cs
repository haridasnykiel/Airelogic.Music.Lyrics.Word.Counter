using System.Linq;

namespace Music.Lyrics.Word.Counter.Services
{
    public class LyricsHandler : ILyricsHandler
    {
        public int LyricsCount(string lyrics) 
        {
            var lines = lyrics.Split("\n");
            int lyricCount = 0;

            foreach (var line in lines)
            {
                lyricCount += line.Split(" ").Count();
            }

            return lyricCount;
        }
    }
}