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
    public class LinksService:ILinksService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Links> GetAll()
        {
            string sql="select * from Links";
            return OleDbHelper.GetList<Links>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Links GetById(int id)
        {
            string sql="select * from Links where ID=@id";
            OleDbParameter spm=new OleDbParameter("@id",id);
            return OleDbHelper.GetSingle<Links>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="links">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Links> GetByPage(int startIndex, int count, string sortColumn, Links links)
        {
            //string sql = "select * from (select top  * from (select top  * from 表 order by id desc) order by id) order by id desc";
            //string sql = "select * from (select top 20 * from (select top 1 * from table order id desc)) order by id desc";

            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "ID DESC";
            string sql = string.Empty;
            string where = GetConditions(links);
           // List<OleDbParameter> listSpms = new List<OleDbParameter>();
            if (count == 0)
            {
                sql = "select top " + startIndex + " * from Links where 1=1 " + where + " order by "+sortColumn;

                //listSpms.Add(new OleDbParameter("@startIndex", startIndex));
                //listSpms.Add(new OleDbParameter("@tableName", "Links"));
                //listSpms.Add(new OleDbParameter("@where", GetConditions(links)));
                //listSpms.Add(new OleDbParameter("@order", sortColumn));
            }
            else
            {
               // sql = "select top " + startIndex + " * from (select top " + (count + startIndex) + "* from Links order by ID desc)";
                sql = @"
            select top " + startIndex + " * from Links where ID not in (select top " + count + " ID from Links where 1=1 " + where + " order by " + sortColumn + ") " + where + " order by " + sortColumn;
                //listSpms.Add(new OleDbParameter("@count", count));
                //listSpms.Add(new OleDbParameter("@pkName", "ID"));
                //listSpms.Add(new OleDbParameter("@startIndex", startIndex));
                //listSpms.Add(new OleDbParameter("@tableName", "Links"));
                //listSpms.Add(new OleDbParameter("@where", GetConditions(links)));
                //listSpms.Add(new OleDbParameter("@order", sortColumn));
            }
            
            return OleDbHelper.GetList<Links>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="links">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Links links)
        {
            string sql="select count(*) from Links where 1=1"+GetConditions(links);
            return OleDbHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="links">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Links links)
        {
            string condition="";
            if (links != null)
            {
                if (links.ID != null)
                {
                    condition += " and ID = " + links.ID + "";
                }
                if (!string.IsNullOrEmpty(links.LName))
                {
                    condition += " and LName like '%" + OleDbHelper.GetParameterValue(links.LName) + "%'";
                }
                if (!string.IsNullOrEmpty(links.LPath))
                {
                    condition += " and LPath like '%" + OleDbHelper.GetParameterValue(links.LPath) + "%'";
                }
            }
            
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="links">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(Links links)
      //  {
       //     string sql="update Links set LName=@lName,LPath=@lPath where ID=@id";
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
         //       new OleDbParameter("@id",links.ID),new OleDbParameter("@lName",links.LName??(object)DBNull.Value),new OleDbParameter("@lPath",links.LPath??(object)DBNull.Value)
         //   };
         //   return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Links links)
        {
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql="update Links set ";
            if (links != null)
            {
                if (links.LName != null)
                {
                    sql += "LName=@lName,";
                    spms.Add(new OleDbParameter("@lName", links.LName));
                }

                if (links.LPath != null)
                {
                    sql += "LPath=@lPath,";
                    spms.Add(new OleDbParameter("@lPath", links.LPath));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@id";
            spms.Add(new OleDbParameter("@id",links.ID));
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
            string sql="delete from Links where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new OleDbParameter("@id"+i,ids[i]));
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="links">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Links links)
        {
            string sql="insert into Links(LName,LPath) values ("+(links.LName==null?"null":"@lName")+","+(links.LPath==null?"null":"@lPath")+")";
            OleDbParameter[] spms=new OleDbParameter[]
            {
                new OleDbParameter("@lName",links.LName??(object)DBNull.Value),new OleDbParameter("@lPath",links.LPath??(object)DBNull.Value)
            };
            return OleDbHelper.ExecuteNonQuery(sql,spms);
        }
    }
}
