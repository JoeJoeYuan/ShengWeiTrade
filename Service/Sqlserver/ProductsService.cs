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
    public class ProductsService : IProductsService
    {

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Products> GetAll()
        {
            string sql = "select * from Products";
            return SqlHelper.GetList<Products>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Products GetById(int id)
        {
            string sql = "select * from Products where ID=@id";
            SqlParameter spm = new SqlParameter("@id", id);
            return SqlHelper.GetSingle<Products>(sql, spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="news">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Products> GetByPage(int startIndex, int count, string sortColumn, Products products)
        {
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "ID DESC";
            string sql = "exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms = new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","News"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(products))
            };
            return SqlHelper.GetList<Products>(sql, spms);
        }
        /// <summary>
        /// 前台新闻查询
        /// </summary>
        /// <returns></returns>
        public List<Products> GetByPage()
        {
            string sql = @"exec GetFrontNews";
            return SqlHelper.GetList<Products>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="news">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Products products)
        {
            string sql = "select count(*) from News where 1=1" + GetConditions(products);
            return SqlHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="news">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Products products)
        {
            string condition = "";
            if (products != null)
            {
                if (products.ID != null)
                {
                    condition += " and ID = '" + products.ID + "'";
                }
                if (products.NID != null)
                {
                    condition += " and NID = '" + products.NID + "'";
                }
                if (!string.IsNullOrEmpty(products.Title))
                {
                    condition += " and Title like '%" + SqlHelper.GetParameterValue(products.Title) + "%'";
                }
                if (!string.IsNullOrEmpty(products.Description))
                {
                    condition += " and Description like '%" + SqlHelper.GetParameterValue(products.Description) + "%'";
                }
                if (products.CreateTime != null)
                {
                    condition += " and CreateTime = '" + products.CreateTime + "'";
                }
                if (!string.IsNullOrEmpty(products.FilePath))
                {
                    condition += " and FilePath like '%" + SqlHelper.GetParameterValue(products.FilePath) + "%'";
                }
                if (products.ImagePath != null)
                {
                    condition += " and ImagePath !='" + products.ImagePath + "'";
                }
                if (products.Category != null)
                {
                    condition += " and Category !='" + products.Category + "'";
                }
                if (products.IsHot != null)
                {
                    condition += " and IsHot = '" + products.IsHot + "'";
                }
                if (products.IsTop != null)
                {
                    condition += " and IsTop = '" + products.IsTop + "'";
                }
                if (!string.IsNullOrEmpty(products.Author))
                {
                    condition += " and Author like '%" + SqlHelper.GetParameterValue(products.Author) + "%'";
                }
            }
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="news">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(News news)
        //{
        //    string sql="update News set NID=@nID,Title=@title,Author=@author,CreateTime=@createTime,Source=@source,Click=@click,DownLoadNum=@downLoadNum,CommentNum=@commentNum,IsFile=@isFile,FilePath=@filePath,ImagePath=@imagePath,FileDescription=@fileDescription,NewsContent=@newsContent,IsHot=@isHot,IsTop=@isTop where ID=@id";
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
        //        new SqlParameter("@id",news.ID),new SqlParameter("@nID",news.NID??(object)DBNull.Value),new SqlParameter("@title",news.Title??(object)DBNull.Value),new SqlParameter("@author",news.Author??(object)DBNull.Value),new SqlParameter("@createTime",news.CreateTime??(object)DBNull.Value),new SqlParameter("@source",news.Source??(object)DBNull.Value),new SqlParameter("@click",news.Click??(object)DBNull.Value),new SqlParameter("@downLoadNum",news.DownLoadNum??(object)DBNull.Value),new SqlParameter("@commentNum",news.CommentNum??(object)DBNull.Value),new SqlParameter("@isFile",news.IsFile??(object)DBNull.Value),new SqlParameter("@filePath",news.FilePath??(object)DBNull.Value),new SqlParameter("@imagePath",news.ImagePath??(object)DBNull.Value),new SqlParameter("@fileDescription",news.FileDescription??(object)DBNull.Value),new SqlParameter("@newsContent",news.NewsContent??(object)DBNull.Value),new SqlParameter("@isHot",news.IsHot??(object)DBNull.Value),new SqlParameter("@isTop",news.IsTop??(object)DBNull.Value)
        //    };
        //    return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
        /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Products products)
        {
            List<SqlParameter> spms = new List<SqlParameter>();
            //string sql = "update News set ";
            //if (products != null)
            //{
            //    if (products.NID != null)
            //    {
            //        sql += "NID=@nID,";
            //        spms.Add(new SqlParameter("@nID", news.NID));
            //    }
            //    if (news.Title != null)
            //    {
            //        sql += "Title=@title,";
            //        spms.Add(new SqlParameter("@title", news.Title));
            //    }
            //    if (news.Author != null)
            //    {
            //        sql += "Author=@author,";
            //        spms.Add(new SqlParameter("@author", news.Author));
            //    }
            //    if (news.CreateTime != null)
            //    {
            //        sql += "CreateTime=@createTime,";
            //        spms.Add(new SqlParameter("@createTime", news.CreateTime));
            //    }
            //    if (news.Source != null)
            //    {
            //        sql += "Source=@source,";
            //        spms.Add(new SqlParameter("@source", news.Source));
            //    }
            //    if (news.Click != null)
            //    {
            //        sql += "Click=@click,";
            //        spms.Add(new SqlParameter("@click", news.Click));
            //    }
            //    if (news.DownLoadNum != null)
            //    {
            //        sql += "DownLoadNum=@downLoadNum,";
            //        spms.Add(new SqlParameter("@downLoadNum", news.DownLoadNum));
            //    }
            //    if (news.CommentNum != null)
            //    {
            //        sql += "CommentNum=@commentNum,";
            //        spms.Add(new SqlParameter("@commentNum", news.CommentNum));
            //    }
            //    if (news.IsFile != null)
            //    {
            //        sql += "IsFile=@isFile,";
            //        spms.Add(new SqlParameter("@isFile", news.IsFile));
            //    }
            //    if (news.FilePath != null)
            //    {
            //        sql += "FilePath=@filePath,";
            //        spms.Add(new SqlParameter("@filePath", news.FilePath));
            //    }
            //    if (news.ImagePath != null)
            //    {
            //        sql += "ImagePath=@imagePath,";
            //        spms.Add(new SqlParameter("@imagePath", news.ImagePath));
            //    }
            //    if (news.FileDescription != null)
            //    {
            //        sql += "FileDescription=@fileDescription,";
            //        spms.Add(new SqlParameter("@fileDescription", news.FileDescription));
            //    }
            //    if (news.NewsContent != null)
            //    {
            //        sql += "NewsContent=@newsContent,";
            //        spms.Add(new SqlParameter("@newsContent", news.NewsContent));
            //    }
            //    if (news.IsHot != null)
            //    {
            //        sql += "IsHot=@isHot,";
            //        spms.Add(new SqlParameter("@isHot", news.IsHot));
            //    }
            //    if (news.IsTop != null)
            //    {
            //        sql += "IsTop=@isTop,";
            //        spms.Add(new SqlParameter("@isTop", news.IsTop));
            //    }
            //}
            //sql = sql.Substring(0, sql.Length - 1);
            //sql += " where ID =@ID";
            //spms.Add(new SqlParameter("@ID", news.ID));

            ///
            /// Need TO DO.
            ///
            string sql = "";
            return SqlHelper.ExecuteNonQuery(sql, spms.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        public int Delete(params int[] ids)
        {
            if (ids.Length == 0) return 0;
            List<SqlParameter> spms = new List<SqlParameter>();
            string sql = "delete from Products where ID in (";
            for (int i = 0; i < ids.Length; i++)
            {
                sql += "@id" + i + ",";
                spms.Add(new SqlParameter("@id" + i, ids[i]));
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            return SqlHelper.ExecuteNonQuery(sql, spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="news">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Products products)
        {
            //string sql = "insert into Products(NID,Title,Author,CreateTime,Source,Click,DownLoadNum,CommentNum,IsFile,FilePath,ImagePath,FileDescription,NewsContent,IsHot,IsTop) values (" + (products.NID == null ? "null" : "@nID") + "," + (products.Title == null ? "null" : "@title") + "," + (products.Author == null ? "null" : "@author") + "," + (products.CreateTime == null ? "null" : "@createTime") + "," + (products.Source == null ? "null" : "@source") + "," + (news.Click == null ? "null" : "@click") + "," + (news.DownLoadNum == null ? "null" : "@downLoadNum") + "," + (news.CommentNum == null ? "null" : "@commentNum") + "," + (news.IsFile == null ? "null" : "@isFile") + "," + (news.FilePath == null ? "null" : "@filePath") + "," + (news.ImagePath == null ? "null" : "@imagePath") + "," + (news.FileDescription == null ? "null" : "@fileDescription") + "," + (news.NewsContent == null ? "null" : "@newsContent") + "," + (news.IsHot == null ? "null" : "@isHot") + "," + (news.IsTop == null ? "null" : "@isTop") + ");Select @@IDENTITY";
            //SqlParameter[] spms = new SqlParameter[]
            //{
            //    new SqlParameter("@nID",products.NID??(object)DBNull.Value),new SqlParameter("@title",products.Title??(object)DBNull.Value),new SqlParameter("@author",products.Author??(object)DBNull.Value),new SqlParameter("@createTime",products.CreateTime??(object)DBNull.Value),new SqlParameter("@source",products.Source??(object)DBNull.Value),new SqlParameter("@click",products.Click??(object)DBNull.Value),new SqlParameter("@downLoadNum",news.DownLoadNum??(object)DBNull.Value),new SqlParameter("@commentNum",news.CommentNum??(object)DBNull.Value),new SqlParameter("@isFile",news.IsFile??(object)DBNull.Value),new SqlParameter("@filePath",news.FilePath??(object)DBNull.Value),new SqlParameter("@imagePath",news.ImagePath??(object)DBNull.Value),new SqlParameter("@fileDescription",news.FileDescription??(object)DBNull.Value),new SqlParameter("@newsContent",news.NewsContent??(object)DBNull.Value),new SqlParameter("@isHot",news.IsHot??(object)DBNull.Value),new SqlParameter("@isTop",news.IsTop??(object)DBNull.Value)
            //};
            ///
            /// Need TO DO.
            ///
            string sql = "";
            SqlParameter[] spms = new SqlParameter[] { };
            return int.Parse(SqlHelper.ExecuteScalar(sql, spms).ToString());
        }
        ///// <summary>
        ///// 修改点击数
        ///// </summary>
        ///// <param name="ID"></param>
        //public void UpdateClick(int ID)
        //{
        //    string sql = "exec SetClick @ID";
        //    SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@ID", ID));
        //}
        ///// <summary>
        ///// 修改下载数
        ///// </summary>
        ///// <param name="ID"></param>
        //public void UpdateDownLoad(int ID)
        //{
        //    string sql = "exec SetDownLoadNum @ID";
        //    SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@ID", ID));
        //}
        /// <summary>
        /// 热点产品
        /// </summary>
        /// <returns></returns>
        public List<Products> GetHot()
        {
            string sql = "select top 8  ID,Title,CreateTime from Products order by CreateTime DESC WHERE IsHot = 1 and ImagePath != 0";
            return SqlHelper.GetList<Products>(sql);
        }
    }
}
