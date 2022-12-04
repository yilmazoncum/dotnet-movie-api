using dotnet_movie_api.src.Models;

namespace dotnet_movie_api.src.DataAccess
{
    public class GenericRepository<T> : IGenericDal<T> where T : class

    {
        public  void Add(T t)
        {
            using var ctx = new MovieDbContext();
            ctx.Add(t);
            ctx.SaveChanges();
        }

        public  void Delete(T t)
        {
            using var ctx = new MovieDbContext();
            ctx.Remove(t);
            ctx.SaveChanges();
        }

        public  T Get(int id)
        {
            using var ctx = new MovieDbContext();
            return ctx.Set<T>().Find(id);
            
        }

        public  List<T> GetList()
        {
            using var ctx = new MovieDbContext();
            return ctx.Set<T>().ToList();
        }

        public  void Update(T t)
        {
            using var ctx = new MovieDbContext();
            ctx.Update(t);
            ctx.SaveChanges();
        }
    }
}
