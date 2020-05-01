using Ksiegarnia.IRepositories;
using Ksiegarnia.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using Ksiegarnia.Contracts.Responses;

namespace Ksiegarnia.Services
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

        public async Task<IEnumerable<BookResponse>> GetBooks(int page, int pageSize)
        {
            var books = await bookRepository.GetBooks(page, pageSize);
            var bookResponse = new List<BookResponse>();

            foreach(Book book in books)
            {
                bookResponse.Add(new BookResponse()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price
                });
            }

            return bookResponse;
        }

        public async Task<IEnumerable<BookResponse>> GetBooksByCategory(int page, int pageSize, Guid categoryId)
        {
            var books = await bookRepository.GetBooksByCategory(categoryId, page, pageSize);

            var bookResponse = new List<BookResponse>();

            foreach (Book book in books)
            {
                bookResponse.Add(new BookResponse()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price
                });
            }

            return bookResponse;
        }

        public async Task<IEnumerable<BookResponse>> GetBooksRandomly(int count)
        {
            var books = await bookRepository.GetBooksRandomly(count);
            var bookResponse = new List<BookResponse>();

            foreach(Book book in books)
            {
                bookResponse.Add(new BookResponse()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price
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
                Price = book.Price
            };
        }

        public async Task AddBook(Book book)
        {//Dodać walidacje, zabronić dwie ksiązki o tym samym tytule, sprawdic nulle itd
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
