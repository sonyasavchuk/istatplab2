using System.ComponentModel.DataAnnotations;

namespace istatplab2.ModelsDTO;

public class PlaylistsDtoRead
{
    public PlaylistsDtoRead()
    {
        TracksIds = new List<int>();
        Tracks = new List<string>();
    }

    public int Id { get; set; }
    [Required] public string Name { get; set; }
    public ICollection<string>? Tracks { get; set; }
    public ICollection<int> TracksIds { get; set; }
}