using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Contracts.Requests
{
    public class PaginationRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
