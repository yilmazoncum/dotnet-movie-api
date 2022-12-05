using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.ExternalApi;
using System;

namespace dotnet_movie_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastsController : ControllerBase
    {
        private readonly GenericRepository<Cast> _repository;

        public CastsController(MovieDbContext context)
        {
            _repository = new GenericRepository<Cast>();
        }

        // GET: api/Casts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cast>>> GetCasts()
        {
            return _repository.GetList();
        }

        // GET: api/Casts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cast>> GetCast(int id)
        {
            var person = _repository.Get(id);

            if (person != null)
            {
                return person;
            }

            try
            {
                Console.WriteLine("Cast not found in DB -> external api");
                return ExternalApi.GetCast(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // PUT: api/Casts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCast(int id, Cast cast)
        {
            if (id != cast.MovieId)
            {
                return BadRequest();
            }

            

            try
            {
                _repository.Update(cast);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CastExists(id))
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

        // POST: api/Casts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cast>> PostCast(Cast cast)
        {
            _repository.Add(cast);

            return CreatedAtAction("GetCast", new { id = cast.MovieId }, cast);
        }

        // DELETE: api/Casts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCast(int id)
        {
            var cast = _repository.Get(id);
            
            if (cast == null)
            {
                return NotFound();
            }

            _repository.Delete(cast);

            return NoContent();
        }

        private bool CastExists(int id)
        {
            if (_repository.Get(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
