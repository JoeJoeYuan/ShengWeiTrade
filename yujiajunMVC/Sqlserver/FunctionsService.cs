using System;
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
    public class FunctionsService:IFunctionsService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Functions> GetAll()
        {
            string sql="select * from Functions";
            return SqlHelper.GetList<Functions>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Functions GetById(int id)
        {
            string sql="select * from Functions where ID=@id";
            SqlParameter spm=new SqlParameter("@id",id);
            return SqlHelper.GetSingle<Functions>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="functions">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Functions> GetByPage(int startIndex,int count,string sortColumn,Functions functions)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql="exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","Functions"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(functions))
            };
            return SqlHelper.GetList<Functions>(sql,spms);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="functions">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Functions functions)
        {
            string sql="select count(*) from Functions where 1=1"+GetConditions(functions);
            return SqlHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="functions">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Functions functions)
        {
            string condition="";
            if (functions != null)
            {
                if (functions.ID != null)
                {
                    condition += " and ID = '" + functions.ID + "'";
                }
                if (functions.ParentID != null)
                {
                    condition += " and ParentID = '" + functions.ParentID + "'";
                }
                if (!string.IsNullOrEmpty(functions.FName))
                {
                    condition += " and FName like '%" + SqlHelper.GetParameterValue(functions.FName) + "%'";
                }
                if (!string.IsNullOrEmpty(functions.FPath))
                {
                    condition += " and FPath like '%" + SqlHelper.GetParameterValue(functions.FPath) + "%'";
                }
                if (functions.IsLock != null)
                {
                    condition += " and IsLock = '" + functions.IsLock + "'";
                }
            }
            
            
            
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="functions">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(Functions functions)
        //{
        //    string sql="update Functions set ParentID=@parentID,FName=@fName,FPath=@fPath,IsLock=@isLock where ID=@id";
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
        //        new SqlParameter("@id",functions.ID),new SqlParameter("@parentID",functions.ParentID??(object)DBNull.Value),new SqlParameter("@fName",functions.FName??(object)DBNull.Value),new SqlParameter("@fPath",functions.FPath??(object)DBNull.Value),new SqlParameter("@isLock",functions.IsLock??(object)DBNull.Value)
        //    };
        //    return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Functions functions)
        {
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="update Functions set ";
            if (functions != null)
            {
                if (functions.ParentID != null)
                {
                    sql += "ParentID=@parentID,";
                    spms.Add(new SqlParameter("@parentID", functions.ParentID));
                }
                if (functions.FName != null)
                {
                    sql += "FName=@fName,";
                    spms.Add(new SqlParameter("@fName", functions.FName));
                }
                if (functions.FPath != null)
                {
                    sql += "FPath=@fPath,";
                    spms.Add(new SqlParameter("@fPath", functions.FPath));
                }
                if (functions.IsLock != null)
                {
                    sql += "IsLock=@isLock,";
                    spms.Add(new SqlParameter("@isLock", functions.IsLock));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new SqlParameter("@ID", functions.ID));
            
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
            string sql="delete from Functions where ID in (";
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
        /// <param name="functions">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Functions functions)
        {
            string sql="insert into Functions(ParentID,FName,FPath,IsLock) values ("+(functions.ParentID==null?"null":"@parentID")+","+(functions.FName==null?"null":"@fName")+","+(functions.FPath==null?"null":"@fPath")+","+(functions.IsLock==null?"null":"@isLock")+");Select @@IDENTITY";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@parentID",functions.ParentID??(object)DBNull.Value),new SqlParameter("@fName",functions.FName??(object)DBNull.Value),new SqlParameter("@fPath",functions.FPath??(object)DBNull.Value),new SqlParameter("@isLock",functions.IsLock??(object)DBNull.Value)
            };
            return int.Parse(SqlHelper.ExecuteScalar(sql,spms).ToString());
        }
    }
}
