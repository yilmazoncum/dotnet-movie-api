using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MovieDbContext context) : base(context) { }

        public Person GetwithApiId(int id)
        {
            using var ctx = new MovieDbContext();
            try
            {
                return ctx.Set<Person>().Where(p => p.ApiId == id).First();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
