using CodingChallenge.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace CodingChallenge.DataAccess
{
    class MovieTitleComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (x is null || y is null)
                return false;

            return x.Title == y.Title;
        }
        public int GetHashCode(Movie movie)
        {
            if (movie is null) return 0;
            int hashTitle = movie.Title == null ? 0 : movie.Title.GetHashCode();
            return hashTitle;
        }
    }
}
