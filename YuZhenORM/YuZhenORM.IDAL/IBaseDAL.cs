using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuZhenORM.Framework.Model;

namespace YuZhenORM.IDAL
{
    public interface IBaseDAL
    {
        /// <summary>
        ///查询一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find<T>(int id) where T : BaseModel;

        /// <summary>
        /// 查找所有记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> FindAll<T>() where T : BaseModel;

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        bool insert<T>(T t) where T : BaseModel;

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        bool delete<T>(int id) where T : BaseModel;

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        bool update<T>(T t) where T : BaseModel;
    }
}
