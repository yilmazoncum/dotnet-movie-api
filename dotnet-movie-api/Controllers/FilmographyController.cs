using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;
using dotnet_movie_api.src.DataAccess;
using MovieApi.ExternalApi;
using MovieApi.DataAccess.DataAccess;

namespace dotnet_movie_api.Controllers
{
    [Route("/[controller]")]
    public class FilmographyController : Controller
    {

        IFilmographyRepository _repository;
        ExternalApi _externalApi;

        public FilmographyController(IFilmographyRepository repository, ExternalApi externalApi)
        {
            _repository = repository;
            _externalApi = externalApi;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<Filmography>>> GetFilmography(Guid id)
        {
            try
            {
                return _repository.GetFilmographyList(id);

            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/Filmography/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmography(Guid id, Filmography filmography)
        {
            if (id != filmography.MovieId)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(filmography);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmographyExists(id))
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

        // POST: api/Filmography
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filmography>> PostFilmography(Filmography filmography)
        {
            try
            {
                _repository.Add(filmography);
            }
            catch (DbUpdateException)
            {
                if (FilmographyExists(filmography.MovieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFilmography", new { id = filmography.MovieId }, filmography);
        }

        private bool FilmographyExists(Guid id)
        {
            if (_repository.GetwithGuid(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
