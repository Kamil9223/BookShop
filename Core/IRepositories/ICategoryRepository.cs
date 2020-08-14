using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategory(Guid categoryId);
        Task<IEnumerable<Category>> GetCategories();
    }
}
