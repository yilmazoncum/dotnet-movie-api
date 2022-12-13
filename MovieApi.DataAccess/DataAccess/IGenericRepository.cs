namespace dotnet_movie_api.src.DataAccess
{
    public interface IGenericRepository<T> where T : class

    {
        List<T> GetList();
        void Add(T t);
        void Update(T t);
        void Delete(T t);
        T GetwithGuid(Guid id);

        
    }
}