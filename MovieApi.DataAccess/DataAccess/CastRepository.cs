using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class CastRepository : GenericRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieDbContext context) : base(context) {}
    }
}
