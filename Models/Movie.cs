using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieSearch.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string PosterPath { get; set; }

        public Dictionary<string,string> StreamingLinks { get; set; }
    } 
}