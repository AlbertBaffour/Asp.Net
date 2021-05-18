using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models.ViewModel
{
    public class ListMoviesViewModel
    {
        public List<Movie> Movies { get; set; }
        public SelectList Ratings { get; set; }
        public int ratingID { get; set; }
            
            
    }
}
