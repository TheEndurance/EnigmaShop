using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnigmaShop.Utilities
{
    public class Pager
    {
        public int NumberOfItems { get; set; }
        public int CurrentPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public Pager(int numberOfItems,int? page,int pageSize=10)
        {
            var currentPage = page ?? 1;
            var totalPages = (int)Math.Ceiling((decimal) numberOfItems / pageSize);
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (EndPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            NumberOfItems = numberOfItems;
            CurrentPage = currentPage;
            StartPage = startPage;
            EndPage = endPage;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}
