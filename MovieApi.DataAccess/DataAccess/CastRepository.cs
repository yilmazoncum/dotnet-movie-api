using dotnet_movie_api.src.DataAccess;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;
using System.Linq;

namespace MovieApi.DataAccess.DataAccess
{
    public class CastRepository : GenericRepository<Cast>, ICastRepository
    {

        private readonly MovieDbContext ctx;
        public CastRepository(MovieDbContext context) : base(context) {
            ctx = context;
        }

        List<Cast> ICastRepository.GetCastsList(Guid id)
        {
            //List<Cast> list;
            //ctx.Set<Cast>().Where(c => c.MovieId == id).Append(list);
            //list = ctx.Casts.Where(c => c.MovieId.Equals(id)).ToList();
            //ctx.Casts.SelectMany().Where(c => c.MovieId.Equals(id)).Result.ToList();
            return ctx.Set<Cast>().Where(c => c.MovieId == id).ToList();
            //return ctx.Casts.ToList();
            //string sql = string.Format("Select * from Casts where movieID={0}", id.ToString());
            //FormattableString.Create(sql)
            //list = ctx.Casts.FromSql((FormattableString)sql).ToList();
            //Select * from Casts where movieID='1137C860-288B-4BD5-8D51-80FE703422FE';
            // return list;

            //return ctx.Casts
            //.FromSql($"SELECT * FROM Casts where movieID={id.ToString()}")
            //.ToList();
        }
    }
}
