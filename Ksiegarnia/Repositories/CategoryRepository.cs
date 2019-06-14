﻿using Ksiegarnia.DB;
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

        public IEnumerable<Category> GetCategoriesByType(Guid typeId)
        {
            var categoryIds = context.TypeCategories.Where(x => x.TypeId == typeId).Select(x => x.CategoryId).ToList();
            var categories = context.Categories.Where(x => categoryIds.Contains(x.CategoryId));
            return categories;
        }

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
    }
}