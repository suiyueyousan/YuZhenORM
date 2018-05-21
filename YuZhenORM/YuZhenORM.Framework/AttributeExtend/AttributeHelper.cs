using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YuZhenORM.Framework.AttributeExtend.Mapping;

namespace YuZhenORM.Framework.AttributeExtend
{
    public static class AttributeHelper
    {
        public static string GetColumnName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColumnAttribute), true))
            {
                ColumnAttribute attribute = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute), true);
                return attribute.GetColumnName();
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
