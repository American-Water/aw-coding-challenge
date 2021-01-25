using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using CodingChallenge.Utilities;

namespace CodingChallenge.DataAccess
{
    public class LibraryService : ILibraryService
    {
        public LibraryService() { }

        private IEnumerable<Movie> GetMovies()
        {
            return _movies ?? (_movies = ConfigurationManager.AppSettings["LibraryPath"].FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies.Distinct().ToList());
        }
        private IEnumerable<Movie> _movies { get; set; }

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }

        //public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = null, SortDirection sortDirection = SortDirection.Ascending)
        //{
        //    var movies = GetMovies().Where(s => s.Title.Contains(title));
        //    if (skip.HasValue && take.HasValue)
        //    {
        //        movies = movies.Skip(skip.Value).Take(take.Value);
        //    }

        //    return movies.ToList();
        //}
        public IEnumerable<Movie> SearchMovies(string title = "a", int? skip = null, int? take = null, string sortColumn = "ID", SortDirection sortDirection = SortDirection.Ascending)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "a";
            }
            var movies = GetMovies().Where(s => s.Title.ToLower().Contains(title.ToLower()));

            var movieInfo = typeof(Movie).GetProperty(sortColumn);

            if (sortColumn != "Title")
            {
                movies = sortDirection == SortDirection.Ascending
                    ? movies.OrderBy(x => movieInfo.GetValue(x, null))
                    : movies.OrderByDescending(x => movieInfo.GetValue(x, null));
            }
            else
            {
                movies = sortDirection == SortDirection.Ascending
                    ? movies.OrderBy(x => RemoveLeadingArticle(x.Title))
                    : movies.OrderByDescending(x => RemoveLeadingArticle(x.Title));
            }


            if (skip.HasValue && take.HasValue)
            {
                movies = movies.Skip(skip.Value).Take(take.Value);
            }

            //Remove duplicates based on Title
            return movies.GroupBy(x => x.Title)
                .Select(x => x.FirstOrDefault())
                .ToList();
        }
        //Removes the leading article (the, a, an) for sorting purposes
        private string RemoveLeadingArticle(string title)
        {
            //no leading article
            if (String.IsNullOrEmpty(title))
                return title;
            //leading article is "the"
            if (title.StartsWith("the ", StringComparison.CurrentCultureIgnoreCase))
                return title.Substring(4).TrimStart();
            //leading article is "an"
            if (title.StartsWith("an ", StringComparison.CurrentCultureIgnoreCase))
                return title.Substring(3).TrimStart();
            //leading article is "a"
            if (title.StartsWith("a ", StringComparison.CurrentCultureIgnoreCase))
                return title.Substring(2).TrimStart();

            return title;
        }

        //Service call to get movies by title
        public IEnumerable<Movie> GetMoviesByTitle(string title)
        {
            //handle null
            title = title ?? "";

            var movies = GetMovies()
                .Where(s => s.Title.ToLower().Contains(title.ToLower()))
                .GroupBy(x => x.Title)
                .Select(x => x.FirstOrDefault());

            return movies;
        }

        //Service call to get movies between two years
        public IEnumerable<Movie> GetMoviesBetweenDates(int startYear, int endYear)
        {
            var movies = GetMovies().Where(s => s.Year > startYear && s.Year < endYear);

            return movies;
        }

        //Service call to get movies based on rating
        public IEnumerable<Movie> GetMoviesByRating(bool isAbove, int rating)
        {
            var movies = isAbove ? GetMovies().Where(s => s.Rating > rating) : GetMovies().Where(s => s.Rating < rating);

            return movies;
        }

        //Service call to get movies by franchise
        public IEnumerable<Movie> GetMoviesByFranchise(string franchise)
        {
            //handle null
            franchise = franchise ?? "";

            var movies = GetMovies().Where(s => s.Franchise.ToLower().Contains(franchise.ToLower()));

            return movies;
        }


    }
}

