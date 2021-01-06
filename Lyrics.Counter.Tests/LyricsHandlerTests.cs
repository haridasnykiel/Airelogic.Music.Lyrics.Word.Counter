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

        [Test]
        public void GetMaximumLyricsNumber_WithFiveSongs() 
        {
            var songs = SongsTestData.FiveSongs();
            
            var result = _sut.GetMaximumLyrics(songs);

            result.Should().Be(8);
        }

        [Test]
        public void GetMinimumLyricsNumber_WithFiveSongs() 
        {
            var songs = SongsTestData.FiveSongs();
            
            var result = _sut.GetMinimumLyrics(songs);

            result.Should().Be(2);
        }

        [Test]
        public void GetStandardDeviationOfLyrics_WithFiveSongs() 
        {
            var songs = SongsTestData.FiveSongs();
            
            var result = _sut.GetStandardDeviationOfLyrics(songs);

            result.Should().Be(2.28);
        }
    }
}