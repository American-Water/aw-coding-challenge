using CodingChallenge.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge.WebAPI.Models
{
    public class MovieListViewModel
    {
        public List<Movie> Movies { get; set; }
        public GridOptions GridOptions { get; set; }
    }
}
