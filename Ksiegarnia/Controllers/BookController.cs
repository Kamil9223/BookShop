using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;

namespace Ksiegarnia.Controllers
{
    [Route("api/Book")]
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet("[action]")]
        public IActionResult GetBooks(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }
            var books = bookRepository.GetBooks(page, pageSize);
            return new JsonResult(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetBook(Guid bookId)
        {
            var book = bookRepository.GetBook(bookId);
            return new JsonResult(book);
        }
    }
}