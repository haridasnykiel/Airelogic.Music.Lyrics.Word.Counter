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
            var testData = Task.Factory.StartNew(() => { return WorksResponseTestData.FourWorksResponseAllSongs(); });
            //_lyricsApiClientMock.Setup(lyricsApiClient => lyricsApiClient.GetLyricsAsync(artistName, It.IsAny<string>())).Returns(testData);

            var results = await _sut.GetAllSongLyricsAsync(artistName, WorksResponseTestData.FourWorksResponseAllSongs().Works);

           // results.Works.Should().BeEmpty();
        }
    }
}