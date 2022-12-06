using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_movie_api.Controllers
{

    [Route("api/[controller]")]
    public class GenericController<T> : Controller where T : class
    {
        
        protected readonly GenericRepository<T> _repository;

        public GenericController()
        {
            _repository = new GenericRepository<T>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<T>> GetList()
        {
            return _repository.GetList();
        }

        [HttpDelete("{id}")]
        public ActionResult<T> Delete(int id, T t)
        {
            if (_repository.Get(id) == null)
            {
                return NotFound();
            }

            _repository.Delete(t);

            return NoContent();
        }
    }
}
