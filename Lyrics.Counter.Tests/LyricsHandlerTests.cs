using System.Collections.Generic;
using FluentAssertions;
using Lyrics.Counter.Tests.TestData;
using Music.Lyrics.Word.Counter.Models;
using Music.Lyrics.Word.Counter.Services;
using NUnit.Framework;

namespace Lyrics.Counter.Tests
{
    public class LyricsHandlerTests
    {
        private ILyricsHandler _sut = new LyricsHandler();

        [Test]
        public void GetLyricsAverage_WithFiveSongs() 
        {
            var songs = SongsTestData.FiveSongs();
            
            var result = _sut.GetLyricsAverage(songs);

            result.Should().Be(5);
        }
    }
}