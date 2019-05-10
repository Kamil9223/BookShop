using System;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.Models;
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
        public IActionResult GetBook(Guid bookId)
        {
            var book = bookService.ShowBookDetails(bookId);
            return new JsonResult(book);
        }
    }
}