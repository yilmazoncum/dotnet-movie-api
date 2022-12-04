using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;

namespace MovieApi.DataAccess.DataAccess
{
    public class MovieRepository : GenericRepository<Movie>,IMovieDal
    {
    }
}
