using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.IServices;
using Infrastructure.Contracts.Responses;
using Infrastructure.Exceptions;
using Core.IRepositories;
using Core.Models;

namespace Infrastructure.Services
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

        public async Task<IEnumerable<BookHeaderResponse>> GetBooks(int page, int pageSize)
        {
            var books = await bookRepository.GetBooks(page, pageSize);
            var bookResponse = new List<BookHeaderResponse>();

            foreach(Book book in books)
            {
                bookResponse.Add(new BookHeaderResponse()
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

        public async Task<IEnumerable<BookHeaderResponse>> GetBooksByCategory(int page, int pageSize, Guid categoryId)
        {
            var books = await bookRepository.GetBooksByCategory(categoryId, page, pageSize);

            var bookResponse = new List<BookHeaderResponse>();

            foreach (Book book in books)
            {
                bookResponse.Add(new BookHeaderResponse()
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

        public async Task<IEnumerable<BookHeaderResponse>> GetBooksRandomly(int count)
        {
            var books = await bookRepository.GetBooksRandomly(count);
            var bookResponse = new List<BookHeaderResponse>();

            foreach(Book book in books)
            {
                bookResponse.Add(new BookHeaderResponse()
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

        public async Task<BookResponse> ShowBookDetails(Guid bookId)
        {
            var book = await bookRepository.GetBook(bookId);
            if (book == null)
                throw new Exception("Book with provided Id doesn't exist");

            return new BookResponse
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

        public async Task AddBook(Book book)
        {
            var dbBook = bookRepository.GetBook(book.BookId);

            if (dbBook == null)
                throw new AlreadyExistException($"Book with id = {book.BookId} already exist.");

            await bookRepository.AddBook(book);
            await bookRepository.SaveChanges();
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            var categories = await categoryRepository.GetCategories();
            var categoryResponse = new List<CategoryResponse>();

            foreach(var category in categories)
            {
                categoryResponse.Add(new CategoryResponse
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                });
            }

            return categoryResponse;
        }
    }
}
