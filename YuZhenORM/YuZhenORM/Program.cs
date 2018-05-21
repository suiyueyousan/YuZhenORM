using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuZhenORM.DAL;
using YuZhenORM.IDAL;
using YuZhenORM.Model;

namespace YuZhenORM
{
    class Program
    {
        static void Main(string[] args)
        {
            IBaseDAL basedal = new BaseDAL();
            //查询一条
            User user= basedal.Find<User>(1);
            //查询所有
            List<User> listuser = basedal.FindAll<User>();

            user.Name = "喻贞";
            //更新一条数据
            //bool update = basedal.update<User>(user);
            //插入一条数据
            //bool insert = basedal.insert<User>(user);
            //删除一条数据
            bool delete = basedal.delete<User>(2);


            //List<a> l= new List<a>();
            //for (int i = 0; i < 10; i++)
            //{
            //    a a = new a() { id = i+1 };
            //    l.Add(a);
            //}
            //var result = l.Select(x => x.id < 3 ? 0 : x.id).Where(x=>x!=0);

        }
        public class a {
            public int id { get; set; }
        }
    }
}
