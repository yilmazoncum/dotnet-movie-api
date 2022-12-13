namespace dotnet_movie_api.src.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MovieDbContext ctx;
        public GenericRepository(MovieDbContext context) {

            this.ctx = context;
        }
        public  void Add(T t)
        {
            try
            { 
                ctx.Add(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            ctx.SaveChanges();
        }

        public  void Delete(T t)
        {
            ctx.Remove(t);
            ctx.SaveChanges();
        }

        public  T GetwithGuid(Guid id)
        {      
            return ctx.Set<T>().Find(id);           
        }

        public  List<T> GetList()
        {
            return ctx.Set<T>().ToList();
        }

        public  void Update(T t)
        {
            ctx.Update(t);
            ctx.SaveChanges();
        }

    }
}
