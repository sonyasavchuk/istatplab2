namespace istatplab2.Models;

public class PlaylistsTracks
{
    public int PlaylistId { get; set; }
    public int TrackId { get; set; }
    public virtual Playlists Playlists { get; set; }
    public virtual Tracks Tracks { get; set; }
}