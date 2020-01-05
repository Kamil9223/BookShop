using Ksiegarnia.IRepositories;
using Ksiegarnia.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DTO;
using Ksiegarnia.Models;

namespace Ksiegarnia.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ITypeRepository typeRepository;
        private readonly ITypeCategoryRepository typeCategoryRepository;

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository,
            ITypeRepository typeRepository, ITypeCategoryRepository typeCategoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
            this.typeRepository = typeRepository;
            this.typeCategoryRepository = typeCategoryRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetBooks(int page, int pageSize)
        {
            var books = await bookRepository.GetBooks(page, pageSize);
            var booksDto = new List<BookDTO>();

            foreach(Book book in books)
            {
                booksDto.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    TypeCategoryId = book.TypeCategoryId,
                    TypeCategory = book.TypeCategory
                });
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByType(int page, int pageSize, Guid typeId)
        {
            var books = await bookRepository.GetBooksByType(typeId, page, pageSize);

            var booksDto = new List<BookDTO>();

            foreach (Book book in books)
            {
                booksDto.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    TypeCategoryId = book.TypeCategoryId,
                    TypeCategory = book.TypeCategory
                });
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksByTypeAndCategory(int page, int pageSize, Guid typeId, Guid categoryId)
        {
            var books = await bookRepository.GetBooksByTypeAndCategory(typeId, categoryId, page, pageSize);
            if (books == null)
                throw new Exception("Can not find books collection with provided type Id and category Id.");

            var booksDto = new List<BookDTO>();

            foreach (Book book in books)
            {
                booksDto.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    TypeCategoryId = book.TypeCategoryId,
                    TypeCategory = null
                });
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDTO>> GetBooksRandomly(int count)
        {
            var books = await bookRepository.GetBooksRandomly(count);
            var booksDto = new List<BookDTO>();

            foreach(Book book in books)
            {
                booksDto.Add(new BookDTO()
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    PhotoUrl = book.PhotoUrl,
                    Price = book.Price,
                    TypeCategoryId = book.TypeCategoryId,
                    TypeCategory = book.TypeCategory
                });
            }

            return booksDto;
        }

        public async Task<IEnumerable<Category>> GetCategoriesByType(Guid typeId)
        {
            var categories = await categoryRepository.GetCategoriesByType(typeId);
            return categories;
        }

        public async Task<IEnumerable<Models.Type>> GetTypes()
        {
            var types = await typeRepository.GetTypes();
            return types;
        }

        public async Task<Models.Type> GetType(string typeName)
        {
            var type = await typeRepository.GetType(typeName);
            return type;
        }

        public async Task<Book> ShowBookDetails(Guid bookId)
        {
            var book = await bookRepository.GetBook(bookId);
            if (book == null)
                throw new Exception("Book with provided Id doesn't exist");

            return book;
        }

        public async Task<Guid> AddTypeCategoryRelation(Guid categoryId, Guid typeId)
        {
            var category = await categoryRepository.GetCategory(categoryId);
            if (category == null)
                throw new Exception("Category with provided Id doesn't exist.");

            var type = await typeRepository.GetType(typeId);
            if (type == null)
                throw new Exception("Type with provided Id doesn't exist");

            var existingRelation = await typeCategoryRepository.GetExistingRelation(categoryId, typeId);
            if (existingRelation != null)
                throw new Exception("Relation already exist");

            var relation = new TypeCategory(categoryId, typeId);
            await typeCategoryRepository.AddTypeCategoryRelation(relation);
            await typeCategoryRepository.SaveChanges();

            return relation.TypeCategoryId;
        }

        public async Task AddBook(Book book)
        {//Dodać walidacje, zabronić dwie ksiązki o tym samym tytule, sprawdic nulle itd
            await bookRepository.AddBook(book);
            await bookRepository.SaveChanges();
        }
    }
}
