using MovieSearch.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace MovieSearch.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Error = "Please enter a valid movie name.";
                return View();
            }

            var movies = GetMoviesFromAPI(query);
            return View(movies); 
        }

        private List<Movie> GetMoviesFromAPI(string query)
        {
            string apiKey = "6039371b4e4d0a300329a3fc2c799414";
            string apiUrl = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={query}";
            List<Movie> movies = new List<Movie>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    dynamic result = JsonConvert.DeserializeObject(data);
                    foreach (var item in result.results)
                    {
                        movies.Add(new Movie
                        {
                            Title = item.title,
                            ReleaseDate = item.release_date,
                            Overview = item.overview,
                            Rating = item.vote_average,
                            PosterPath = item.poster_path != null ? $"https://image.tmdb.org/t/p/w500{item.poster_path}" : null,
                            StreamingLinks=new Dictionary<string, string>
                            {
                                {"Netflix", "https://www.netflix.com/search?q="+item.title },
                                {"Amazon Prime", "https://www.amazon.com/s?k=" + item.title  },
                                { "YouTube", "https://www.youtube.com/results?search_query=" + item.title },
                                   { "JioCinema", "https://www.jiocinema.com/search?q=" + item.title }
                            }
                        });
                    }
                }
            }
            return movies;
        }

    }
}
