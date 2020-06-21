using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Category
    {
        public Guid CategoryId { get; protected set; }
        public string CategoryName { get; protected set; }
        public virtual IEnumerable<Book> Books { get; protected set; }

        protected Category() { }

        public Category(string categoryName)
        {
            CategoryId = Guid.NewGuid();
            CategoryName = categoryName;
        }
    }
}
