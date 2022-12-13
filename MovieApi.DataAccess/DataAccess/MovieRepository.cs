using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository 
    {
        private readonly MovieDbContext ctx;
        public MovieRepository(MovieDbContext context) : base(context) {
            ctx = context;
        }

        public Movie GetwithApiId(int id)
        {
            using var ctx = new MovieDbContext();

            try
            {
                return ctx.Set<Movie>().Where(m => m.ApiId == id).First();
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
