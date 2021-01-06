using System.ComponentModel.DataAnnotations;

namespace Music.Lyrics.Word.Counter.Models
{
    public class ArtistRequest
    {
        [Required]
        public string Name { get; set; }
    }
}