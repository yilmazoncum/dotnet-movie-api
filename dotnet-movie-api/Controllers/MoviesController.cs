using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MovieApi.ExternalApi;

namespace dotnet_movie_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly GenericRepository<Movie> _repository;

        public MoviesController()
        {
            _repository = new GenericRepository<Movie>();
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            //return await _context.Movies.ToListAsync();
            return _repository.GetList();

        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = _repository.Get(id);

            if (movie != null)
            {
                return movie;
            }

            try
            {
                Console.WriteLine("Movie not found in DB -> external api");
                return ExternalApi.GetMovie(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }


            try
            {
                _repository.Update(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {

            try
            {
                _repository.Add(movie);
            }
            catch (DbUpdateException)
            {
                if (MovieExists(movie.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = _repository.Get(id);
            

            if (movie == null)
            {
                return NotFound();
            }
            _repository.Delete(movie);

            return NoContent();
        }

        private bool MovieExists(int id)
        {

            if (_repository.Get(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
