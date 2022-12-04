using dotnet_movie_api.src.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace dotnet_movie_api.src.DataAccess
{
    public interface IGenericDal<T> where T : class

    {
        List<T> GetList();
        T Get (int id);
        void Add(T t);
        void Update(T t);
        void Delete(T t);
    }
}
