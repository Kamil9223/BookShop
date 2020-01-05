using Ksiegarnia.DB;
using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Microsoft.EntityFrameworkCore;

namespace Ksiegarnia.Repositories
{
    public class TypeCategoryRepository : ITypeCategoryRepository
    {
        private readonly BookShopContext context;

        public TypeCategoryRepository(BookShopContext context)
        {
            this.context = context;
        }

        public async Task<TypeCategory> GetTypeCategoryRelation(Guid relationId)
            => await context.TypeCategories.SingleOrDefaultAsync(tp => tp.TypeCategoryId == relationId);

        public async Task<Guid> GetExistingRelation(Guid categoryId, Guid typeId)
            => (await context.TypeCategories.SingleOrDefaultAsync(x => x.CategoryId == categoryId && x.TypeId == typeId)).TypeCategoryId;

        public async Task AddTypeCategoryRelation(TypeCategory relation)
        {
            await context.TypeCategories.AddAsync(relation);
        }

        public async Task UpdateTypeCategoryRelation(TypeCategory relation)
        {
            context.TypeCategories.Update(relation);
        }

        public async Task RemoveTypeCategoryRelation(Guid relationId)
        {
            var relation = await GetTypeCategoryRelation(relationId);
            context.TypeCategories.Remove(relation);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
