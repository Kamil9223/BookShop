using System;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using System.Collections.Generic;

namespace Ksiegarnia.Controllers
{
    [Route("api")]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet("/Books")]
        public IActionResult GetBooks(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooks(page, pageSize);
            return new JsonResult(books);
        }

        [HttpGet("/Books/{typeId}")]
        public IActionResult GetBooksByType(Guid typeId, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooksByType(page, pageSize, typeId);
            return new JsonResult(books);
        }

        [HttpGet("/Books/{typeId}/{categoryId}")]
        public IActionResult GetBooksByCategory(Guid typeId, Guid categoryId, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooksByTypeAndCategory(page, pageSize, typeId, categoryId);
            return new JsonResult(books);
        }

        [HttpGet("/Books/Random/{count}")]
        public IActionResult GetBooksRandomly(int count)
        {
            if (count <= 0)
            {
                return BadRequest();
            }
            var books = bookService.GetBooksRandomly(count);
            return new JsonResult(books);
        }

        [HttpGet("/Types")]
        public IActionResult GetTypes()
        {
            var types = bookService.GetTypes();
            return new JsonResult(types);
        }

        [HttpGet("/Types/{typeName}")]
        public IActionResult GetType(string typeName)
        {
            if (String.IsNullOrEmpty(typeName))
            {
                return BadRequest();
            }
            var type = bookService.GetType(typeName);

            if (type == null)
            {
                return NotFound();
            }
            return new JsonResult(type);
        }

        [HttpGet("/Categories")]
        public IActionResult GetCategories(Guid typeId)
        {
            var cateogries = bookService.GetCategoriesByType(typeId);
            return new JsonResult(cateogries);
        }

        [HttpGet("/Book/{bookId}")]
        public IActionResult GetBook(Guid bookId)
        {
            var book = bookService.ShowBookDetails(bookId);
            return new JsonResult(book);
        }
    }
}