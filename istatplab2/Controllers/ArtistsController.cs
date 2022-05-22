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
    public class ArtistsController : ControllerBase
    {
        private readonly MusicAPIContext _context;

        public ArtistsController(MusicAPIContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artists>>> GetArtists()
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            return await _context.Artists.ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artists>> GetArtists(int id)
        {
          if (_context.Artists == null)
          {
              return NotFound();
          }
            var artists = await _context.Artists.FindAsync(id);

            if (artists == null)
            {
                return NotFound();
            }

            return artists;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtists(int id, Artists artists)
        {
            if (id != artists.Id)
            {
                return BadRequest();
            }

            _context.Entry(artists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistsExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artists>> PostArtists(Artists artists)
        {
          if (_context.Artists == null)
          {
              return Problem("Entity set 'MusicAPIContext.Artists'  is null.");
          }
            _context.Artists.Add(artists);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtists", new { id = artists.Id }, artists);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtists(int id)
        {
            if (_context.Artists == null)
            {
                return NotFound();
            }
            var artists = await _context.Artists.FindAsync(id);
            if (artists == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artists);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistsExists(int id)
        {
            return (_context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
