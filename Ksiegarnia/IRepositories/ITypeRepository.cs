using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ksiegarnia.IRepositories
{
    public interface ITypeRepository  
    {
        Task<Models.Type> GetType(Guid typeId);
        Task<Models.Type> GetType(string typeName);
        Task<IEnumerable<Models.Type>> GetTypes();
        Task AddType(Models.Type type);
        Task UpdateType(Models.Type type);
        Task RemoveType(Guid typeId);
        Task SaveChanges();
    }
}
