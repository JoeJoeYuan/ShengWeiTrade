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
    public class FunctionsService:IFunctionsService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Functions> GetAll()
        {
            string sql="select * from Functions";
            return OleDbHelper.GetList<Functions>(sql);
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
            OleDbParameter spm=new OleDbParameter("@id",id);
            return OleDbHelper.GetSingle<Functions>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="functions">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Functions> GetByPage(int startIndex, int count, string sortColumn, Functions functions)
        {
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "ID DESC";
            string sql = string.Empty;
            string where = GetConditions(functions);
            if (count == 0)
            {
                sql = "select top " + startIndex + " * from Functions where 1=1 " + where + " order by " + sortColumn;
            }
            else
            {
                sql = @"
            select top " + startIndex + " * from Functions where ID not in (select top " + count + " ID from Functions where 1=1 " + where + " order by " + sortColumn + ") " + where + " order by " + sortColumn;
            }
            return OleDbHelper.GetList<Functions>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="functions">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Functions functions)
        {
            string sql="select count(*) from Functions where 1=1"+GetConditions(functions);
            return OleDbHelper.GetCount(sql);
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
                    condition += " and ID = " + functions.ID + "";
                }
                if (functions.ParentID != null)
                {
                    condition += " and ParentID = " + functions.ParentID + "";
                }
                if (!string.IsNullOrEmpty(functions.FName))
                {
                    condition += " and FName like '%" + OleDbHelper.GetParameterValue(functions.FName) + "%'";
                }
                if (!string.IsNullOrEmpty(functions.FPath))
                {
                    condition += " and FPath like '%" + OleDbHelper.GetParameterValue(functions.FPath) + "%'";
                }
                if (functions.IsLock != null)
                {
                    condition += " and IsLock = " + functions.IsLock + "";
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
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
        //        new OleDbParameter("@id",functions.ID),new OleDbParameter("@parentID",functions.ParentID??(object)DBNull.Value),new OleDbParameter("@fName",functions.FName??(object)DBNull.Value),new OleDbParameter("@fPath",functions.FPath??(object)DBNull.Value),new OleDbParameter("@isLock",functions.IsLock??(object)DBNull.Value)
        //    };
        //    return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Functions functions)
        {
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql="update Functions set ";
            if (functions != null)
            {
                if (functions.ParentID != null)
                {
                    sql += "ParentID=@parentID,";
                    spms.Add(new OleDbParameter("@parentID", functions.ParentID));
                }
                if (functions.FName != null)
                {
                    sql += "FName=@fName,";
                    spms.Add(new OleDbParameter("@fName", functions.FName));
                }
                if (functions.FPath != null)
                {
                    sql += "FPath=@fPath,";
                    spms.Add(new OleDbParameter("@fPath", functions.FPath));
                }
                if (functions.IsLock != null)
                {
                    sql += "IsLock=@isLock,";
                    spms.Add(new OleDbParameter("@isLock", functions.IsLock));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new OleDbParameter("@ID", functions.ID));
            
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
            string sql="delete from Functions where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new OleDbParameter("@id"+i,ids[i]));
                LimitService service = new LimitService();
                service.DeleteFID(ids[i]);
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="functions">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Functions functions)
        {
            string sql="insert into Functions(ParentID,FName,FPath,IsLock) values ("+(functions.ParentID==null?"null":"@parentID")+","+(functions.FName==null?"null":"@fName")+","+(functions.FPath==null?"null":"@fPath")+","+(functions.IsLock==null?"null":"@isLock")+")";
            OleDbParameter[] spms=new OleDbParameter[]
            {
                new OleDbParameter("@parentID",functions.ParentID??(object)DBNull.Value),new OleDbParameter("@fName",functions.FName??(object)DBNull.Value),new OleDbParameter("@fPath",functions.FPath??(object)DBNull.Value),new OleDbParameter("@isLock",functions.IsLock??(object)DBNull.Value)
            };
            return OleDbHelper.ExecuteNonQuery(sql,spms);
        }
    }
}
