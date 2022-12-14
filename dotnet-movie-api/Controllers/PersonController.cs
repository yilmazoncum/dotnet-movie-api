
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.ExternalApi;
using MovieApi.Data.Entities;
using MovieApi.DataAccess.DataAccess;
using System.Drawing;

namespace dotnet_movie_api.Controllers
{
    [Route("/[controller]")]
    public class PersonController : Controller
    {

        IPersonRepository _repository;
        ExternalApi _externalApi;

        public PersonController(IPersonRepository personRepository,ExternalApi externalApi)
        {
            _repository = personRepository;
            _externalApi = externalApi;
        }

        [HttpGet("api/{apiId}")]
        public async Task<ActionResult<Person>> GetPersonwithApiId(int apiId)
        {
            var person = _repository.GetwithApiId(apiId);

            if (person != null)
            {
                return person;
            }
            try
            {
                Console.WriteLine("Person not found in DB -> external api");
                return _externalApi.GetPerson(apiId).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("db/{guid}")]
        public async Task<ActionResult<Person>> GetPersonwithGuid(Guid guid)
        {
            var person = _repository.GetwithGuid(guid);

            if (person != null)
            {
                return person;
            }
            return NotFound() ;
        }

        [HttpPut("")]
        public async Task<IActionResult> PutPerson([Bind] Person person)
        {
            if (!PersonExists(person.Id))
            {
                return NotFound();
            }
            else
            {
                _repository.Update(person);
                return Ok();
            }
        }
        
        [HttpPost("")]
        public async Task<ActionResult<Person>> PostPerson([Bind] Person person)
        {
            
            try
            {
                person.Id = Guid.NewGuid();
                _repository.Add(person);
                return Ok(person);
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.Id))
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
        public ActionResult<IEnumerable<Person>> GetList()
        {
            //logger.LogInformation("Get list : " + typeof(T));
            return _repository.GetList();
        }

        [HttpDelete("")]
        public ActionResult Delete(Guid id)
        {
            Person person = _repository.GetwithGuid(id);
            if (person == null)
            {
                return NotFound();
            }

            _repository.Delete(person);
            return Ok();

        }
        private bool PersonExists(Guid id)
        {
            if (_repository.GetwithGuid(id) == null)
            {
                return false;
            }
            return true;
        }

    }
}
