using System.ComponentModel.DataAnnotations;
namespace istatplab2.Models
{
    public class Albums
    {
        public Albums()
        {
            Tracks = new List<Tracks>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Shouldn`t be empty")]
        [Display(Name = "Albums")]
        public string Title { get; set; }
        public int ArtistId { get; set; }
        public virtual Artists Artists { get; set; }
        public virtual ICollection<Tracks> Tracks { get; set; }
    }
}
