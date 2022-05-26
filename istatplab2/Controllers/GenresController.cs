using istatplab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace istatplab2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly MusicAPIContext _context;

    public GenresController(MusicAPIContext context)
    {
        _context = context;
    }

    // GET: api/Genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genres>>> GetGenres()
    {
        if (_context.Genres == null) return NotFound();
        return await _context.Genres.ToListAsync();
    }

    // GET: api/Genres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Genres>> GetGenres(int id)
    {
        if (_context.Genres == null) return NotFound();
        var genres = await _context.Genres.FindAsync(id);

        if (genres == null) return NotFound();

        return genres;
    }

    // PUT: api/Genres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenres(int id, Genres genres)
    {
        if (id != genres.Id) return BadRequest();

        _context.Entry(genres).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenresExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Genres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Genres>> PostGenres(Genres genres)
    {
        if (_context.Genres == null) return Problem("Entity set 'MusicAPIContext.Genres'  is null.");
        _context.Genres.Add(genres);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGenres", new { id = genres.Id }, genres);
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenres(int id)
    {
        if (_context.Genres == null) return NotFound();
        var genres = await _context.Genres.FindAsync(id);
        if (genres == null) return NotFound();

        _context.Genres.Remove(genres);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenresExists(int id)
    {
        return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}