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

        public ICategoryRepository CategoryRepository { get { return categoryRepository; } }
        public ITypeRepository TypeRepository { get { return typeRepository; } }

        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository,
            ITypeRepository typeRepository, ITypeCategoryRepository typeCategoryRepository)
        {
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
            this.typeRepository = typeRepository;
            this.typeCategoryRepository = typeCategoryRepository;
        }

        public IEnumerable<BookDTO> GetBooks(int page, int pageSize)
        {
            var books = bookRepository.GetBooks(page, pageSize);
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

        public Book ShowBookDetails(Guid bookId)
        {
            var book = bookRepository.GetBook(bookId);
            if (book == null)
                throw new Exception("Book with provided Id doesn't exist");

            return book;
        }

        public Guid AddTypeCategoryRelation(Guid categoryId, Guid typeId)
        {
            var category = categoryRepository.GetCategory(categoryId);
            if (category == null)
                throw new Exception("Category with provided Id doesn't exist.");

            var type = typeRepository.GetType(typeId);
            if (type == null)
                throw new Exception("Type with provided Id doesn't exist");

            var existingRelation = typeCategoryRepository.GetExistingRelation(categoryId, typeId);
            if (existingRelation != null)
                throw new Exception("Relation already exist");

            var relation = new TypeCategory(categoryId, typeId);
            typeCategoryRepository.AddTypeCategoryRelation(relation);
            typeCategoryRepository.SaveChanges();

            return relation.TypeCategoryId;
        }
    }
}
