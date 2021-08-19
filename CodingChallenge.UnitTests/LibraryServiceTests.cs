using System.Collections.Generic;
using System.Linq;
using CodingChallenge.DataAccess;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.DataAccess.Models;
using NUnit.Framework;

namespace CodingChallenge.UnitTests
{
    [TestFixture]
    public class LibraryServiceTests
    {
        public ILibraryService LibraryService { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            LibraryService = new LibraryService();
        }

        [Test]
        public void TestSearchMoviesCountReturnsExpectedCountWhenDuplicatesAreRemoved()
        {
            var count = LibraryService.SearchMoviesCount("");
            Assert.AreEqual(28, count);
        }

        [Test]
        public void TestSearchMoviesOnlyReturnsResultsByTitleWhenTitleIsProvided()
        {
            var movies = LibraryService.SearchMovies("Jaws");
            Assert.AreEqual(3, movies.Count());
        }

        [Test]
        public void TestSearchMoviesCanSortTitlesInAscendingOrderWhileIgnoringArticles()
        {
            var sorted = LibraryService.SearchMovies("", null, null, "Title", "asc").ToArray();

            var first = sorted[2];
            Assert.AreEqual(28, first.ID);
            Assert.AreEqual("An American Werewolf in London", first.Title);

            var second = sorted[3];
            Assert.AreEqual(24, second.ID);
            Assert.AreEqual("Back to the Future", second.Title);
        }

        [Test]
        public void TestSearchMoviesCanSortByYearInAscendingOrder()
        {
            var sorted = LibraryService.SearchMovies("", null, null, "Year", "asc");
            Assert.AreEqual(6, sorted.First().ID);
        }

        [Test]
        public void TestSearchMoviesCanSortByYearInDescendingOrder()
        {
            var sorted = LibraryService.SearchMovies("", null, null, "Year", "asc");
            Assert.AreEqual(6, sorted.First().ID);
        }
    }
}
