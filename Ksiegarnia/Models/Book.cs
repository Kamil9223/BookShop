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

        protected Book() { }

        public Book(string title, Category category, decimal price, int numberOfPages, 
            string shortDescription, int numberOfBooks)
        {
            BookId = Guid.NewGuid();
            Title = title;
            Category = category;
            Price = price;
            NumberOfPages = numberOfPages;
            ShortDescription = shortDescription;
            NumberOfBooks = numberOfBooks;
        }
    }

    public enum Category
    {
        Novel = 0,
        Detective_Story = 1,
        Fantasy = 2
    }
}
