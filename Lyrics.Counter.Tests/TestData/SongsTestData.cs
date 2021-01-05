using System.Collections.Generic;
using Music.Lyrics.Word.Counter.Models;

namespace Lyrics.Counter.Tests.TestData
{
    public static class SongsTestData
    {
        public static List<Song> FiveSongs() 
        {
            var songOne = new Song();
            var songTwo = new Song();
            var songThree = new Song();
            var songFour = new Song();
            var songFive = new Song();

            songOne.AddTitle("one");
            songTwo.AddTitle("two");
            songThree.AddTitle("three");
            songFour.AddTitle("four");
            songFive.AddTitle("five");
            songOne.AddLyrics("Some Lyrics");
            songTwo.AddLyrics("Some Other Lyrics");
            songThree.AddLyrics("Even more lyrics and stuff");
            songFour.AddLyrics("sing sing sing sing sing sing sing sing");
            songFive.AddLyrics("loud loud loud loud loud loud loud");

            var songs = new List<Song>();
            songs.Add(songOne);
            songs.Add(songTwo);
            songs.Add(songThree);
            songs.Add(songFour);
            songs.Add(songFive);

            return songs;
        }
    }
}