using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public interface IFilmographyRepository : IGenericRepository<Filmography>
    {
    }
}
