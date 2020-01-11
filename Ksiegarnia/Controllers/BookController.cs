using System;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IServices;
using System.Threading.Tasks;

namespace Ksiegarnia.Controllers
{//zamienic DTO (do usuniecia) na responses, zwracac jakies inne statuscody?, poprawic w Order, sprawdzic w bazie, testy jednostowe, odpalic i skonfigurowsc integacyjne, postman
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

        [HttpGet("Books/Types/{typeId}")]
        public async Task<IActionResult> GetBooksByType(Guid typeId, int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = await bookService.GetBooksByType(page, pageSize, typeId);
            return new JsonResult(books);
        }

        [HttpGet("Books/Types/{typeId}/Categories/{categoryId}")]
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

        [HttpGet("Types/{typeId}")]
        public async Task<IActionResult> GetType(Guid typeId)
        {
            var type = await bookService.GetType(typeId);

            if (type == null)
            {
                return NotFound();
            }
            return new JsonResult(type);
        }

        [HttpGet("Types/{typeId}/Categories")]
        public async Task<IActionResult> GetCategories(Guid typeId)
        {
            var cateogries = await bookService.GetCategoriesByType(typeId);
            return new JsonResult(cateogries);
        }

        [HttpGet("Books/{bookId}")]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var book = await bookService.ShowBookDetails(bookId);
            return new JsonResult(book);
        }
    }
}