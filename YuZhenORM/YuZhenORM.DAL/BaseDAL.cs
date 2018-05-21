using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuZhenORM.Framework;
using YuZhenORM.Framework.AttributeExtend;
using YuZhenORM.Framework.Model;
using YuZhenORM.IDAL;

namespace YuZhenORM.DAL
{
    public class BaseDAL : IBaseDAL
    {
        public bool insert<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);
            var propNoIdArray = type.GetProperties().Where(p => !p.Name.Equals("Id"));
            string ColumnValue = String.Join(",", propNoIdArray.Select(p => $"@{p.GetColumnName()}"));
            
            string ColumnString = String.Join(",", propNoIdArray.Select(p => $"[{p.GetColumnName()}]"));

            var parameters = propNoIdArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            string InsertSql = $"INSERT INTO [{type.Name}] ({ColumnString}) VALUES ({ColumnValue})";

            Func<SqlCommand, int> func =c => c.ExecuteNonQuery();

            int result= this.Execute<int>(InsertSql, func);
            if (result == 1)
                return true;
            else
                //throw new Exception("result！=1");
                return false;
            //using (SqlConnection conn = new SqlConnection(StaticConstant.SqlserverConnString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(InsertSql, conn))
            //    {
            //        conn.Open();
            //        SqlTransaction transaction = conn.BeginTransaction();
            //        cmd.Parameters.AddRange(parameters);
            //        cmd.Transaction = transaction;
            //        int result = cmd.ExecuteNonQuery();
            //        if (result == 1)
            //        {
            //            transaction.Commit();
            //            return true;
            //        }
            //        else
            //        {
            //            transaction.Rollback();
            //            return false;
            //        }
            //    }
            //}

        }

        public bool delete<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"{TSqlHelper<T>.DeleteSql}{id}";
            using (SqlConnection conn = new SqlConnection(StaticConstant.SqlserverConnString))
            {
                Func<SqlCommand,int> func=c=> c.ExecuteNonQuery();

                int result = this.Execute<int>(sql, func);
                if (result == 1)
                    return true;
                else
                    //throw new Exception("result！=1");
                    return false;
                //using (SqlCommand cmd = new SqlCommand(sql, conn))
                //{
                //    conn.Open();
                //    SqlTransaction transaction = conn.BeginTransaction();
                //    cmd.Transaction = transaction;
                //    int result = cmd.ExecuteNonQuery();
                //    if (result == 1)
                //    {
                //        transaction.Commit();
                //        return true;
                //    }
                //    else
                //    {
                //        transaction.Rollback();
                //        return false;
                //    }
                //}
            }
        }

        public bool update<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);
            var propNoIdArray = type.GetProperties().Where(p => !p.Name.Equals("Id"));
            string columnNoIdString = String.Join(",", propNoIdArray.Select(p => $"[{p.GetColumnName()}]=@{p.GetColumnName()}"));
            var parameters = propNoIdArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            string sql = $"UPDATE [{type.Name}] SET {columnNoIdString} WHERE Id={t.Id}";

            using (SqlConnection conn = new SqlConnection(StaticConstant.SqlserverConnString))
            {
                Func<SqlCommand, int> func = c =>
                {
                    c.Parameters.AddRange(parameters);
                    return c.ExecuteNonQuery();
                };

                int result = this.Execute<int>(sql, func);
                if (result == 1)
                    return true;
                else
                    //throw new Exception("result！=1");
                    return false;
                //using (SqlCommand cmd = new SqlCommand(sql, conn))
                //{
                //    conn.Open();
                //    SqlTransaction transaction = conn.BeginTransaction();
                //    cmd.Parameters.AddRange(parameters);
                //    cmd.Transaction = transaction;
                //    int result = cmd.ExecuteNonQuery();
                //    if (result == 1)
                //    {
                //        transaction.Commit();
                //        return true;
                //    }
                //    else
                //    {
                //        transaction.Rollback();
                //        return false;
                //    }
                //}
            }

        }

        public T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);

            string sql = $"{TSqlHelper<T>.FindSql}{id}";
            Func<SqlCommand, T> func = c => {
                SqlDataReader reader = c.ExecuteReader();
                T t = (T)Activator.CreateInstance(type);
                while (reader.Read())
                {
                    foreach (var field in type.GetProperties())
                    {
                        object value = reader[field.GetColumnName()];
                        if (value is DBNull)
                            value = null;
                        field.SetValue(t, value);
                    }
                }
                return t;
            };
            return this.Execute<T>(sql, func);
            //using (SqlConnection conn = new SqlConnection(StaticConstant.SqlserverConnString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        conn.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        T t = (T)Activator.CreateInstance(type);
            //        while (reader.Read())
            //        {
            //            foreach (var field in type.GetProperties())
            //            {
            //                object value = reader[field.GetColumnName()];
            //                if (value is DBNull)
            //                    value = null;
            //                field.SetValue(t, value);
            //            }
            //        }
            //        return t;
            //    }
            //}
        }

        public List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);

            string sql = $"{TSqlHelper<T>.FindAllSql}";

            Func<SqlCommand, List<T>> func = c => {
                SqlDataReader reader = c.ExecuteReader();
                List<T> listT = new List<T>();
                while (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(type);
                    foreach (var field in type.GetProperties())
                    {
                        object value = reader[field.GetColumnName()];
                        if (value is DBNull)
                            value = null;
                        field.SetValue(t, value);
                    }
                    listT.Add(t);
                }
                return listT;
            };
            return this.Execute<List<T>>(sql, func);
            //using (SqlConnection conn = new SqlConnection(StaticConstant.SqlserverConnString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        conn.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        List<T> listT = new List<T>();
            //        while (reader.Read())
            //        {
            //            T t = (T)Activator.CreateInstance(type);
            //            foreach (var field in type.GetProperties())
            //            {
            //                object value = reader[field.GetColumnName()];
            //                if (value is DBNull)
            //                    value = null;
            //                field.SetValue(t, value);
            //            }
            //            listT.Add(t);
            //        }
            //        return listT;
            //    }
            //}
        }

        public T Execute<T>(string sql,Func<SqlCommand,T> func) {

            using (SqlConnection conn = new SqlConnection(StaticConstant.SqlserverConnString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = transaction;
                        T t = func.Invoke(cmd);
                        transaction.Commit();
                        return t;
                    }
                    catch (Exception ex){
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
