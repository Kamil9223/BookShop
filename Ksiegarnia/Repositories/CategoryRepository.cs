using Ksiegarnia.DB;
using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Microsoft.EntityFrameworkCore;

namespace Ksiegarnia.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookShopContext context;

        public CategoryRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<Category> GetCategory(Guid categoryId)
            => await context.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);

        public async Task<IEnumerable<Category>> GetCategories()
            => await context.Categories.ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesByType(Guid typeId)
        {
            var categories = await (from typeCat in context.TypeCategories
                                    where typeCat.TypeId == typeId
                                    join category in context.Categories on typeCat.CategoryId equals category.CategoryId
                                    select category).ToListAsync();

            return categories;
        }

        public async Task AddCategory(Category category)
        {
            await context.Categories.AddAsync(category);
        }

        public async Task UpdateCategory(Category category)
        {
            context.Categories.Update(category);
        }

        public async Task RemoveCategory(Guid categoryId)
        {
            var category = await GetCategory(categoryId);
            context.Categories.Remove(category);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
