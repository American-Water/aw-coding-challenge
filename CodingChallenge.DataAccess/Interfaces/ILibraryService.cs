using CodingChallenge.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.DataAccess.Interfaces
{
    public interface ILibraryService
    {
        /// <summary>
        /// SearchMovies
        /// </summary>
        /// <param name="title"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = null, SortDirection sortDirection = SortDirection.Ascending);

        /// <summary>
        /// SearchMovies count
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        int SearchMoviesCount(string title);

        /// <summary>
        /// SearchMovies title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        IEnumerable<Movie> GetMoviesByTitle(string title);

        /// <summary>
        /// SearchMoviesrating
        /// </summary>
        /// <param name="isAbove"></param>
        /// <param name="movieRating"></param>
        /// <returns></returns>
        IEnumerable<Movie> GetMoviesByRating(bool isAbove, int movieRating);

        /// <summary>
        /// SearchMovies dates
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        IEnumerable<Movie> GetMoviesBetweenDates(int startYear, int endYear);

        /// <summary>
        /// SearchMovies franchise
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        IEnumerable<Movie> GetMoviesByFranchise(string franchise);
    }
}
