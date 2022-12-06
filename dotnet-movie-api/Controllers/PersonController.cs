using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using MovieApi.ExternalApi;
using MovieApi.Data.Entities;

namespace dotnet_movie_api.Controllers
{
    public class PersonController : GenericController<Person>
    {      
        //// GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = _repository.Get(id);

            if (person != null)
            {
                return person;
            }
            try
            {
                Console.WriteLine("Person not found in DB -> external api");
                return ExternalApi.GetPerson(id).Result;

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {

            try
            {
                _repository.Add(person);
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

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }
        private bool PersonExists(int id)
        {
            if (_repository.Get(id) == null)
            {
                return false;
            }
            return true;
        }
    }
}
