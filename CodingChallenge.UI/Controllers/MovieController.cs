using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingChallenge.DataAccess;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;

namespace CodingChallenge.UI.Controllers
{
    [RoutePrefix("api/Movie")]
    public class MovieController : ApiController
    {

        public ILibraryService LibraryService { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MovieController() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="libraryService"></param>
        public MovieController(ILibraryService libraryService)
        {
            LibraryService = libraryService;
        }

        /// <summary>
        /// GET api/Movie/GetMoviesByTitle?title=test
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
        /// GET api/Movie/GetMoviesByRating?isAbove=true&movieRating=7
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
    }
}