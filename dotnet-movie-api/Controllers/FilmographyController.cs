using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;
using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using MovieApi.ExternalApi;

namespace dotnet_movie_api.Controllers
{
    public class FilmographyController : GenericController<Filmography>
    {
        // GET: api/Filmography/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filmography>> GetFilmography(int id)
        {
            var filmography = _repository.Get(id);

            if (filmography != null)
            {
                return filmography;
            }

            try
            {
                Console.WriteLine("Filmography not found in DB -> external api");
                return ExternalApi.GetFilmography(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // PUT: api/Filmography/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmography(int id, Filmography filmography)
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

        private bool FilmographyExists(int id)
        {
            if (_repository.Get(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
