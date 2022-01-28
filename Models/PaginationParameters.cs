using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class PaginationParameters
    {
        const int maxPageSize = 1;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
