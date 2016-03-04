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
    public class NavigationService:INavigationService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Navigation> GetAll()
        {
            string sql="select * from Navigation";
            return SqlHelper.GetList<Navigation>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Navigation GetById(int id)
        {
            string sql="select * from Navigation where ID=@id";
            SqlParameter spm=new SqlParameter("@id",id);
            return SqlHelper.GetSingle<Navigation>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="navigation">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Navigation> GetByPage(int startIndex,int count,string sortColumn,Navigation navigation)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql="exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","Navigation"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(navigation))
            };
            return SqlHelper.GetList<Navigation>(sql,spms);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="navigation">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Navigation navigation)
        {
            string sql="select count(*) from Navigation where 1=1"+GetConditions(navigation);
            return SqlHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="navigation">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Navigation navigation)
        {
            string condition="";
            if (navigation != null)
            {
                if (navigation.ID != null)
                {
                    condition += " and ID = '" + navigation.ID + "'";
                }
                if (navigation.ParentID != null)
                {
                    condition += " and ParentID = '" + navigation.ParentID + "'";
                }
                if (!string.IsNullOrEmpty(navigation.NName))
                {
                    condition += " and NName like '%" + SqlHelper.GetParameterValue(navigation.NName) + "%'";
                }
                if (navigation.Sort != null)
                {
                    condition += " and Sort = '" + navigation.Sort + "'";
                }
                if (navigation.IsLock != null)
                {
                    condition += " and IsLock = '" + navigation.IsLock + "'";
                }
                if (navigation.IsImg != null)
                {
                    condition += " and IsImg = '" + navigation.IsImg + "'";
                }
                if (!string.IsNullOrEmpty(navigation.NPath))
                {
                    condition += " and NPath like '%" + SqlHelper.GetParameterValue(navigation.NPath) + "%'";
                }
            }
            
            
            
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="navigation">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(Navigation navigation)
        //{
        //    string sql="update Navigation set ParentID=@parentID,NName=@nName,IsLock=@isLock,NPath=@nPath where ID=@id";
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
        //        new SqlParameter("@id",navigation.ID),new SqlParameter("@parentID",navigation.ParentID??(object)DBNull.Value),new SqlParameter("@nName",navigation.NName??(object)DBNull.Value),new SqlParameter("@isLock",navigation.IsLock??(object)DBNull.Value),new SqlParameter("@nPath",navigation.NPath??(object)DBNull.Value)
        //    };
        //    return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Navigation navigation)
        {
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="update Navigation set ";
            if(navigation!=null)
            {
            if(navigation.ParentID!=null)
            {
                sql+="ParentID=@parentID,";
                spms.Add(new SqlParameter("@parentID",navigation.ParentID));
            }
            if(navigation.NName!=null)
            {
                sql+="NName=@nName,";
                spms.Add(new SqlParameter("@nName",navigation.NName));
            }
            if(navigation.IsLock!=null)
            {
                sql+="IsLock=@isLock,";
                spms.Add(new SqlParameter("@isLock",navigation.IsLock));
            }
            if (navigation.IsImg != null)
            {
                sql += "IsImg=@isImg,";
                spms.Add(new SqlParameter("@isImg", navigation.IsImg));
            }
            if (navigation.Sort != null)
            {
                sql += "Sort=@sort,";
                spms.Add(new SqlParameter("@sort", navigation.Sort));
            }
            if(navigation.NPath!=null)
            {
                sql+="NPath=@nPath,";
                spms.Add(new SqlParameter("@nPath",navigation.NPath));
            }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new SqlParameter("@ID", navigation.ID));
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
            string sql="delete from Navigation where ID in (";
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
        /// <param name="navigation">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Navigation navigation)
        {
            string sql = "insert into Navigation(ParentID,NName,IsLock,IsImg,Sort,NPath) values (" + (navigation.ParentID == null ? "null" : "@parentID") + "," + (navigation.NName == null ? "null" : "@nName") + "," + (navigation.IsLock == null ? "null" : "@isLock") + "," + (navigation.IsImg == null ? "0" : "@isImg") + "," + (navigation.Sort == null ? "null" : "@sort") + "," + (navigation.NPath == null ? "null" : "@nPath") + ");Select @@IDENTITY";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@parentID",navigation.ParentID??(object)DBNull.Value),new SqlParameter("@nName",navigation.NName??(object)DBNull.Value),new SqlParameter("@isLock",navigation.IsLock??(object)DBNull.Value),new SqlParameter("@isImg",navigation.IsImg??(object)DBNull.Value),new SqlParameter("@sort",navigation.Sort??(object)DBNull.Value),new SqlParameter("@nPath",navigation.NPath??(object)DBNull.Value)
            };
            return int.Parse(SqlHelper.ExecuteScalar(sql,spms).ToString());
        }
    }
}
