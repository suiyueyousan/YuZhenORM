using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuZhenORM.Framework.AttributeExtend;
using YuZhenORM.Framework.Model;

namespace YuZhenORM.DAL
{
    public class TSqlHelper<T> where T : BaseModel
    {
        public static string FindAllSql=null;
        public static string FindSql = null;
        public static string InsertSql = null;
        public static string DeleteSql = null;
        public static string UpdateSql = null;

        static TSqlHelper() {
            Type type = typeof(T);
            string columnString = String.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            FindSql = $"SELECT {columnString} FROM [{type.Name}] WHERE Id=";
            FindAllSql = $"SELECT {columnString} FROM [{type.Name}]";    
            DeleteSql= $"DELETE  FROM [{type.Name}] WHERE Id=";
        }
    }
}
