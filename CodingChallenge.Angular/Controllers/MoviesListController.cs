using CodingChallenge.Angular.Model;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge.Angular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesListController : Controller
    {
        public ILibraryService LibraryService { get; private set; }
        public MoviesListController(ILibraryService libraryService)
        {
            LibraryService = libraryService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return null;
        }

        [HttpPost]
        public IEnumerable<Movie> Post(GridOptions options)
        {
            return LibraryService.SearchMovies("", 0,50, options.SortColumn, options.SortDirection).ToList();
        }

    }
}
