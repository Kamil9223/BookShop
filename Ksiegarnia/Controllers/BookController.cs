using System;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using System.Threading.Tasks;

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

        [HttpGet("Books")]
        public async Task<IActionResult> GetBooks(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = await bookService.GetBooks(page, pageSize);
            return new JsonResult(books);
        }

        [HttpGet("Books/{typeId}")]
        public async Task<IActionResult> GetBooksByType(Guid typeId, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = await bookService.GetBooksByType(page, pageSize, typeId);
            return new JsonResult(books);
        }

        [HttpGet("Books/{typeId}/{categoryId}")]
        public async Task<IActionResult> GetBooksByCategory(Guid typeId, Guid categoryId, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = await bookService.GetBooksByTypeAndCategory(page, pageSize, typeId, categoryId);
            return new JsonResult(books);
        }

        [HttpGet("Books/Random/{count}")]
        public async Task<IActionResult> GetBooksRandomly(int count)
        {
            if (count <= 0)
            {
                return BadRequest();
            }
            var books = await bookService.GetBooksRandomly(count);
            return new JsonResult(books);
        }

        [HttpGet("Types")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await bookService.GetTypes();
            return new JsonResult(types);
        }

        [HttpGet("Types/{typeName}")]
        public async Task<IActionResult> GetType(string typeName)
        {
            if (String.IsNullOrEmpty(typeName))
            {
                return BadRequest();
            }
            var type = await bookService.GetType(typeName);

            if (type == null)
            {
                return NotFound();
            }
            return new JsonResult(type);
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories(Guid typeId)
        {
            var cateogries = await bookService.GetCategoriesByType(typeId);
            return new JsonResult(cateogries);
        }

        [HttpGet("Book/{bookId}")]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var book = await bookService.ShowBookDetails(bookId);
            return new JsonResult(book);
        }
    }
}