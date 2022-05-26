using System.ComponentModel.DataAnnotations;

namespace istatplab2.Models;

public class Artists
{
    public Artists()
    {
        Albums = new List<Albums>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "Shouldn`t be empty")]
    [Display(Name = "Artists")]

    public string Name { get; set; }

    public virtual ICollection<Albums> Albums { get; set; }
}