using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;

namespace dotnet_movie_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesPersonsController : ControllerBase
    {
        private readonly MovieDbContext _context;

        public MoviesPersonsController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: api/MoviesPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesPerson>>> GetMoviesPeople()
        {
            return await _context.MoviesPeople.ToListAsync();
        }

        // GET: api/MoviesPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoviesPerson>> GetMoviesPerson(int id)
        {
            var moviesPerson = await _context.MoviesPeople.FindAsync(id);

            if (moviesPerson == null)
            {
                return NotFound();
            }

            return moviesPerson;
        }

        // PUT: api/MoviesPersons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoviesPerson(int id, MoviesPerson moviesPerson)
        {
            if (id != moviesPerson.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(moviesPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesPersonExists(id))
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

        // POST: api/MoviesPersons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoviesPerson>> PostMoviesPerson(MoviesPerson moviesPerson)
        {
            _context.MoviesPeople.Add(moviesPerson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MoviesPersonExists(moviesPerson.MovieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMoviesPerson", new { id = moviesPerson.MovieId }, moviesPerson);
        }

        // DELETE: api/MoviesPersons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoviesPerson(int id)
        {
            var moviesPerson = await _context.MoviesPeople.FindAsync(id);
            if (moviesPerson == null)
            {
                return NotFound();
            }

            _context.MoviesPeople.Remove(moviesPerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesPersonExists(int id)
        {
            return _context.MoviesPeople.Any(e => e.MovieId == id);
        }
    }
}
