using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace MovieApi.ExternalApi
{
    public class ExternalApi
    {

        public static ExternalApi ExternalApiSingleton = new ExternalApi();
        static WebApplicationBuilder builder = WebApplication.CreateBuilder();
        static string baseUrl = "https://api.themoviedb.org/3/";
        static readonly HttpClient client = new HttpClient();
        static MovieDbContext ctx = new MovieDbContext();
        static string apiKey = builder.Configuration.GetValue<string>("ExternalApiKey").ToString();

        private ExternalApi() { }

        public static ExternalApi GetInstance()
        {
            return ExternalApiSingleton;
        }

        public static async Task<Movie> GetMovie(int id)
        {
       
            string currentUrl = baseUrl + "movie/" + id.ToString() + "?" + apiKey;
                          
            GenericRepository<Movie> genericRepository= new GenericRepository<Movie>();
            Movie movie = new Movie();

            movie = ParseMovieJson(makeRequest(currentUrl).Result);

            genericRepository.Add(movie);

            return movie;
           
        }

        public static async Task<Person> GetPerson(int id)
        {
            string currentUrl = baseUrl + "person/" + id.ToString() + "?" + apiKey;

            Console.WriteLine("External Api triggered");
           
            GenericRepository<Person> genericRepository = new GenericRepository<Person>();         
            Person person = new Person();
            person = ParsePersonJson(makeRequest(currentUrl).Result);

            genericRepository.Add(person);

            return person;
    
        }

        public static async Task<Cast> GetCast(int id)
        {
            string currentUrl = baseUrl + "movie/" + id.ToString() + "/credits" + "?" + apiKey;

            Console.WriteLine("External Api triggered");

            GenericRepository<Cast> genericRepository = new GenericRepository<Cast>();
            Cast cast = new Cast();
            cast = ParseCastJson(makeRequest(currentUrl).Result);

            return cast;
        }

        public static async Task<Filmography> GetFilmography(int id)
        {
            string currentUrl = baseUrl + "person/" + id.ToString() + "/movie_credits" + "?" + apiKey;
            
            Console.WriteLine("External Api triggered");
            
            GenericRepository<Filmography> genericRepository = new GenericRepository<Filmography>();
            Filmography filmo = new Filmography();
            filmo = ParseFilmographyJson(makeRequest(currentUrl).Result,id);

            return filmo;
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

        private static Cast ParseCastJson(string response)
        {

            JObject json = JObject.Parse(response);
            GenericRepository<Cast> genericRepository = new GenericRepository<Cast>();
            Cast cast = new Cast();

            cast.MovieId = int.Parse(json.Property("id").Value.ToString());
            for (int i = 0; i < 10; i++)
            {
                JToken tempCast = json["cast"][i];
                cast.PersonId = int.Parse((string)tempCast["id"]);
                cast.Name =(string)tempCast["name"];
                cast.KnownForDepartment = (string)tempCast["known_for_department"];
                cast.Character = (string)tempCast["character"];

                genericRepository.Add(cast);
            }
            return cast;
        }

        private static Filmography ParseFilmographyJson(string response, int id)
        {

            JObject json = JObject.Parse(response);
            Filmography filmo = new Filmography();         
            GenericRepository<Filmography> genericRepository = new GenericRepository<Filmography>();
            

            filmo.PersonId = id;
            for (int i = 0; i < 10; i++)
            {
                JToken tempCast = json["cast"][i];
                filmo.MovieId = int.Parse((string)tempCast["id"]);
                filmo.Title = (string)tempCast["title"];
                filmo.Character = (string)tempCast["character"];

                genericRepository.Add(filmo);
            }
            return filmo;

        }

        private static async Task<String> makeRequest(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }    
}