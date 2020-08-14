using Core.IRepositories;
using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.AdminRepositories
{
    public interface IAdminCategoryRepository : ICategoryRepository
    {
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task RemoveCategory(Guid categoryId);
        Task SaveChanges();
    }
}
