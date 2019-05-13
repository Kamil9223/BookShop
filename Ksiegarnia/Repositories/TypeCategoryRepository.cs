using Ksiegarnia.DB;
using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;

namespace Ksiegarnia.Repositories
{
    public class TypeCategoryRepository : ITypeCategoryRepository
    {
        private readonly BookShopContext context;

        TypeCategoryRepository(BookShopContext context)
        {
            this.context = context;
        }

        public TypeCategory GetTypeCategoryRelation(Guid relationId)
            => context.TypeCategories.SingleOrDefault(tp => tp.TypeCategoryId == relationId);

        public void AddTypeCategoryRelation(TypeCategory relation)
        {
            context.TypeCategories.Add(relation);
        }

        public void UpdateTypeCategoryRelation(TypeCategory relation)
        {
            context.TypeCategories.Update(relation);
        }

        public void RemoveTypeCategoryRelation(Guid relationId)
        {
            var relation = GetTypeCategoryRelation(relationId);
            context.TypeCategories.Remove(relation);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
