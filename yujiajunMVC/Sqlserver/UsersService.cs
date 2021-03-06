﻿using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Models;
using IService;

namespace Service.Sqlserver
{
    /// <summary>
    /// 本类由软件生成生成
    /// 禁止修改
    /// 若需要扩展，请另建一个partial class完成。
    /// </summary>
    public class UsersService:IUsersService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Users> GetAll()
        {
            string sql="select * from Users";
            return SqlHelper.GetList<Users>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Users GetById(int id)
        {
            string sql="select * from Users where ID=@id";
            SqlParameter spm=new SqlParameter("@id",id);
            return SqlHelper.GetSingle<Users>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="users">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Users> GetByPage(int startIndex,int count,string sortColumn,Users users)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql="exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","Users"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(users))
            };
            return SqlHelper.GetList<Users>(sql,spms);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="users">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Users users)
        {
            string sql="select count(*) from Users where 1=1"+GetConditions(users);
            return SqlHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="users">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Users users)
        {
            string condition="";
            if (users != null)
            {
                if (users.ID != null)
                {
                    condition += " and ID = '" + users.ID + "'";
                }
                if (!string.IsNullOrEmpty(users.UserName))
                {
                    condition += " and UserName like '%" + SqlHelper.GetParameterValue(users.UserName) + "%'";
                }
                if (!string.IsNullOrEmpty(users.UserPassword))
                {
                    condition += " and UserPassword like '%" + SqlHelper.GetParameterValue(users.UserPassword) + "%'";
                }
                if (users.IsAdmin != null)
                {
                    condition += " and IsAdmin = '" + users.IsAdmin + "'";
                }
                if (users.IsLock != null)
                {
                    condition += " and IsLock = '" + users.IsLock + "'";
                }
            }
            
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="users">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(Users users)
        //{
        //    string sql="update Users set UserName=@userName,UserPassword=@userPassword,IsAdmin=@isAdmin,IsLock=@isLock where ID=@id";
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
        //        new SqlParameter("@id",users.ID),new SqlParameter("@userName",users.UserName??(object)DBNull.Value),new SqlParameter("@userPassword",users.UserPassword??(object)DBNull.Value),new SqlParameter("@isAdmin",users.IsAdmin??(object)DBNull.Value),new SqlParameter("@isLock",users.IsLock??(object)DBNull.Value)
        //    };
        //    return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Users users)
        {
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="update Users set ";
            if(users!=null)
            {
            if(users.UserName!=null)
            {
                sql+="UserName=@userName,";
                spms.Add(new SqlParameter("@userName",users.UserName));
            }
            if(users.UserPassword!=null)
            {
                sql+="UserPassword=@userPassword,";
                spms.Add(new SqlParameter("@userPassword",users.UserPassword));
            }
            if(users.IsAdmin!=null)
            {
                sql+="IsAdmin=@isAdmin,";
                spms.Add(new SqlParameter("@isAdmin",users.IsAdmin));
            }
            if(users.IsLock!=null)
            {
                sql+="IsLock=@isLock,";
                spms.Add(new SqlParameter("@isLock",users.IsLock));
            }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new SqlParameter("@ID", users.ID));
            
            return SqlHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        public int Delete(params int[] ids)
        {
            if(ids.Length==0)return 0;
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="delete from Users where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new SqlParameter("@id"+i,ids[i]));
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return SqlHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="users">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Users users)
        {
            string sql="insert into Users(UserName,UserPassword,IsAdmin,IsLock) values ("+(users.UserName==null?"null":"@userName")+","+(users.UserPassword==null?"null":"@userPassword")+","+(users.IsAdmin==null?"null":"@isAdmin")+","+(users.IsLock==null?"null":"@isLock")+");Select @@IDENTITY";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@userName",users.UserName??(object)DBNull.Value),new SqlParameter("@userPassword",users.UserPassword??(object)DBNull.Value),new SqlParameter("@isAdmin",users.IsAdmin??(object)DBNull.Value),new SqlParameter("@isLock",users.IsLock??(object)DBNull.Value)
            };
            return int.Parse(SqlHelper.ExecuteScalar(sql,spms).ToString());
        }
        /// <summary>
        /// 查询返回实体
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Users GetSingle(Users users)
        {
            List<SqlParameter> spms = new List<SqlParameter>();
            string sql = "select * from Users where 1=1";
            if (users != null)
            {
                if (users.ID != null)
                {
                    sql += " and ID=@ID";
                    spms.Add(new SqlParameter("@ID", users.ID));
                }
                if (users.UserName!=null)
                {
                    sql += " and UserName=@userName";
                    spms.Add(new SqlParameter("@userName", users.UserName));
                }
                if (users.UserPassword!=null)
                {
                    sql += " and UserPassword=@userPassword";
                    spms.Add(new SqlParameter("@userPassword", users.UserPassword));
                }
                if (users.IsAdmin != null)
                {
                    sql += " and IsAdmin=@isAdmin";
                    spms.Add(new SqlParameter("@isAdmin", users.IsAdmin));
                }
                if (users.IsLock != null)
                {
                    sql += " and IsLock=@isLock";
                    spms.Add(new SqlParameter("@isLock", users.IsLock));
                }
            }
            return SqlHelper.GetSingle<Users>(sql, spms.ToArray());
        }
    }
}
