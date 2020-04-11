using Ksiegarnia.Models;
using System;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ITypeCategoryRepository
    {
        Task <TypeCategory> GetTypeCategoryRelation(Guid relationId);
        Task<Guid> GetExistingRelation(Guid categoryId, Guid typeId);
        Task AddTypeCategoryRelation(TypeCategory relation);
        Task UpdateTypeCategoryRelation(TypeCategory relation);
        Task RemoveTypeCategoryRelation(Guid relationId);
        Task SaveChanges();
    }
}
