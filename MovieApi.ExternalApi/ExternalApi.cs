using dotnet_movie_api.src.DataAccess;
using dotnet_movie_api.src.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace MovieApi.ExternalApi
{
    static public class ExternalApi
    {
        static WebApplicationBuilder builder = WebApplication.CreateBuilder();
        static string baseUrl = "https://api.themoviedb.org/3/";
        static readonly HttpClient client = new HttpClient();
        static MovieDbContext ctx = new MovieDbContext();

        public static async void GetMovie(int id)
        {

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api_key"] = builder.Configuration.GetValue<string>("ExternalApiKey");
            string queryString = query.ToString();

            string currentUrl = baseUrl + "movie/" + id.ToString() + "?" + queryString;


            try
            {
                using HttpResponseMessage response = await client.GetAsync(currentUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                ctx.Movies.Add(ParseMovieJson(responseBody));
                ctx.SaveChanges();

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public static async void GetPerson(int id)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api_key"] = builder.Configuration.GetValue<string>("ExternalApiKey");
            string queryString = query.ToString();

            string currentUrl = baseUrl + "person/" + id.ToString() + "?" + queryString;


            try
            {
                using HttpResponseMessage response = await client.GetAsync(currentUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(ParsePersonJson(responseBody));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
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

        private static string ParsePersonJson(string response)
        {
            JObject json = JObject.Parse(response);

            json.Remove("adult");
            json.Remove("also_known_as");
            json.Remove("biography");
            json.Remove("gender");
            json.Remove("homepage");
            json.Remove("popularity");
            json.Remove("profile_path");

            return json.ToString();
        }

    }
}
