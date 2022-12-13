﻿using dotnet_movie_api.src.DataAccess;
using MovieApi.Data.Entities;

namespace MovieApi.DataAccess.DataAccess
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository 
    {
        public MovieRepository(MovieDbContext context) : base(context) { }

        public Movie GetwithApiId(int id)
        {
            using var ctx = new MovieDbContext();

            try
            {
                return ctx.Set<Movie>().Where(m => m.ApiId == id).First();
            }
            catch (Exception ex)
            {
                return null;
            }


        }
    }
}
