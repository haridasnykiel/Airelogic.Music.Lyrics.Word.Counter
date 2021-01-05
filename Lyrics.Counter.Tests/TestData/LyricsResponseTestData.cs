using Music.Lyrics.Word.Counter.Models;

namespace Lyrics.Counter.Tests.TestData
{
    public static class LyricsResponseTestData
    {
        public static LyricsResponse ResponseWithLyrics ()
        {

            return new LyricsResponse
            {
                Lyrics = "Some Lyrics"
            };
        }
    }
}