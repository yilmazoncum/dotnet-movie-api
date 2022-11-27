using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace dotnet_movie_api.src.ExternalApi
{
    static public class ExternalApi
    {
        static WebApplicationBuilder builder = WebApplication.CreateBuilder();
        static String baseUrl = "https://api.themoviedb.org/3/";
        static readonly HttpClient client = new HttpClient();


        public static async void GetMovie(int id)
        {
            
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["api_key"] = builder.Configuration.GetValue<String>("ExternalApiKey");
            string queryString = query.ToString();

            String currentUrl = baseUrl + "movie/" + id.ToString() + "?" + queryString;


            try
            {
                using HttpResponseMessage response = await client.GetAsync(currentUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ParseMovieJson(responseBody);
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
            query["api_key"] = builder.Configuration.GetValue<String>("ExternalApiKey");
            string queryString = query.ToString();

            String currentUrl = baseUrl + "person/" + id.ToString() + "?" + queryString;


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

        private static String ParseMovieJson(String response)
        {
            JObject json = JObject.Parse(response);

            json.Remove("adult");
            json.Remove("backdrop_path");
            json.Remove("belongs_to_collection");
            json.Remove("genres");
            json.Remove("homepage");
            json.Remove("original_language");
            json.Remove("popularity");
            json.Remove("production_companies");
            json.Remove("production_countries");
            json.Remove("spoken_languages");
            json.Remove("status");
            json.Remove("tagline");
            json.Remove("video");

            return json.ToString();
        }
        private static String ParsePersonJson(String response)
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
