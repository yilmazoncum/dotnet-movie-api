using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class CastRepository : GenericRepository<Cast>, ICastRepository
    {

        private readonly MovieDbContext ctx;
        public CastRepository(MovieDbContext context) : base(context) {
            ctx = context;
        }

        List<Cast> ICastRepository.GetCastsList(Guid id)
        {
            return ctx.Set<Cast>().Where(c => c.MovieId == id).ToList();
        }
    }
}
