using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using CodingChallenge.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public ILibraryService LibraryService { get; private set; }

        /// <summary>
        /// MoviesController Constructor
        /// </summary>
        /// <param name="libraryService"></param>
        public MoviesController(ILibraryService libraryService)
        {
            LibraryService = libraryService;
        }
        /// <summary>
        /// GET:/api/Movies?sort=Title&dir=ASC
        /// </summary>
        /// <param name="options"></param>
        /// <param name="SearchByTtile"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery] GridOptions options, string SearchByTtile)
        {
            options.TotalItems = LibraryService.SearchMoviesCount("");
            if (options.SortColumn == null)
                options.SortColumn = "ID";
            var model = new MovieListViewModel
            {
                GridOptions = options,
                Movies = LibraryService.SearchMovies(SearchByTtile, null, null, options.SortColumn).ToList()
            };

            return model.Movies;
        }

        /// <summary>
        ///  GET api/Movie/GetMoviesByTitle?title=test
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>        
        [Route("GetMoviesByTitle")]
        [HttpGet]
        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            var movies = LibraryService.GetMoviesByTitle(title);

            return movies;
        }

        /// <summary>
        ///  GET api/Movie/GetMoviesByRating?isAbove=true&movieRating=7
        /// </summary>
        /// <param name="isAbove"></param>
        /// <param name="movieRating"></param>
        /// <returns></returns>
        [Route("GetMoviesByRating")]
        [HttpGet]
        public IEnumerable<Movie> GetMoviesByRating(bool isAbove, int movieRating)
        {
            var movies = LibraryService.GetMoviesByRating(isAbove, movieRating);

            return movies;
        }

        /// <summary>
        /// GET api/Movie/GetMoviesBetweenDates?startYear=1990&mendYear=2000
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        [Route("GetMoviesBetweenDates")]
        [HttpGet]
        public IEnumerable<Movie> GetMoviesBetweenDates(int startYear, int endYear)
        {
            var movies = LibraryService.GetMoviesBetweenDates(startYear, endYear);

            return movies;
        }

        /// <summary>
        /// GET api/Movie/GetMoviesByFranchise?franchise=star
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [Route("GetMoviesByFranchise")]
        [HttpGet]
        public IEnumerable<Movie> GetMoviesByFranchise(string franchise)
        {
            var movies = LibraryService.GetMoviesByFranchise(franchise);

            return movies;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
