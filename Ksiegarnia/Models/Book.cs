using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public Guid BookId { get; protected set; }
        public string Title { get; protected set; }
        public Category Category { get; protected set; }
        public decimal Price { get; protected set; }
        public int NumberOfPages { get; protected set; }
        public string Description { get; protected set; }
        public string ShortDescription { get; protected set; }
        public int NumberOfBooks { get; protected set; }
        public IEnumerable<BookInOrder> BooksInOrder { get; protected set; }
    }

    public enum Category
    {
        Novel = 0,
        Detective_Story = 1,
        Fantasy = 2
    }
}
