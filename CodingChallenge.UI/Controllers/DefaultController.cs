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

        public ActionResult Index([ModelBinder(typeof(GridBinder))]GridOptions options, string searchText)
        {
            if (searchText != null)
            {
                options.SearchText = searchText;
                options.Page = 1;
            }
            else
            {
                options.SearchText = options.SearchText ?? "";
            }

            options.TotalItems = LibraryService.SearchMoviesCount(options.SearchText);

            if (options.SortColumn == null)
                options.SortColumn = "ID";

            var movies = LibraryService.SearchMovies(options.SearchText ?? "",
                    (options.Page - 1) * options.ItemsPerPage,
                    options.ItemsPerPage,
                    options.SortColumn,
                    options.SortDirection).ToList();

            var model = new MovieListViewModel
            {
                GridOptions = options,
                Movies = movies
            };
            return View(model);
            
        }
    }
}

