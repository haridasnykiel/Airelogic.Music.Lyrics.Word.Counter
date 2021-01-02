using System.Collections.Generic;
using Music.Lyrics.Word.Counter.Models;

namespace Music.Lyrics.Word.Counter.Services
{
    public interface ILyricsHandler
    {
        double GetLyricsAverage(List<Song> songs);
    }
}