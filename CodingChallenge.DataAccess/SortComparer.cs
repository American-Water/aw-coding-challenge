using System;
using System.Collections.Generic;

namespace CodingChallenge.DataAccess
{
    public class SortComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(TrimArticles(x), TrimArticles(y), true);
        }

        private string TrimArticles(string title)
        {
            if (title.StartsWith("a ", StringComparison.InvariantCultureIgnoreCase))
            {
                return title.Substring(2).TrimStart();
            }
            else if (title.StartsWith("an ", StringComparison.InvariantCultureIgnoreCase))
            {
                return title.Substring(3).TrimStart();
            }
            else if (title.StartsWith("the ", StringComparison.InvariantCultureIgnoreCase))
            {
                return title.Substring(4).TrimStart();
            }
            else
            {
                return title;
            }
        }
    }
}
