using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class TypeCategory
    {
        public Guid TypeCategoryId { get; protected set; }

        public virtual Guid CategoryId { get; protected set; }
        public virtual Category Category { get; protected set; }
        public virtual Guid TypeId { get; protected set; }
        public virtual Type Type { get; protected set; }
        public virtual IEnumerable<Book> Books { get; protected set; }

        protected TypeCategory() { }

        public TypeCategory(Guid categoryId, Guid typeId)
        {
            TypeCategoryId = Guid.NewGuid();
            CategoryId = categoryId;
            TypeId = typeId;
        }
    }
}
