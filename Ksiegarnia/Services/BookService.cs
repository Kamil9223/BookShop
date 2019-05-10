using Ksiegarnia.IRepositories;
using Ksiegarnia.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DTO;
using Ksiegarnia.Models;

namespace Ksiegarnia.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IEnumerable<BookDTO> GetBooks(int page, int pageSize)
        {
            var books = bookRepository.GetBooks(page, pageSize);
            var booksDto = new List<BookDTO>();

            foreach(Book book in books)
            {
                booksDto.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    TypeCategoryId = book.TypeCategoryId,
                    TypeCategory = book.TypeCategory
                });
            }

            return booksDto;
        }

        public Book ShowBookDetails(Guid bookId)
        {
            var book = bookRepository.GetBook(bookId);
            return book;
        }
    }
}
