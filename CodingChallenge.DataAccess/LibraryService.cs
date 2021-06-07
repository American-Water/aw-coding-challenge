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
            return _movies ?? (_movies = ConfigurationManager.AppSettings["LibraryPath"].FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies);
        }
        private IEnumerable<Movie> _movies { get; set; }

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }

        public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = "ID", SortDirection sortDirection = SortDirection.Ascending)
        {
            var movies = GetMovies().Where(s => s.Title.ToLower().Contains(title.ToLower()));

            var propertyInfo = typeof(Movie).GetProperty(sortColumn);

            if (sortColumn != "Title")
            {
                movies = sortDirection == SortDirection.Ascending ? movies.OrderBy(x => propertyInfo.GetValue(x)) : movies.OrderByDescending(x => propertyInfo.GetValue(x));
            }
            else
            {
                movies = sortDirection == SortDirection.Ascending ? movies.OrderBy(x => x.Title, new SortComparer()) : movies.OrderByDescending(x => x.Title, new SortComparer());
            }

            if (skip.HasValue && take.HasValue)
            {
                movies = movies.Skip(skip.Value).Take(take.Value);
            }

            return movies.ToList().Distinct(new MovieTitleComparer());
        }
    }
}
