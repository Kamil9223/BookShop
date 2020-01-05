﻿using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface IBookRepository
    {
        Task<Book> GetBook(Guid bookId);
        Task<Book> GetBook(string title);
        Task<IEnumerable<Book>> GetBooks(int page, int pageSize);
        Task<IEnumerable<Book>> GetBooksByType(Guid typeId, int page, int pageSize);
        Task<IEnumerable<Book>> GetBooksByTypeAndCategory(Guid typeId, Guid categoryId, int page, int pageSize);
        Task<IEnumerable<Book>> GetBooksRandomly(int count);
        Task AddBook(Book book);
        Task UpdateBook(Book book);
        Task RemoveBook(Guid bookId);
        Task SaveChanges();
    }
}
