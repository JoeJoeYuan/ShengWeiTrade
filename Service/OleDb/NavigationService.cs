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
    public class NavigationService:INavigationService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Navigation> GetAll()
        {
            string sql="select * from Navigation";
            return OleDbHelper.GetList<Navigation>(sql);
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
            OleDbParameter spm=new OleDbParameter("@id",id);
            return OleDbHelper.GetSingle<Navigation>(sql,spm);
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
            string sql = string.Empty;
            string where = GetConditions(navigation);
            if (count == 0)
            {
                sql = "select top " + startIndex + " * from Navigation where 1=1 " + where + " order by " + sortColumn;
            }
            else
            {
                sql = @"
            select top " + startIndex + " * from Navigation where ID not in (select top " + count + " ID from Navigation where 1=1 " + where + " order by " + sortColumn + ") " + where + " order by " + sortColumn;
            }
            return OleDbHelper.GetList<Navigation>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="navigation">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Navigation navigation)
        {
            string sql="select count(*) from Navigation where 1=1"+GetConditions(navigation);
            return OleDbHelper.GetCount(sql);
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
                    condition += " and ID = " + navigation.ID + "";
                }
                if (navigation.ParentID != null)
                {
                    condition += " and ParentID = " + navigation.ParentID + "";
                }
                if (!string.IsNullOrEmpty(navigation.NName))
                {
                    condition += " and NName like '%" + OleDbHelper.GetParameterValue(navigation.NName) + "%'";
                }
                if (navigation.Sort != null)
                {
                    condition += " and Sort = " + navigation.Sort + "";
                }
                if (navigation.IsLock != null)
                {
                    condition += " and IsLock = " + navigation.IsLock + "";
                }
                if (navigation.IsImg != null)
                {
                    condition += " and IsImg = " + navigation.IsImg + "";
                }
                if (!string.IsNullOrEmpty(navigation.NPath))
                {
                    condition += " and NPath like '%" + OleDbHelper.GetParameterValue(navigation.NPath) + "%'";
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
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
        //        new OleDbParameter("@id",navigation.ID),new OleDbParameter("@parentID",navigation.ParentID??(object)DBNull.Value),new OleDbParameter("@nName",navigation.NName??(object)DBNull.Value),new OleDbParameter("@isLock",navigation.IsLock??(object)DBNull.Value),new OleDbParameter("@nPath",navigation.NPath??(object)DBNull.Value)
        //    };
        //    return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Navigation navigation)
        {
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql="update Navigation set ";
            if (navigation != null)
            {
                if (navigation.ParentID != null)
                {
                    sql += "ParentID=@parentID,";
                    spms.Add(new OleDbParameter("@parentID", navigation.ParentID));
                }
                if (navigation.NName != null)
                {
                    sql += "NName=@nName,";
                    spms.Add(new OleDbParameter("@nName", navigation.NName));
                }
                if (navigation.IsLock != null)
                {
                    sql += "IsLock=@isLock,";
                    spms.Add(new OleDbParameter("@isLock", navigation.IsLock));
                }
                if (navigation.IsImg != null)
                {
                    sql += "IsImg=@isImg,";
                    spms.Add(new OleDbParameter("@isImg", navigation.IsImg));
                }
                if (navigation.Sort != null)
                {
                    sql += "Sort=@sort,";
                    spms.Add(new OleDbParameter("@sort", navigation.Sort));
                }
                if (navigation.NPath != null)
                {
                    sql += "NPath=@nPath,";
                    spms.Add(new OleDbParameter("@nPath", navigation.NPath));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new OleDbParameter("@ID", navigation.ID));
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
            string sql="delete from Navigation where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new OleDbParameter("@id"+i,ids[i]));
                NewsService newsService = new NewsService();
                newsService.DeleteNID(ids[i]);
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="navigation">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Navigation navigation)
        {
            string sql = "insert into Navigation(ParentID,NName,IsLock,IsImg,Sort,NPath) values (" + (navigation.ParentID == null ? "null" : "@parentID") + "," + (navigation.NName == null ? "null" : "@nName") + "," + (navigation.IsLock == null ? "null" : "@isLock") + "," + (navigation.IsImg == null ? "0" : "@isImg") + "," + (navigation.Sort == null ? "null" : "@sort") + "," + (navigation.NPath == null ? "null" : "@nPath") + ")";
            OleDbParameter[] spms=new OleDbParameter[]
            {
                new OleDbParameter("@parentID",navigation.ParentID??(object)DBNull.Value),new OleDbParameter("@nName",navigation.NName??(object)DBNull.Value),new OleDbParameter("@isLock",navigation.IsLock??(object)DBNull.Value),new OleDbParameter("@isImg",navigation.IsImg??(object)DBNull.Value),new OleDbParameter("@sort",navigation.Sort??(object)DBNull.Value),new OleDbParameter("@nPath",navigation.NPath??(object)DBNull.Value)
            };
            return OleDbHelper.ExecuteNonQuery(sql,spms);
        }
    }
}
