using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuZhenORM.Framework.Model;

namespace YuZhenORM.Model
{
    public class Company:BaseModel
    {
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 必须是可空类型，才能跟数据库对应
        /// </summary>
        public int? LastModifierId { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
