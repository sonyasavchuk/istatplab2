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
    public class PlaylistsController : ControllerBase
    {
        private readonly MusicAPIContext _context;

        public PlaylistsController(MusicAPIContext context)
        {
            _context = context;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlists>>> GetPlaylists()
        {
          if (_context.Playlists == null)
          {
              return NotFound();
          }
            return await _context.Playlists.ToListAsync();
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlists>> GetPlaylists(int id)
        {
          if (_context.Playlists == null)
          {
              return NotFound();
          }
            var playlists = await _context.Playlists.FindAsync(id);

            if (playlists == null)
            {
                return NotFound();
            }

            return playlists;
        }

        // PUT: api/Playlists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylists(int id, Playlists playlists)
        {
            if (id != playlists.Id)
            {
                return BadRequest();
            }

            _context.Entry(playlists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistsExists(id))
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

        // POST: api/Playlists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Playlists>> PostPlaylists(Playlists playlists)
        {
          if (_context.Playlists == null)
          {
              return Problem("Entity set 'MusicAPIContext.Playlists'  is null.");
          }
            _context.Playlists.Add(playlists);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaylists", new { id = playlists.Id }, playlists);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylists(int id)
        {
            if (_context.Playlists == null)
            {
                return NotFound();
            }
            var playlists = await _context.Playlists.FindAsync(id);
            if (playlists == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlists);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaylistsExists(int id)
        {
            return (_context.Playlists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
