using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class Type
    {
        public Guid TypeId { get; protected set; }
        public string TypeName { get; protected set; }

        public virtual IEnumerable<TypeCategory> TypeCategories { get; protected set; }

        protected Type() { }

        public Type(string typeName)
        {
            TypeId = Guid.NewGuid();
            TypeName = typeName;
        }
    }
}
