using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public interface ICastRepository : IGenericRepository<Cast>
    {

        List<Cast> GetCastsList(Guid id);
    }
}
