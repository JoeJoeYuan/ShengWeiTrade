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
    public class LinksService:ILinksService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Links> GetAll()
        {
            string sql="select * from Links";
            return SqlHelper.GetList<Links>(sql);
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
            SqlParameter spm=new SqlParameter("@id",id);
            return SqlHelper.GetSingle<Links>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="links">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Links> GetByPage(int startIndex,int count,string sortColumn,Links links)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql="exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","Links"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(links))
            };
            return SqlHelper.GetList<Links>(sql,spms);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="links">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Links links)
        {
            string sql="select count(*) from Links where 1=1"+GetConditions(links);
            return SqlHelper.GetCount(sql);
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
                    condition += " and ID = '" + links.ID + "'";
                }
                if (!string.IsNullOrEmpty(links.LName))
                {
                    condition += " and LName like '%" + SqlHelper.GetParameterValue(links.LName) + "%'";
                }
                if (!string.IsNullOrEmpty(links.LPath))
                {
                    condition += " and LPath like '%" + SqlHelper.GetParameterValue(links.LPath) + "%'";
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
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
         //       new SqlParameter("@id",links.ID),new SqlParameter("@lName",links.LName??(object)DBNull.Value),new SqlParameter("@lPath",links.LPath??(object)DBNull.Value)
         //   };
         //   return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Links links)
        {
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="update Links set ";
            if(links!=null)
            {
            if(links.LName!=null)
            {
                sql+="LName=@lName,";
                spms.Add(new SqlParameter("@lName",links.LName));
            }
                 
            if(links.LPath!=null)
            {
                sql+="LPath=@lPath,";
                spms.Add(new SqlParameter("@lPath",links.LPath));
            }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@id";
            spms.Add(new SqlParameter("@id",links.ID));
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
            string sql="delete from Links where ID in (";
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
        /// <param name="links">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Links links)
        {
            string sql="insert into Links(LName,LPath) values ("+(links.LName==null?"null":"@lName")+","+(links.LPath==null?"null":"@lPath")+");Select @@IDENTITY";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@lName",links.LName??(object)DBNull.Value),new SqlParameter("@lPath",links.LPath??(object)DBNull.Value)
            };
            return int.Parse(SqlHelper.ExecuteScalar(sql,spms).ToString());
        }
    }
}
