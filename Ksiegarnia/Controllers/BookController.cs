using System;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.Models;
using Ksiegarnia.IServices;
using Ksiegarnia.IRepositories;

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

        [HttpGet("[action]")]
        public IActionResult test()
        {//testowa
            var type = bookService.TypeRepository.GetType(Guid.Parse("74191B2B-C2BC-4E6D-9589-0E50AA73A543"));
            var categories = bookService.CategoryRepository.GetCategoriesByType(type.TypeId);
            return new JsonResult(categories);
        }
    }
}