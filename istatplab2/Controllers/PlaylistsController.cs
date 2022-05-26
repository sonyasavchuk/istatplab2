using istatplab2.Models;
using istatplab2.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace istatplab2.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PlaylistsController : ControllerBase
{
    private readonly MusicAPIContext _context;

    public PlaylistsController(MusicAPIContext context)
    {
        _context = context;
    }

    // GET: api/Playlists
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaylistsDtoRead>>> GetPlaylists()
    {
        var playlists = await _context.Playlists.Include(d => d.Tracks).ToListAsync();
        var playlistsDtoRead = new List<PlaylistsDtoRead>();
        foreach (var playlist in playlists)
        {
            var tracksIds = await _context.PlaylistsTracks
                .Where(c => c.PlaylistId == playlist.Id).Select(c => c.TrackId)
                .ToListAsync();
            var tracks = await _context.Tracks.Where(c => tracksIds.Contains(c.Id))
                .Select(d => d.Name).ToListAsync();
            playlistsDtoRead.Add(
                new PlaylistsDtoRead
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    TracksIds = tracksIds,
                    Tracks = tracks
                });
        }

        return playlistsDtoRead;
    }

    // GET: api/Playlists/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PlaylistsDtoRead>> GetPlaylists(int id)
    {
        var playlist = await _context.Playlists.Where(c => c.Id == id).Include(d => d.Tracks)
            .FirstOrDefaultAsync();

        if (playlist == null) return NotFound();

        var tracksIds = playlist.Tracks.Select(d => d.Id).ToList();
        var tracks = playlist.Tracks.Select(d => d.Name).ToList();
        var playlistDtoRead = new PlaylistsDtoRead
        {
            Id = playlist.Id,
            Name = playlist.Name,
            TracksIds = tracksIds,
            Tracks = tracks
        };
        return playlistDtoRead;
    }

    // PUT: api/Playlists/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlaylists(int? id, PlaylistsDtoWrite playlistsDtoWrite)
    {
        if (id is null) return NotFound("Id is null.");
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist is null) return NotFound("Playlist is not found.");
        var tracksIds = playlistsDtoWrite.TracksIDs;

        if (tracksIds.Count() != 0)
        {
            var tracksToDelete = await _context.PlaylistsTracks
                .Where(c => c.PlaylistId == id).ToListAsync();
            foreach (var trackToDelete in tracksToDelete) _context.PlaylistsTracks.Remove(trackToDelete);
            await _context.SaveChangesAsync();
            var tracks = await _context.Tracks
                .Where(c => tracksIds.Contains(c.Id)).ToListAsync();
            playlist.Tracks = tracks;
        }

        playlist.Name = playlistsDtoWrite.Name;

        _context.Entry(playlist).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlaylistsExists(id.Value))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Playlists
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Playlists>> PostPlaylists(PlaylistsDtoWrite playlistsDtoWrite)
    {
        if (playlistsDtoWrite.TracksIDs != null && !playlistsDtoWrite.TracksIDs.Any()) return NotFound("No track id.");
        var tracks = await _context.Tracks
            .Where(c => playlistsDtoWrite.TracksIDs.Contains(c.Id)).ToListAsync();
        if (!tracks.Any()) return NotFound("Bad track id(s)");
        var playlist = new Playlists
        {
            Name = playlistsDtoWrite.Name,
            Tracks = tracks
        };
        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();

        return RedirectToAction("GetPlaylists", new { id = playlist.Id });
    }

    // DELETE: api/Playlists/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylists(int id)
    {
        if (_context.Playlists == null) return NotFound();
        var playlists = await _context.Playlists.FindAsync(id);
        if (playlists == null) return NotFound();

        _context.Playlists.Remove(playlists);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PlaylistsExists(int id)
    {
        return (_context.Playlists?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}