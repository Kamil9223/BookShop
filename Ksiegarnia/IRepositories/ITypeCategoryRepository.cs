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
        void AddTypeCategoryRelation(TypeCategory relation);
        void UpdateTypeCategoryRelation(TypeCategory relation);
        void RemoveTypeCategoryRelation(Guid relationId);
        void SaveChanges();

    }
}
