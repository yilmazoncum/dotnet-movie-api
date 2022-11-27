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

                Console.WriteLine(responseBody);
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

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private static void ParseMovieJson(String response)
        {
            JObject json = JObject.Parse(response);
            Console.WriteLine(json);
        }
    }
} 
