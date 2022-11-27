using dotnet_movie_api.src.Models;

namespace dotnet_movie_api.src.DataAccess
{
    public class GenericRepository<T> : IGenericDal<T>
    {
        public void Add(T t)
        {
            using var ctx = new MovieDbContext();
            ctx.Remo(t);
            ctx.SaveChanges();
        }

        public void Delete(T t)
        {
            using var ctx = new MovieDbContext();
            ctx.Remove(t);
            ctx.SaveChanges();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList(T t)
        {
            throw new NotImplementedException();
        }

        public void Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
