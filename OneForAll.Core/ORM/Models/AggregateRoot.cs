using OneForAll.Core.Extension;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneForAll.Core.ORM.Models
{
    public class AggregateRoot<TType> : Entity<TType>, IAggregateRoot<TType>
    {
        public virtual void MapFrom<T>(T entity)
        {
            var pros = ReflectionHelper.GetPropertys(this);
            var desPros = ReflectionHelper.GetPropertys(entity);
            pros.ForEach(e =>
            {
                if (e.Name.ToLower() == "id") return;
                var pro = desPros.FirstOrDefault(w => w.Name.Equals(e.Name));
                if (pro != null) e.SetValue(this, pro.GetValue(entity));
            });
        }
    }
}
