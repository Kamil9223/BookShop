using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using BookService.Services.Interfaces;
using BookService.DTO;
using Core.Repositories;

namespace BookService.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<BookHeader>> GetBooks(int page, int pageSize)
        {
            var books = await bookRepository.GetBooks(page, pageSize);
            var bookResponse = new List<BookHeader>();

            foreach (Book book in books)
            {
                bookResponse.Add(new BookHeader()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    ShortDescription = book.ShortDescription
                });
            }

            return bookResponse;
        }

        public async Task<IEnumerable<BookHeader>> GetBooksByCategory(int page, int pageSize, Guid categoryId)
        {
            var books = await bookRepository.GetBooksByCategory(categoryId, page, pageSize);

            var bookResponse = new List<BookHeader>();

            foreach (Book book in books)
            {
                bookResponse.Add(new BookHeader()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    ShortDescription = book.ShortDescription
                });
            }

            return bookResponse;
        }

        public async Task<IEnumerable<BookHeader>> GetBooksRandomly(int count)
        {
            var books = await bookRepository.GetBooksRandomly(count);
            var bookResponse = new List<BookHeader>();

            foreach (Book book in books)
            {
                bookResponse.Add(new BookHeader()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    ShortDescription = book.ShortDescription
                });
            }

            return bookResponse;
        }

        public async Task<BookDetails> ShowBookDetails(Guid bookId)
        {
            var book = await bookRepository.GetBook(bookId);
            if (book == null)
                throw new Exception("Book with provided Id doesn't exist");

            return new BookDetails
            {
                BookId = book.BookId,
                Title = book.Title,
                PhotoUrl = book.PhotoUrl,
                Price = book.Price,
                Description = book.Description,
                NumberOfPages = book.NumberOfPages,
                NumberOfPieces = book.NumberOfPieces,
                CategoryId = book.CategoryId,
                CategoryName = book.Category.CategoryName
            };
        }

        public async Task<IEnumerable<CategoryInformations>> GetCategories()
        {
            var categories = await categoryRepository.GetCategories();
            var categoryResponse = new List<CategoryInformations>();

            foreach (var category in categories)
            {
                categoryResponse.Add(new CategoryInformations
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                });
            }

            return categoryResponse;
        }
    }
}
