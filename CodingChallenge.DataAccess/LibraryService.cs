using System;
using System.Collections.Generic;
using System.IO;
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
            return _movies ?? (_movies = "Library.xml".FromFileInExecutingDirectory().DeserializeFromXml<Library>().Movies);
        }
        private IEnumerable<Movie> _movies { get; set; }

        public int SearchMoviesCount(string title)
        {
            return SearchMovies(title).Count();
        }


        public IEnumerable<Movie> SearchMovies(string title, int? skip = null, int? take = null, string sortColumn = null, string sortDirection = "asc")
        {
            var movies = GetMovies().Where(s => s.Title.Contains(title));

            movies = movies.Distinct(new MoviesComparer()).ToList();

            if (!string.IsNullOrEmpty(sortColumn))
            {
                movies = Sort(movies, sortColumn, sortDirection);

            }

            if (skip.HasValue && take.HasValue)
            {
                movies = movies.Skip(skip.Value).Take(take.Value);
            }

            return movies.ToList();
        }

        private IEnumerable<Movie> Sort(IEnumerable<Movie> movies, string sortColumn, string sortDirection)
        {
            if (movies == null && string.IsNullOrEmpty(sortColumn)) return movies;

            switch (sortColumn.ToLower())
            {
                case "year":
                    movies = sortDirection != "asc"
                        ? movies.OrderByDescending(o => o.Year)
                        : movies.OrderBy(o => o.Year);
                    break;

                case "title":
                    movies = sortDirection != "asc"
                        ? movies.OrderByDescending(o => o.Title)
                        : movies.OrderBy(o => o.Title);
                    break;
            }

            return movies;
        }
    }

    class MoviesComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return x.Title == y.Title;
        }

        public int GetHashCode(Movie obj)
        {
            return (obj.Title).GetHashCode();
        }
    }
}
