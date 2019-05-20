using System;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;

namespace Ksiegarnia.Controllers
{
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet("[action]")]
        public IActionResult GetBooks(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooks(page, pageSize);
            return new JsonResult(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetBooksByType(int page, int pageSize, Guid typeId)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooksByType(page, pageSize, typeId);
            return new JsonResult(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetBooksByCategory(int page, int pageSize, Guid typeId, Guid categoryId)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooksByTypeAndCategory(page, pageSize, typeId, categoryId);
            return new JsonResult(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetTypes()
        {
            var types = bookService.GetTypes();
            return new JsonResult(types);
        }

        [HttpGet("[action]")]
        public IActionResult GetCategories(Guid typeId)
        {
            var cateogries = bookService.GetCategoriesByType(typeId);
            return new JsonResult(cateogries);
        }

        [HttpGet("[action]")]
        public IActionResult GetBook(Guid bookId)
        {
            var book = bookService.ShowBookDetails(bookId);
            return new JsonResult(book);
        }
    }
}