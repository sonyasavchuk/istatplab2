using System.ComponentModel.DataAnnotations;

namespace istatplab2.ModelsDTO;

public class PlaylistsDtoWrite
{
    public PlaylistsDtoWrite()
    {
        TracksIDs = new List<int>();
    }

    public int Id { get; set; }
    [Required] public string Name { get; set; }
    public ICollection<int>? TracksIDs { get; set; }
}