using dotnet_movie_api.src.DataAccess;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data.Entities;
using MovieApi.DataAccess.DataAccess;
using MovieApi.ExternalApi;
using NuGet.Protocol.Core.Types;

namespace dotnet_movie_api.Controllers
{
    [Route("/[controller]")]
    public class SearchController : Controller
    {

        IMovieRepository _movieRepository;
        IPersonRepository _personRepository;
        ExternalApi _externalApi;

        public SearchController(IMovieRepository movieRepository, IPersonRepository personRepository, ExternalApi externalApi)
        {
            _movieRepository = movieRepository;
            _personRepository = personRepository;
            _externalApi = externalApi;
        }


        [Route("[controller]/movie")]
        [HttpGet()]
        public async Task<ActionResult<Movie>> SearchMovie([FromQuery] string q)
        {
           
           int id = _externalApi.SearchMovie(q).Result;
           var movie = _movieRepository.GetwithApiId(id);

            if (movie != null)
            {
                return movie;
            }

            try
            {
                Console.WriteLine("Movie not found in DB -> external api");
                return _externalApi.GetMovie(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }

        }

        [Route("[controller]/person")]
        [HttpGet()]
        public async Task<ActionResult<Person>> SearchPerson([FromQuery] string q)
        {
           
           int id = _externalApi.SearchPerson(q).Result;
           var person = _personRepository.GetwithApiId(id);

            if (person != null)
            {
                return person;
            }

            try
            {
                Console.WriteLine("Person not found in DB -> external api");
                return _externalApi.GetPerson(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

    }
}
