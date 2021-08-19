using CodingChallenge.DataAccess;

namespace CodingChallenge.Angular.Model
{
    public class GridOptions
    {
        public GridOptions()
        {
            ItemsPerPage = 10;
        }

        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public int PageCount
        {
            get { return (TotalItems / ItemsPerPage) + (TotalItems % ItemsPerPage > 0 ? 1 : 0); }
        }
    }
}
