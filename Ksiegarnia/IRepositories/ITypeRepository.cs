using System;
using System.Collections.Generic;

namespace Ksiegarnia.IRepositories
{
    public interface ITypeRepository  
    {
        Models.Type GetType(Guid typeId);
        IEnumerable<Models.Type> GetTypes();
        void AddType(Models.Type type);
        void UpdateType(Models.Type type);
        void RemoveType(Guid typeId);
        void SaveChanges();
    }
}
