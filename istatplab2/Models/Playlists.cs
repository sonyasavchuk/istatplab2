using System.ComponentModel.DataAnnotations;
namespace istatplab2.Models
{
    public class Playlists
    {
        public Playlists()
        {
            Tracks=new List<Tracks>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tracks> Tracks { get; set; }
    }
}
