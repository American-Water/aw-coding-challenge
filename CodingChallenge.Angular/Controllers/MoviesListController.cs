using CodingChallenge.Angular.Model;
using CodingChallenge.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

            var movies = new Movie[2];

            var movie = new Movie { ID = 1, Rating = 3.5, Title = "success", Year = DateTime.Now };

            movies[0] = movie;
            movies[1] = movie;

            return movies;
        }


        //public ActionResult Index([ModelBinder(typeof(GridBinder))] GridOptions options)
        //{
        //    options.TotalItems = LibraryService.SearchMoviesCount("");
        //    if (options.SortColumn == null)
        //        options.SortColumn = "ID";
        //    var model = new MovieListViewModel
        //    {
        //        GridOptions = options,
        //        Movies = LibraryService.SearchMovies("", (options.Page - 1) * options.ItemsPerPage, options.ItemsPerPage).ToList()
        //    };
        //    return View(model);
        //}



    }
}
