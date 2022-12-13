using dotnet_movie_api.src.DataAccess;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Data.Entities;
using MovieApi.DataAccess.DataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace MovieApi.ExternalApi
{
    public class ExternalApi
    {
        //static WebApplicationBuilder builder = WebApplication.CreateBuilder();
        static string apiKey;// = builder.Configuration.GetValue<string>("ExternalApiKey").ToString();

        static string baseUrl = "https://api.themoviedb.org/3/";
        static readonly HttpClient client = new HttpClient();
        

        IMovieRepository _movieRepository;
        IPersonRepository _personRepository;
        ICastRepository _castRepository;
        IFilmographyRepository _filmographyRepository;

        public ExternalApi(IMovieRepository movieRepository, IPersonRepository personRepository, ICastRepository castRepository, IFilmographyRepository filmographyRepository, IConfiguration conf)
        {
            _movieRepository = movieRepository;
            _personRepository = personRepository;
            _castRepository = castRepository;
            _filmographyRepository = filmographyRepository;
            apiKey = conf.GetValue<string>("ExternalApiKey").ToString();
        }

        public async Task<Movie> GetMovie(int id)
        {
            Console.WriteLine("GetMovie");
            string currentUrl = baseUrl + "movie/" + id.ToString() + "?api_key=" + apiKey;
                          
            Movie movie = new Movie();

            movie = ParseMovieJson(makeRequest(currentUrl).Result);

            movie.Id = Guid.NewGuid();
            _movieRepository.Add(movie);

            GetCast(id,movie.Id);

            return movie;
           
        }

        public async Task<Person> GetPerson(int id)
        {
            string currentUrl = baseUrl + "person/" + id.ToString() + "?api_key=" + apiKey;

            Console.WriteLine("External Api triggered");
        
            Person person = new Person();
            person = ParsePersonJson(makeRequest(currentUrl).Result);

            person.Id = Guid.NewGuid();
            _personRepository.Add(person);

            GetFilmography(id,person.Id);

            return person;
    
        }

        private async void GetCast(int id, Guid movie_id)
        {
            string currentUrl = baseUrl + "movie/" + id.ToString() + "/credits" + "?api_key=" + apiKey;
            Console.WriteLine("External Api triggered");
            ParseCastJson(makeRequest(currentUrl).Result, movie_id);
        }

        private async void GetFilmography(int api_id, Guid person_id)
        {
            string currentUrl = baseUrl + "person/" + api_id.ToString() + "/movie_credits" + "?api_key=" + apiKey;
            
            Console.WriteLine("External Api triggered");
            
            ParseFilmographyJson(makeRequest(currentUrl).Result,person_id);
        }

        public async Task<int> SearchMovie(string str)
        {
            string currentUrl = baseUrl + "search/movie/?api_key=" +apiKey + "&query=" + str;
            Console.WriteLine($"currentUrl: {currentUrl}");

            Movie movie = new Movie();

            int id = ParseIdfromSearch(makeRequest(currentUrl).Result);

            return id;
        }

        public async Task<int> SearchPerson(string str)
        {
            string currentUrl = baseUrl + "search/person/?api_key=" + apiKey + "&query=" + str;
            Console.WriteLine($"currentUrl: {currentUrl}");

            Person person = new Person();

            int id = ParseIdfromSearch(makeRequest(currentUrl).Result);

            return id;
        }
        private Movie ParseMovieJson(string response)
        {
           // var f = new System.Text.Json.Serialization.JsonConverter<Movie>();

            JObject json = JObject.Parse(response);
            Movie mv = new Movie();

            mv.ApiId = int.Parse(json.Property("id").Value.ToString());
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

        private Person ParsePersonJson(string response)
        {

            JObject json = JObject.Parse(response);
            Person person = new Person();

            person.ApiId = int.Parse(json.Property("id").Value.ToString());

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

        private void ParseCastJson(string response, Guid movie_id)
        {

            JObject json = JObject.Parse(response);
            Cast cast = new Cast();

            cast.MovieId = movie_id;
            for (int i = 0; i < 10; i++)
            {
                JToken tempCast = json["cast"][i];
                cast.Name =(string)tempCast["name"];
                cast.KnownForDepartment = (string)tempCast["known_for_department"];
                cast.Character = (string)tempCast["character"];

                _castRepository.Add(cast);
            }
        }

        private void ParseFilmographyJson(string response, Guid person_id)
        {

            JObject json = JObject.Parse(response);
            Filmography filmo = new Filmography();  

            filmo.PersonId = person_id;
            for (int i = 0; i < 10; i++)
            {
                JToken tempCast = json["cast"][i];
                filmo.Title = (string)tempCast["title"];
                filmo.Character = (string)tempCast["character"];

                _filmographyRepository.Add(filmo);
            }

        }

        private int ParseIdfromSearch(string response)
        {
            JObject json = JObject.Parse(response);
            int id = int.Parse((string)json["results"][0]["id"]);
            return id;
        }

        private async Task<String> makeRequest(string url)
        {
            using HttpResponseMessage response = await client.GetAsync(url);

            //recursive :)
            if (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                System.Uri newUri = response.Headers.Location;
                string str = newUri.ToString();
                return makeRequest(str).Result;
            }
            else
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }

            
        }

        
    }    
}