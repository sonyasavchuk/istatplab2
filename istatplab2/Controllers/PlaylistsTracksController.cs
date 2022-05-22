using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using istatplab2.Models;

namespace istatplab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsTracksController : ControllerBase
    {
        private readonly MusicAPIContext _context;

        public PlaylistsTracksController(MusicAPIContext context)
        {
            _context = context;
        }

        // GET: api/PlaylistsTracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistsTracks>>> GetPlaylistsTracks()
        {
         
            return await _context.PlaylistsTracks.ToListAsync();
        }

        // GET: api/PlaylistsTracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistsTracks>> GetPlaylistsTracks(int id)
        {
          if (_context.PlaylistsTracks == null)
          {
              return NotFound();
          }
            var playlistsTracks = await _context.PlaylistsTracks.FindAsync(id);

            if (playlistsTracks == null)
            {
                return NotFound();
            }

            return playlistsTracks;
        }

        // PUT: api/PlaylistsTracks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylistsTracks(int id, PlaylistsTracks playlistsTracks)
        {
            if (id != playlistsTracks.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlistsTracks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistsTracksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlaylistsTracks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlaylistsTracks>> PostPlaylistsTracks(PlaylistsTracks playlistsTracks)
        {
          if (_context.PlaylistsTracks == null)
          {
              return Problem("Entity set 'MusicAPIContext.PlaylistsTracks'  is null.");
          }
            _context.PlaylistsTracks.Add(playlistsTracks);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistsTracksExists(playlistsTracks.PlaylistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylistsTracks", new { id = playlistsTracks.PlaylistId }, playlistsTracks);
        }

        // DELETE: api/PlaylistsTracks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylistsTracks(int id)
        {
            if (_context.PlaylistsTracks == null)
            {
                return NotFound();
            }
            var playlistsTracks = await _context.PlaylistsTracks.FindAsync(id);
            if (playlistsTracks == null)
            {
                return NotFound();
            }

            _context.PlaylistsTracks.Remove(playlistsTracks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaylistsTracksExists(int id)
        {
            return (_context.PlaylistsTracks?.Any(e => e.PlaylistId == id)).GetValueOrDefault();
        }
    }
}
