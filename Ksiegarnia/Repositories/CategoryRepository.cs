using Ksiegarnia.DB;
using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;

namespace Ksiegarnia.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookShopContext context;

        public CategoryRepository(BookShopContext context)
        {
            this.context = context;
        }

        public Category GetCategory(Guid categoryId)
            => context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);

        public IEnumerable<Category> GetCategories()
            => context.Categories.ToList();

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
        }

        public void RemoveCategory(Guid categoryId)
        {
            var category = GetCategory(categoryId);
            context.Categories.Remove(category);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void test()
        {
            //id kategorii powiesc z bazy
            var novelCatId = context.Categories.SingleOrDefault(x => x.CategoryName == "Novel").CategoryId;
            //id typu książka z bazy
            var BookTypeId = context.Types.SingleOrDefault(x => x.TypeName == "Book").TypeId;
            //id relacji o powyzszych typie i kateogrii
            var tcId = context.TypeCategories.SingleOrDefault(x => x.CategoryId == novelCatId && x.TypeId == BookTypeId).TypeCategoryId;
            //wszystkie książki o typie book
            var tcIds = context.TypeCategories.Where(x => x.TypeId == BookTypeId).ToList();
            //var books = context.Books.Where(x => x.TypeCategoryId == tcIds);
        }
    }
}
