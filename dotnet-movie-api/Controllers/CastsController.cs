using dotnet_movie_api.src.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;
using MovieApi.DataAccess.DataAccess;
using MovieApi.ExternalApi;
using System;

namespace dotnet_movie_api.Controllers
{
    [Route("/[controller]")]
    public class CastsController : Controller
    {
        IPersonRepository _personRepository;
        ICastRepository _castRepository;
        ExternalApi _externalApi;

        public CastsController(ICastRepository castRepository, IPersonRepository personRepository, ExternalApi externalApi) {

            _castRepository = castRepository;
            _personRepository = personRepository;

            _externalApi = externalApi;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cast>> GetCast(Guid id)
        {
            var cast = _castRepository.GetwithGuid(id);

            if (cast != null)
            {
                return cast;
            }

            try
            {
                Console.WriteLine("Cast not found in DB -> external api");
                return _externalApi.GetCast(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCast(Guid id, Cast cast)
        {
            if (id != cast.MovieId)
            {
                return BadRequest();
            }

            try
            {
                _castRepository.Update(cast);
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

        [HttpPost]
        public async Task<ActionResult<Cast>> PostCast(Cast cast)
        {
            _castRepository.Add(cast);

            return CreatedAtAction("GetCast", new { id = cast.MovieId }, cast);
        }

        private bool CastExists(Guid id)
        {
            if (_castRepository.GetwithGuid(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
