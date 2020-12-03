using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookService.Services.Interfaces;
using API.Requests.BookRequests;
using BookService.DTO;
using Microsoft.AspNetCore.Authorization;
using Core.AdminRepositories;

namespace API.Controllers
{
    [Route("api")]
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAdminBookRepository adminBookRepository;

        public BookController(IBookService bookService, IAdminBookRepository adminBookRepository)
        {
            this.bookService = bookService;
            this.adminBookRepository = adminBookRepository;
        }

        [HttpGet("Books")]
        public async Task<IActionResult> GetBooks([FromBody] PaginationRequest pagination)
        {
            var books = await bookService.GetBooks(pagination.Page, pagination.PageSize);
            return new JsonResult(books);
        }

        [HttpGet("Books/Category/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(Guid categoryId, [FromBody] PaginationRequest pagination)
        {
            var books = await bookService.GetBooksByCategory(pagination.Page, pagination.PageSize, categoryId);
            return new JsonResult(books);
        }

        [HttpGet("Books/Random")]
        public async Task<IActionResult> GetBooksRandomly(int count)
        {
            if (count <= 0)
            {
                return BadRequest();
            }
            var books = await bookService.GetBooksRandomly(count);
            return new JsonResult(books);
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var cateogries = await bookService.GetCategories();
            return new JsonResult(cateogries);
        }

        [HttpGet("Books/{bookId}")]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var book = await bookService.ShowBookDetails(bookId);
            return new JsonResult(book);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Books")]
        public async Task<IActionResult> AddBook([FromBody] BookDetails book)
        {
            await adminBookRepository.AddBook(new Core.Models.Book(
                Guid.NewGuid(),
                book.Title,
                book.Price,
                book.NumberOfPages,
                "",
                book.NumberOfPieces,
                book.CategoryId));

            return Ok();
        }
    }
}