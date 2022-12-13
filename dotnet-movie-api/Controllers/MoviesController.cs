using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_movie_api.src.DataAccess;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MovieApi.ExternalApi;
using MovieApi.Data.Entities;
using MovieApi.DataAccess.DataAccess;

namespace dotnet_movie_api.Controllers
{
    [Route("/[controller]")]
    public class MoviesController : Controller
    {
        IMovieRepository _repository;
        ExternalApi _externalApi;
        
        public MoviesController(IMovieRepository movieRepository, ExternalApi externalApi)
        {
            _repository = movieRepository;
            _externalApi = externalApi;
        }
        
        [HttpGet("db/{guid}")]
        public async Task<ActionResult<Movie>> GetMoviewithGuid(Guid guid)
        {
            var movie = _repository.GetwithGuid(guid);

            if (movie != null)
            {
                return movie;
            }
            return NotFound();
        }

        
        [HttpGet("api/{apiId}")]
        public async Task<ActionResult<Movie>> GetMoviewithApiId(int apiId)
        {
            var movie = _repository.GetwithApiId(apiId);

            if (movie != null)
            {
                return movie;
            }

            try
            {
                Console.WriteLine("Movie not found in DB -> external api");
                return _externalApi.GetMovie(apiId).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }      
        
        [HttpPut("")]
        public async Task<IActionResult> PutMovie([Bind] Movie movie)
        {
            if (!MovieExists(movie.Id))
            {
                return NotFound();
            }
            else
            {
                _repository.Update(movie);
                return Ok();
            }
        }    
      
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie([Bind] Movie movie)
        {

            try
            {
                movie.Id = Guid.NewGuid();
                _repository.Add(movie);
                return Ok(movie);
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

        }

        [HttpGet("list")]
        public ActionResult<IEnumerable<Movie>> GetList()
        {
            //logger.LogInformation("Get list : " + typeof(T));
            return _repository.GetList();
        }

        [HttpDelete("")]
        public ActionResult Delete(Guid id)
        {
            Movie movie = _repository.GetwithGuid(id);
            if (movie == null)
            {
                return NotFound();
            }

            _repository.Delete(movie);

            return Ok();
        }

        private bool MovieExists(Guid id)
        {

            if (_repository.GetwithGuid(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
