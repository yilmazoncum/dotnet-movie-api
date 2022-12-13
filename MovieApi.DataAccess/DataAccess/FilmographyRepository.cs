using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class FilmographyRepository : GenericRepository<Filmography>, IFilmographyRepository
    {
        private readonly MovieDbContext ctx;
        public FilmographyRepository(MovieDbContext context) : base(context)
        {
            ctx = context;
        }
        List<Filmography> IFilmographyRepository.GetFilmographyList(Guid id)
        {
            return ctx.Set<Filmography>().Where(fl => fl.PersonId == id).ToList();
        }
    }
}
