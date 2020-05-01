using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Contracts.Responses
{
    public class CategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
