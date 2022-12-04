using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace MovieApi.ExternalApi
{
    static public class ExternalApi
    {
        static WebApplicationBuilder builder = WebApplication.CreateBuilder();
        static string baseUrl = "https://api.themoviedb.org/3/";
        static readonly HttpClient client = new HttpClient();
        static MovieDbContext ctx = new MovieDbContext();

        public static async Task<Movie> GetMovie(int id)
        {

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api_key"] = builder.Configuration.GetValue<string>("ExternalApiKey");
            string queryString = query.ToString();

            string currentUrl = baseUrl + "movie/" + id.ToString() + "?" + queryString;
                 
            Console.WriteLine("External Api triggered");
            using HttpResponseMessage response = await client.GetAsync(currentUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            GenericRepository<Movie> genericRepository= new GenericRepository<Movie>();

            Movie movie = new Movie();
            movie = ParseMovieJson(responseBody);

            genericRepository.Add(movie);

            return movie;
           
        }

        public static async Task<Person> GetPerson(int id)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api_key"] = builder.Configuration.GetValue<string>("ExternalApiKey");
            string queryString = query.ToString();

            string currentUrl = baseUrl + "person/" + id.ToString() + "?" + queryString;

            Console.WriteLine("External Api triggered");
            using HttpResponseMessage response = await client.GetAsync(currentUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            GenericRepository<Person> genericRepository = new GenericRepository<Person>();
            
            Person person = new Person();

            person = ParsePersonJson(responseBody);

            genericRepository.Add(person);

            return person;
    
        }

        private static Movie ParseMovieJson(string response)
        {
            JObject json = JObject.Parse(response);
            Movie mv = new Movie();

            mv.Id = int.Parse(json.Property("id").Value.ToString());
            mv.Budget = json.Property("budget").Value.ToString();
            mv.ImdbId = json.Property("imdb_id").Value.ToString();
            mv.OriginalTitle = json.Property("original_title").Value.ToString();
            mv.Overview = json.Property("overview").Value.ToString();
            mv.PosterPath = json.Property("poster_path").Value.ToString();
            mv.ReleaseDate = DateTime.Parse(json.Property("release_date").Value.ToString());
            mv.Revenue = int.Parse(json.Property("revenue").Value.ToString());
            mv.Runtime = int.Parse(json.Property("runtime").Value.ToString());
            mv.Title = json.Property("title").Value.ToString();
            mv.VoteAverage = decimal.Parse(json.Property("vote_average").Value.ToString());
            mv.VoteCount = int.Parse(json.Property("vote_count").Value.ToString());

            return mv;
        }

        private static Person ParsePersonJson(string response)
        {

            JObject json = JObject.Parse(response);
            Person person = new Person();

            person.Id = int.Parse(json.Property("id").Value.ToString());

            person.Birthday = DateTime.Parse(json.Property("birthday").Value.ToString());

            String temp = json.Property("deathday").Value.ToString();
            if (string.IsNullOrEmpty(temp))
            {
                person.Deathday = null;
            }
            else
            {
                person.Deathday = DateTime.Parse(temp);
            }

            person.ImdbId = json.Property("imdb_id").Value.ToString();
            person.KnownForDepartment = json.Property("known_for_department").Value.ToString();
            person.Name= json.Property("name").Value.ToString();
            person.PlaceOfBirth = json.Property("place_of_birth").Value.ToString();

            Console.WriteLine(person.ToString());
            return person;
   
        }

            
        }    
}