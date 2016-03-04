using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Models;
using IService;
using System.Data.OleDb;

namespace Service.OleDb
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
            return OleDbHelper.GetList<Users>(sql);
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
            OleDbParameter spm=new OleDbParameter("@id",id);
            return OleDbHelper.GetSingle<Users>(sql,spm);
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
            string sql = string.Empty;
            string where = GetConditions(users);
            if (count == 0)
            {
                sql = "select top " + startIndex + " * from Users where 1=1 " + where + " order by " + sortColumn;
            }
            else
            {
                sql = @"
            select top " + startIndex + " * from Users where ID not in (select top " + count + " ID from Users where 1=1 " + where + " order by " + sortColumn + ") " + where + " order by " + sortColumn;
            }
            return OleDbHelper.GetList<Users>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="users">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Users users)
        {
            string sql="select count(*) from Users where 1=1"+GetConditions(users);
            return OleDbHelper.GetCount(sql);
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
                    condition += " and ID = " + users.ID + "";
                }
                if (!string.IsNullOrEmpty(users.UserName))
                {
                    condition += " and UserName like '%" + OleDbHelper.GetParameterValue(users.UserName) + "%'";
                }
                if (!string.IsNullOrEmpty(users.UserPassword))
                {
                    condition += " and UserPassword like '%" + OleDbHelper.GetParameterValue(users.UserPassword) + "%'";
                }
                if (users.IsAdmin != null)
                {
                    condition += " and IsAdmin = " + users.IsAdmin + "";
                }
                if (users.IsLock != null)
                {
                    condition += " and IsLock = " + users.IsLock + "";
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
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
        //        new OleDbParameter("@id",users.ID),new OleDbParameter("@userName",users.UserName??(object)DBNull.Value),new OleDbParameter("@userPassword",users.UserPassword??(object)DBNull.Value),new OleDbParameter("@isAdmin",users.IsAdmin??(object)DBNull.Value),new OleDbParameter("@isLock",users.IsLock??(object)DBNull.Value)
        //    };
        //    return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Users users)
        {
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql="update Users set ";
            if (users != null)
            {
                if (users.UserName != null)
                {
                    sql += "UserName=@userName,";
                    spms.Add(new OleDbParameter("@userName", users.UserName));
                }
                if (users.UserPassword != null)
                {
                    sql += "UserPassword=@userPassword,";
                    spms.Add(new OleDbParameter("@userPassword", users.UserPassword));
                }
                if (users.IsAdmin != null)
                {
                    sql += "IsAdmin=@isAdmin,";
                    spms.Add(new OleDbParameter("@isAdmin", users.IsAdmin));
                }
                if (users.IsLock != null)
                {
                    sql += "IsLock=@isLock,";
                    spms.Add(new OleDbParameter("@isLock", users.IsLock));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new OleDbParameter("@ID", users.ID));
            
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        public int Delete(params int[] ids)
        {
            if(ids.Length==0)return 0;
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql="delete from Users where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new OleDbParameter("@id"+i,ids[i]));
                LimitService service = new LimitService(); //删除该用户下相关信息
                service.DeleteUID(ids[i]);
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="users">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Users users)
        {
            string sql="insert into Users(UserName,UserPassword,IsAdmin,IsLock) values ("+(users.UserName==null?"null":"@userName")+","+(users.UserPassword==null?"null":"@userPassword")+","+(users.IsAdmin==null?"null":"@isAdmin")+","+(users.IsLock==null?"null":"@isLock")+")";
            OleDbParameter[] spms=new OleDbParameter[]
            {
                new OleDbParameter("@userName",users.UserName??(object)DBNull.Value),new OleDbParameter("@userPassword",users.UserPassword??(object)DBNull.Value),new OleDbParameter("@isAdmin",users.IsAdmin??(object)DBNull.Value),new OleDbParameter("@isLock",users.IsLock??(object)DBNull.Value)
            };
            return OleDbHelper.ExecuteNonQuery(sql,spms);
        }
        /// <summary>
        /// 查询返回实体
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Users GetSingle(Users users)
        {
            List<OleDbParameter> spms = new List<OleDbParameter>();
            string sql = "select * from Users where 1=1";
            if (users != null)
            {
                if (users.ID != null)
                {
                    sql += " and ID=@ID";
                    spms.Add(new OleDbParameter("@ID", users.ID));
                }
                if (users.UserName!=null)
                {
                    sql += " and UserName=@userName";
                    spms.Add(new OleDbParameter("@userName", users.UserName));
                }
                if (users.UserPassword!=null)
                {
                    sql += " and UserPassword=@userPassword";
                    spms.Add(new OleDbParameter("@userPassword", users.UserPassword));
                }
                if (users.IsAdmin != null)
                {
                    sql += " and IsAdmin=@isAdmin";
                    spms.Add(new OleDbParameter("@isAdmin", users.IsAdmin));
                }
                if (users.IsLock != null)
                {
                    sql += " and IsLock=@isLock";
                    spms.Add(new OleDbParameter("@isLock", users.IsLock));
                }
            }
            return OleDbHelper.GetSingle<Users>(sql, spms.ToArray());
        }
    }
}
