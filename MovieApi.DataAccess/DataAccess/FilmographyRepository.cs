using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class FilmographyRepository : GenericRepository<Filmography>, IFilmographyRepository
    {
        public FilmographyRepository(MovieDbContext context) : base(context) { }
    }
}
