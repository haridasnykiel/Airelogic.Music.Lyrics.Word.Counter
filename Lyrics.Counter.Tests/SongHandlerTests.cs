using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Lyrics.Counter.Tests.TestData;
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


        [SetUp]
        public void TestSetup()
        {
            _musicInfoClientMock = new Mock<IMusicInfoApiClient>();
            _lyricsApiClientMock = new Mock<ILyricsApiClient>();
            _sut = new SongHandler(_musicInfoClientMock.Object, _lyricsApiClientMock.Object);
        }

        [Test]
        public async Task GetArtistWorksAsync_ReturnsArtistSongs_IsValidArtistId()
        {
            var artistId = "1234";
            var testData = Task.Factory.StartNew(() => { return WorksResponseTestData.ThreeWorksResponse(); });
            _musicInfoClientMock.Setup(musicInfoApiClient => musicInfoApiClient.GetWorksAsync(artistId, It.IsAny<int>(), It.IsAny<int>())).Returns(testData);

            var results = await _sut.GetArtistWorksAsync(artistId);

            results.Works.Count.Should().Be(2);
            results.Works[0].Title.Should().Be("SongOne");
            results.Works[1].Title.Should().Be("SongTwo");
        }

        [Test]
        public async Task GetArtistWorksAsync_ReturnsNoSongs_IsInvalidArtistId()
        {
            var artistId = "4324";
            var emptyResponse = Task.Factory.StartNew(() => { return new WorksResponse(); });
            _musicInfoClientMock.Setup(musicInfoApiClient => musicInfoApiClient.GetWorksAsync(artistId, It.IsAny<int>(), It.IsAny<int>())).Returns(emptyResponse);

            var results = await _sut.GetArtistWorksAsync(artistId);

            results.Works.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllSongLyricsAsync_ReturnsLyrics_ValidArtistNameAndSongs()
        {
            var artistName = "artistName";
            var worksResponse = WorksResponseTestData.FourSongsResponse();
            var testData = Task.Factory.StartNew(() => { return LyricsResponseTestData.ResponseWithLyrics(); });
            _lyricsApiClientMock.Setup(lyricsApiClient => lyricsApiClient.GetLyricsAsync(artistName, It.IsAny<string>())).Returns(testData);

            var results = await _sut.GetAllSongLyricsAsync(artistName, worksResponse.Works);

            var resultList = results.ToList();
            resultList.Should().HaveCount(4);
            resultList[0].Title.Should().Be("SongOne");
            resultList[1].Title.Should().Be("SongTwo");
            resultList[2].Title.Should().Be("SongThree");
            resultList[3].Title.Should().Be("SongFour");
            resultList.ForEach(s => s.Lyrics.Should().Be("Some Lyrics"));
        }

        [Test]
        public async Task GetAllSongLyricsAsync_ReturnsNoSongs_NullLyricsResponse()
        {
            var artistName = "artistName";
            var worksResponse = new WorksResponse
            {
                Works = new List<Work> { new Work { Id = "1234DSF", Title = "Song", Type = "Song" } }
            };
            _lyricsApiClientMock.Setup(lyricsApiClient => lyricsApiClient.GetLyricsAsync(artistName, It.IsAny<string>())).Returns(Task.FromResult<LyricsResponse>(null));

            var results = await _sut.GetAllSongLyricsAsync(artistName, worksResponse.Works);

            results.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllSongLyricsAsync_ReturnsNoSongs_NoLyricsResponse()
        {
            var artistName = "artistName";
            var worksResponse = new WorksResponse
            {
                Works = new List<Work> { new Work { Id = "1234DSF", Title = "Song", Type = "Song" } }
            };
            var lyricsResponse = new LyricsResponse();
            _lyricsApiClientMock.Setup(lyricsApiClient => lyricsApiClient.GetLyricsAsync(artistName, It.IsAny<string>()))
                .Returns(Task.FromResult<LyricsResponse>(lyricsResponse));

            var results = await _sut.GetAllSongLyricsAsync(artistName, worksResponse.Works);

            results.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllSongLyricsAsync_ReturnsOneSong_OneSuccessfulLyricsResponse()
        {
            var artistName = "artistName";
            var worksResponse = new WorksResponse
            {
                Works = new List<Work> 
                {
                    new Work { Id = "1234DSFFWD", Title = "Song", Type = "Song" },
                    new Work { Id = "127364GEEC", Title = "AnotherSong", Type = "AnotherSong" }
                }
            };
            var lyricsResponse = LyricsResponseTestData.ResponseWithLyrics();
            _lyricsApiClientMock.Setup(lyricsApiClient => lyricsApiClient.GetLyricsAsync(artistName, worksResponse.Works[0].Title))
                .Returns(Task.FromResult<LyricsResponse>(lyricsResponse));
            _lyricsApiClientMock.Setup(lyricsApiClient => lyricsApiClient.GetLyricsAsync(artistName, worksResponse.Works[1].Title))
                .Returns(Task.FromResult<LyricsResponse>(new LyricsResponse { Lyrics = "" }));

            var results = await _sut.GetAllSongLyricsAsync(artistName, worksResponse.Works);

            var resultList = results.ToList();
            resultList.Should().HaveCount(1);
            resultList[0].Title.Should().Be("Song");
            resultList[0].Lyrics.Should().Be("Some Lyrics");
        }
    }
}