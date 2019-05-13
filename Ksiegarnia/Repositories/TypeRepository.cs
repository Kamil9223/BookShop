using Ksiegarnia.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.Models;
using Ksiegarnia.DB;

namespace Ksiegarnia.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly BookShopContext context;

        public TypeRepository(BookShopContext context)
        {
            this.context = context;
        }

        public Models.Type GetType(Guid typeId)
            => context.Types.SingleOrDefault(t => t.TypeId == typeId);

        public IEnumerable<Models.Type> GetTypes()
            => context.Types.ToList();

        public void AddType(Models.Type type)
        {
            context.Types.Add(type);
        }

        public void UpdateType(Models.Type type)
        {
            context.Types.Update(type);
        }

        public void RemoveType(Guid typeId)
        {
            var type = GetType(typeId);
            context.Types.Remove(type);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
