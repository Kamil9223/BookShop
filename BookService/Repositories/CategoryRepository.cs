using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.IRepositories;
using Core.Models;
using DatabaseAccess.MSSQL_BookShop;

namespace BookService.Repositories
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
