using System;
using System.Collections.Generic;
using System.Data.OleDb;
using IService;
using Models;

namespace Service.OleDb
{
    /// <summary>
    /// 本类由软件生成生成
    /// 禁止修改
    /// 若需要扩展，请另建一个partial class完成。
    /// </summary>
    public class ProductsService:IProductsService
    {
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Products> GetAll()
        {
            string sql = "select * from Products";
            return OleDbHelper.GetList<Products>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Products GetById(int id)
        {
            string sql = "select * from Products where ID=?";
            OleDbParameter spm=new OleDbParameter("?id",id);
            return OleDbHelper.GetSingle<Products>(sql, spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="Products">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Products> GetByPage(int startIndex, int count, string sortColumn, Products products)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql = string.Empty;
            string where = GetConditions(products);
            if (count == 0)
            {
                sql = "select top " + startIndex + " * from Products where 1=1 " + where + " order by " + sortColumn;
               // sql = "select top 8 * from News";
            }
            else
            {
                sql = @"
            select top " + startIndex + " * from Products where ID not in (select top " + count + " ID from Products where 1=1 " + where + " order by " + sortColumn + ") " + where + " order by " + sortColumn;
            }
            return OleDbHelper.GetList<Products>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="products">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Products products)
        {
            string sql = "select count(*) from Products where 1=1" + GetConditions(products);
            return OleDbHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="products">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Products products)
        {
            string condition = "";
            if (products != null)
            {
                if (products.ID != null)
                {
                    condition += " and ID = " + products.ID + "";
                }
                if (products.NID != null)
                {
                    condition += " and NID = " + products.NID + "";
                }
                if (!string.IsNullOrEmpty(products.Title))
                {
                    condition += " and Title like '%" + OleDbHelper.GetParameterValue(products.Title) + "%'";
                }
                if (!string.IsNullOrEmpty(products.Description))
                {
                    condition += " and Description like '%" + OleDbHelper.GetParameterValue(products.Description) + "%'";
                }
                if (products.CreateTime != null)
                {
                    condition += " and CreateTime = #" + products.CreateTime + "#";
                }
                if (!string.IsNullOrEmpty(products.FilePath))
                {
                    condition += " and FilePath like '%" + OleDbHelper.GetParameterValue(products.FilePath) + "%'";
                }
                if (products.ImagePath != null)
                {
                    condition += " and ImagePath <>'" + products.ImagePath + "'";
                }
                if (products.Category != null)
                {
                    condition += " and Category !='" + products.Category + "'";
                }
                if (products.IsHot != null)
                {
                    condition += " and IsHot = " + products.IsHot + "";
                }
                if (products.IsTop != null)
                {
                    condition += " and IsTop = " + products.IsTop + "";
                }
                if (!string.IsNullOrEmpty(products.Author))
                {
                    condition += " and Author like '%" + OleDbHelper.GetParameterValue(products.Author) + "%'";
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
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
        //        new OleDbParameter("@id",news.ID),new OleDbParameter("@nID",news.NID??(object)DBNull.Value),new OleDbParameter("@title",news.Title??(object)DBNull.Value),new OleDbParameter("@author",news.Author??(object)DBNull.Value),new OleDbParameter("@createTime",news.CreateTime??(object)DBNull.Value),new OleDbParameter("@source",news.Source??(object)DBNull.Value),new OleDbParameter("@click",news.Click??(object)DBNull.Value),new OleDbParameter("@downLoadNum",news.DownLoadNum??(object)DBNull.Value),new OleDbParameter("@commentNum",news.CommentNum??(object)DBNull.Value),new OleDbParameter("@isFile",news.IsFile??(object)DBNull.Value),new OleDbParameter("@filePath",news.FilePath??(object)DBNull.Value),new OleDbParameter("@imagePath",news.ImagePath??(object)DBNull.Value),new OleDbParameter("@fileDescription",news.FileDescription??(object)DBNull.Value),new OleDbParameter("@newsContent",news.NewsContent??(object)DBNull.Value),new OleDbParameter("@isHot",news.IsHot??(object)DBNull.Value),new OleDbParameter("@isTop",news.IsTop??(object)DBNull.Value)
        //    };
        //    return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        //public int Update(News news)
        //{
        //    List<OleDbParameter> spms=new List<OleDbParameter>();
        //    string sql="update News set ";
        //    if (news != null)
        //    {
        //        if (news.NID != null)
        //        {
        //            sql += "NID=@nID,";
        //            spms.Add(new OleDbParameter("@nID", news.NID));
        //        }
        //        if (news.Title!=null)
        //        {
        //            sql += "Title=@title,";
        //            spms.Add(new OleDbParameter("@title", news.Title));
        //        }
        //        if (news.Author!=null)
        //        {
        //            sql += "Author=@author,";
        //            spms.Add(new OleDbParameter("@author", news.Author));
        //        }
        //        if (news.CreateTime != null)
        //        {
        //            sql += "CreateTime=@createTime,";
        //            spms.Add(new OleDbParameter("@createTime", news.CreateTime.Value));
        //        }
        //        if (news.Source!=null)
        //        {
        //            sql += "Source=@source,";
        //            spms.Add(new OleDbParameter("@source", news.Source));
        //        }
        //        if (news.Click != null)
        //        {
        //            sql += "Click=@click,";
        //            spms.Add(new OleDbParameter("@click", news.Click));
        //        }
        //        if (news.DownLoadNum != null)
        //        {
        //            sql += "DownLoadNum=@downLoadNum,";
        //            spms.Add(new OleDbParameter("@downLoadNum", news.DownLoadNum));
        //        }
        //        if (news.CommentNum != null)
        //        {
        //            sql += "CommentNum=@commentNum,";
        //            spms.Add(new OleDbParameter("@commentNum", news.CommentNum));
        //        }
        //        if (news.IsFile != null)
        //        {
        //            sql += "IsFile=@isFile,";
        //            spms.Add(new OleDbParameter("@isFile", news.IsFile));
        //        }
        //        if (news.FilePath!=null)
        //        {
        //            sql += "FilePath=@filePath,";
        //            spms.Add(new OleDbParameter("@filePath", news.FilePath));
        //        }
        //        if (news.ImagePath!=null)
        //        {
        //            sql += "ImagePath=@imagePath,";
        //            spms.Add(new OleDbParameter("@imagePath", news.ImagePath));
        //        }
        //        if (news.FileDescription!=null)
        //        {
        //            sql += "FileDescription=@fileDescription,";
        //            spms.Add(new OleDbParameter("@fileDescription", news.FileDescription));
        //        }
        //        if (news.NewsContent!=null)
        //        {
        //            sql += "NewsContent=@newsContent,";
        //            spms.Add(new OleDbParameter("@newsContent", news.NewsContent));
        //        }
        //        if (news.IsHot != null)
        //        {
        //            sql += "IsHot=@isHot,";
        //            spms.Add(new OleDbParameter("@isHot", news.IsHot));
        //        }
        //        if (news.IsTop != null)
        //        {
        //            sql += "IsTop=@isTop,";
        //            spms.Add(new OleDbParameter("@isTop", news.IsTop));
        //        }
        //    }
        //    sql=sql.Substring(0,sql.Length-1);
        //    sql+=" where ID =@ID";
        //    spms.Add(new OleDbParameter("@ID", news.ID));
        //    return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        //}
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        public int Delete(params int[] ids)
        {
            if(ids.Length==0)return 0;
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql = "delete from Products where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new OleDbParameter("@id"+i,ids[i]));
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        public int DeleteNID(int NID)
        {
            string sql = @"delete from Products where NID=@NID";
            return OleDbHelper.ExecuteNonQuery(sql, new OleDbParameter("@NID",NID));
        }
        ///// <summary>
        ///// 插入数据，自增列的值对应更新在实体类参数对象中
        ///// </summary>
        ///// <param name="news">需要插入的数据实体</param>
        ///// <returns>影响行数</returns>
        //public int Insert(News news)
        //{
          
        //    string sql="insert into News(NID,Title,Author,CreateTime,Source,Click,DownLoadNum,CommentNum,IsFile,FilePath,ImagePath,FileDescription,NewsContent,IsHot,IsTop) values (@nID,@title,@author,@createTime,@source,@click,@downLoadNum,@commentNum,@isFile,@filePath,@imagePath,@fileDescription,@newsContent,@isHot,@isTop)";
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
        //        new OleDbParameter("@nID",OleDbType.Integer),
        //        new OleDbParameter("@title",OleDbType.VarChar),
        //        new OleDbParameter("@author",OleDbType.VarChar),
        //        new OleDbParameter("@createTime",OleDbType.Date),
        //        new OleDbParameter("@source",OleDbType.VarChar,50),
        //        new OleDbParameter("@click",OleDbType.Integer),
        //        new OleDbParameter("@downLoadNum",OleDbType.Integer),
        //        new OleDbParameter("@commentNum",OleDbType.Integer),
        //        new OleDbParameter("@isFile",OleDbType.Integer),
        //        new OleDbParameter("@filePath",OleDbType.VarChar),
        //        new OleDbParameter("@imagePath",OleDbType.VarChar),
        //        new OleDbParameter("@fileDescription",OleDbType.VarChar),
        //        new OleDbParameter("@newsContent",OleDbType.LongVarChar),
        //        new OleDbParameter("@isHot",OleDbType.Integer),
        //        new OleDbParameter("@isTop",OleDbType.Integer)
        //    };
        //    spms[0].Value = news.NID ?? (object)DBNull.Value;
        //    spms[1].Value = news.Title ?? (object)DBNull.Value;
        //    spms[2].Value = news.Author ?? (object)DBNull.Value;
        //    spms[3].Value = news.CreateTime.Value;
        //    spms[4].Value = news.Source ?? (object)DBNull.Value;
        //    spms[5].Value = news.Click ?? (object)DBNull.Value;
        //    spms[6].Value = news.DownLoadNum ?? (object)DBNull.Value;
        //    spms[7].Value = news.CommentNum ?? (object)DBNull.Value;
        //    spms[8].Value = news.IsFile ?? (object)DBNull.Value;
        //    spms[9].Value = news.FilePath ?? "0";
        //    spms[10].Value = news.ImagePath ?? "0";
        //    spms[11].Value = news.FileDescription ?? "";
        //    spms[12].Value = news.NewsContent;
        //    spms[13].Value = news.IsHot ?? (object)DBNull.Value;
        //    spms[14].Value = news.IsTop ?? (object)DBNull.Value;
        //    return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
        ///// <summary>
        ///// 修改点击数
        ///// </summary>
        ///// <param name="ID"></param>
        //public void UpdateClick(int ID)
        //{
        //    string sql = "update News set Click=Click+1 where ID=@ID";
        //    OleDbHelper.ExecuteNonQuery(sql,new OleDbParameter("@ID", ID));
        //}
        ///// <summary>
        ///// 修改下载数
        ///// </summary>
        ///// <param name="ID"></param>
        //public void UpdateDownLoad(int ID)
        //{
        //    string sql = "update News set DownLoadNum=DownLoadNum+1 where ID=@ID";
        //    OleDbHelper.ExecuteNonQuery(sql, new OleDbParameter("@ID", ID));
        //}
        /// <summary>
        /// 前台新闻查询
        /// </summary>
        /// <returns></returns>
        public List<Products> GetByPage()
        {
            //string sql = @"exec GetFrontNews";
            string sql = @"select * from (select top 10 ID,NID,Title,ImagePath,CreateTime from Products where ImagePath<>'0' ORDER BY ID DESC) AS TABLE1
                           union all
                           select * from (select top 10 ID,NID,Title,ImagePath,CreateTime from Products where NID=5 ORDER BY ID DESC) AS TABLE2
                           union all
                           select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from Products where NID= 4  ORDER BY ID DESC) AS TABLE3
                           union all
                           select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from Products where NID=3 ORDER BY ID DESC) AS TABLE4
                           union all
                           select * from (select top 8 ID,NID,Title,ImagePath,CreateTime from Products where NID=2 ORDER BY ID DESC) AS TABLE5
                        ";
            return OleDbHelper.GetList<Products>(sql);
        }
        /// <summary>
        /// 热点产品
        /// </summary>
        /// <returns></returns>
        public List<Products> GetHot()
        {
            //string sql = "select ID,Title,CreateTime from Products ORDER BY CreateTime DESC WHERE IsHot = 1 and ImagePath != 0";
            string sql = "select top 10 * from Products WHERE IsHot=1 and ImagePath <> null ORDER BY CreateTime DESC";
            return OleDbHelper.GetList<Products>(sql);
        }
    }
}
