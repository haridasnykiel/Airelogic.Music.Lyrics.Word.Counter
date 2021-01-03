using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Music.Lyrics.Word.Counter.Models;
using Music.Lyrics.Word.Counter.Services;
using NUnit.Framework;

namespace Lyrics.Counter.Tests
{
    public class SongHandlerTests
    {
        Mock<IMusicInfoApiClient> _musicInfoClientMock;
        Mock<ILyricsApiClient> _lyricsApiClientMock;
        ISongHandler _sut;
        private WorksResponse worksResponseTestData = new WorksResponse
        {
            Works = new List<Work>
            {
                new Work {
                    Id = Guid.NewGuid().ToString(),
                    Type = "",
                    Title = "AlbumOne"
                },
                new Work {
                    Id = Guid.NewGuid().ToString(),
                    Type = "Song",
                    Title = "SongOne"
                },
                new Work {
                    Id = Guid.NewGuid().ToString(),
                    Type = "Song",
                    Title = "SongTwo"
                }
            }
        };

        [SetUp]
        public void SetupMocks() 
        {
            _musicInfoClientMock = new Mock<IMusicInfoApiClient>();
            _lyricsApiClientMock = new Mock<ILyricsApiClient>();
            _sut = new SongHandler(_musicInfoClientMock.Object, _lyricsApiClientMock.Object);
        }

        [Test]
        public async Task GetArtistWorksAsync_ReturnsArtistSongs_IsValidArtistId()
        {
            var artistId = "1234";
            var testData = Task.Factory.StartNew(() => { return worksResponseTestData; });
            _musicInfoClientMock.Setup(musicInfoApiClient => musicInfoApiClient.GetWorksAsync(artistId, It.IsAny<int>(), It.IsAny<int>())).Returns(testData);
            
            var works = await _sut.GetArtistWorksAsync(artistId);

            works.Works.Count.Should().Be(2);
            works.Works[0].Title.Should().Be("SongOne");
            works.Works[1].Title.Should().Be("SongTwo");
        }

        [Test]
        public async Task GetArtistWorksAsync_ReturnsNoSongs_IsInvalidArtistId()
        {
            var artistId = "4324";
            var testData = Task.Factory.StartNew(() => { return worksResponseTestData; });
            _musicInfoClientMock.Setup(musicInfoApiClient => musicInfoApiClient.GetWorksAsync("1234", It.IsAny<int>(), It.IsAny<int>())).Returns(testData);

            var works = await _sut.GetArtistWorksAsync(artistId);

            works.Works.Should().BeEmpty();
        }
    }
}