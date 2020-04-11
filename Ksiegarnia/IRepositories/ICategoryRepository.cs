using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ICategoryRepository
    {
        Task <Category> GetCategory(Guid categoryId);
        Task <IEnumerable<Category>> GetCategories();
        Task <IEnumerable<Category>> GetCategoriesByType(Guid typeId);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task RemoveCategory(Guid categoryId);
        Task SaveChanges();
    }
}
