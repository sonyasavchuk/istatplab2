using System.ComponentModel.DataAnnotations;
namespace istatplab2.Models
{
    public class Tracks
    {
        public Tracks()
        {
            Playlists=new List<Playlists>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Shouldn`t be empty")]
        [Display(Name = "Tracks")]
        public string Name { get; set; }
        public int AlbumId { get; set; }

        public int GenreId { get; set; }
        public int miliseconds { get; set; }
        public virtual Genres Genres { get; set; }
        public virtual ICollection<Playlists> Playlists { get; set; }
        public virtual Albums Albums { get; set; }

    }
}
