using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuZhenORM.Framework
{
    public class StaticConstant
    {
        public static string SqlserverConnString = ConfigurationManager.ConnectionStrings["Customers"].ConnectionString;
    }
}
