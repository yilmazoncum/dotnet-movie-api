using dotnet_movie_api.src.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace dotnet_movie_api.src.DataAccess
{
    public interface IMovieDal : IGenericDal<Movie>

    {
      
    }
}
