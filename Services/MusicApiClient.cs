using System;
using MetaBrainz.MusicBrainz;

namespace Music.Lyrics.Word.Counter.Services
{
    public class MusicApiClient
    {
        Query query;

        public MusicApiClient()
        {
            query = new Query("hari", "19.99", "mailto:test@test.com");
        }
        
        public void SearchArtists(string artistName) {
            var artist = query.FindArtists(artistName);


            var lookup = query.LookupArtist(new Guid("cc197bad-dc9c-440d-a5b5-d52ba2e14234"), Include.Releases);

            
        }
    }
}