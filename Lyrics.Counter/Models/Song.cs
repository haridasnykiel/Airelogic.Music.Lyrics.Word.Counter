
namespace Music.Lyrics.Word.Counter.Models
{
    public class Song
    {
        public string Title { get; private set; }
        public string Lyrics { get; private set; }

        public Song AddTitle(string title)
        {
            Title = title;
            return this;
        }

        public Song AddLyrics(string lyrics)
        {
            Lyrics = lyrics;
            return this;
        }
    }
}