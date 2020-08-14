using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.AdminRepositories
{
    public interface ICategoryRepository
    {
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task RemoveCategory(Guid categoryId);
        Task SaveChanges();
    }
}
