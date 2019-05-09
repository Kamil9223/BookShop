using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class Book
    {
        public Guid BookId { get; protected set; }
        public string Title { get; protected set; }
        public decimal Price { get; protected set; }
        public int NumberOfPages { get; protected set; }
        public string Description { get; protected set; }
        public string ShortDescription { get; protected set; }
        public int NumberOfPieces { get; protected set; }
        public string PhotoUrl { get; protected set; }
        
        public virtual Guid TypeCategoryId { get; protected set; }
        public virtual TypeCategory TypeCategory { get; protected set; }
        public virtual IEnumerable<BookInOrder> BooksInOrder { get; protected set; }

        protected Book() { }

        public Book(string title, decimal price, int numberOfPages, 
            string shortDescription, int numberOfPieces, Guid typeCategoryId)
        {
            BookId = Guid.NewGuid();
            Title = title;
            Price = price;
            NumberOfPages = numberOfPages;
            ShortDescription = shortDescription;
            NumberOfPieces = numberOfPieces;
            TypeCategoryId = typeCategoryId;
        }
    }

    //public enum Category
    //{
    //    Novel = 0,
    //    Detective_Story = 1,
    //    Fantasy = 2,
    //    Poetry = 3,
    //    Short_Story = 4,
    //    Comedy = 5,
    //    Thriller = 6
    //}
}
