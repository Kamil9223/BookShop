using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(Guid categoryId);
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategoriesByType(Guid typeId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void RemoveCategory(Guid categoryId);
        void SaveChanges();
    }
}
