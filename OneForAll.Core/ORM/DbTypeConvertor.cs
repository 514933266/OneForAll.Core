using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneForAll.Core.ORM
{
    public class DbTypeConvertor
    {
        public static DbType Convert<T>(T parameter, Type type)
        {
            string typeName = string.Empty;
            if (type != null)
            {
                typeName = type.Name;
            }
            else
            {
                typeName = typeof(T).Name;
            }
            return (DbType)Enum.Parse(typeof(DbType), typeName);
        }
    }
}
