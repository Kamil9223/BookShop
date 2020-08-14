using System;
using System.Collections.Generic;

namespace Core.Models
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
        public Guid CategoryId { get; protected set; }
        public virtual Category Category { get; protected set; }
        public virtual ICollection<BookInOrder> BooksInOrder { get; protected set; }

        protected Book() { }

        public Book(Guid bookId, string title, decimal price, int numberOfPages,
            string shortDescription, int numberOfPieces, Guid categoryId)
        {
            BookId = bookId;
            Title = title;
            Price = price;
            NumberOfPages = numberOfPages;
            ShortDescription = shortDescription;
            NumberOfPieces = numberOfPieces;
            CategoryId = categoryId;
        }

        public void DecreaseAmount(int amount)
        {
            //TODO
            NumberOfPieces -= amount;
        }
    }
}
