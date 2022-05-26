using System.ComponentModel.DataAnnotations;

namespace istatplab2.Models;

public class Genres
{
    public Genres()
    {
        Tracks = new List<Tracks>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Shouldn`t be empty")]
    [Display(Name = "Genres")]
    public string Name { get; set; }

    public virtual ICollection<Tracks> Tracks { get; set; }
}