using System.Linq;
using System.Web.Mvc;
using CodingChallenge.DataAccess;
using CodingChallenge.DataAccess.Interfaces;
using CodingChallenge.UI.Models;

namespace CodingChallenge.UI.Controllers
{
    public class DefaultController : Controller
    {
        public ILibraryService LibraryService { get; private set; }

        public DefaultController() { }

        public DefaultController(ILibraryService libraryService)
        {
            LibraryService = libraryService;
        }

        public ActionResult Index([ModelBinder(typeof(GridBinder))]GridOptions options)
        {
            options.TotalItems = LibraryService.SearchMoviesCount(options.TitleSearch);
            if (options.SortColumn == null)
                options.SortColumn = "ID";
            var model = new MovieListViewModel
            {
                GridOptions = options,
                TitleSearch = options.TitleSearch,
                Movies = LibraryService.SearchMovies(options.TitleSearch, (options.Page - 1) * options.ItemsPerPage, options.ItemsPerPage, options.SortColumn, options.SortDirection).ToList()
            };
            return View(model);
        }
    }
}
