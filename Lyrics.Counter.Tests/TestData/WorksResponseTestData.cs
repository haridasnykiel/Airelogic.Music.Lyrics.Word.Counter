using System;
using System.Collections.Generic;
using Music.Lyrics.Word.Counter.Models;

namespace Lyrics.Counter.Tests.TestData
{
    public static class WorksResponseTestData
    {
        public static WorksResponse ThreeWorksResponse ()
        {

            return new WorksResponse
            {
                Works = new List<Work>
                {
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "",
                        Title = "AlbumOne"
                    },
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "Song",
                        Title = "SongOne"
                    },
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "Song",
                        Title = "SongTwo"
                    }
                }
            };
        }

        public static WorksResponse FourSongsResponse ()
        {

            return new WorksResponse
            {
                Works = new List<Work>
                {
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "Song",
                        Title = "SongOne"
                    },
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "Song",
                        Title = "SongTwo"
                    },
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "Song",
                        Title = "SongThree"
                    },
                    new Work
                    {
                        Id = Guid.NewGuid ().ToString (),
                        Type = "Song",
                        Title = "SongFour"
                    }
                }
            };

        }
    }
}