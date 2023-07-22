using MovieTask.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MovieTask.Services
{
    public class MovieApiService
    {
        public static dynamic Data { get; set; }
        public static dynamic SingleData { get; set; }

        public static async Task<List<Movie>> GetMoviesAsync(string movie)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();

            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=3eb9dfa5&s={movie}&plot=full").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);
            List<Movie> movies = new List<Movie>();

            try
            {
                for (int i = 0; i < 10; i++)
                {
                    response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=3eb9dfa5&t={Data.Search[i].Title}&plot=full").Result;
                    str = response.Content.ReadAsStringAsync().Result;
                    SingleData = JsonConvert.DeserializeObject(str);

                    var myMovie = new Movie
                    {
                        About = SingleData.Plot,
                        ImagePath = SingleData.Poster,
                        MovieName = SingleData.Title,
                        Rating = SingleData.imdbRating,
                    };
                    movies.Add(myMovie);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return movies;
        }
    }
}
