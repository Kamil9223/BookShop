using Ksiegarnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ITypeCategoryRepository
    {
        TypeCategory GetTypeCategoryRelation(Guid relationId);
        Guid GetExistingRelation(Guid categoryId, Guid typeId);
        void AddTypeCategoryRelation(TypeCategory relation);
        void UpdateTypeCategoryRelation(TypeCategory relation);
        void RemoveTypeCategoryRelation(Guid relationId);
        void SaveChanges();
    }
}
