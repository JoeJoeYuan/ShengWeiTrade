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
        public int Update(Products products)
        {
            List<OleDbParameter> spms = new List<OleDbParameter>();
            string sql = "update Products set ";
            if (products != null)
            {
                if (products.NID != null)
                {
                    sql += "NID=@nID,";
                    spms.Add(new OleDbParameter("@nID", products.NID));
                }
                if (products.Title != null)
                {
                    sql += "Title=@title,";
                    spms.Add(new OleDbParameter("@title", products.Title));
                }
                if (products.Description != null)
                {
                    sql += "Description=@description,";
                    spms.Add(new OleDbParameter("@description", products.Description));
                }
                if (products.CreateTime != null)
                {
                    sql += "CreateTime=@createTime,";
                    spms.Add(new OleDbParameter("@createTime", products.CreateTime.Value));
                }
                if (products.ImagePath != null)
                {
                    sql += "ImagePath=@imagePath,";
                    spms.Add(new OleDbParameter("@imagePath", products.ImagePath));
                }
                if (products.Category != null)
                {
                    sql += "Category=@category,";
                    spms.Add(new OleDbParameter("@category", products.Category));
                }
                if (products.IsHot != null)
                {
                    sql += "IsHot=@isHot,";
                    spms.Add(new OleDbParameter("@isHot", products.IsHot));
                }
                if (products.IsTop != null)
                {
                    sql += "IsTop=@isTop,";
                    spms.Add(new OleDbParameter("@isTop", products.IsTop));
                }
                if (products.Author != null)
                {
                    sql += "Author=@author,";
                    spms.Add(new OleDbParameter("@author", products.Author));
                }
            }
            sql = sql.Substring(0, sql.Length - 1);
            sql += " where ID =@ID";
            spms.Add(new OleDbParameter("@ID", products.ID));
            return OleDbHelper.ExecuteNonQuery(sql, spms.ToArray());
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
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="news">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Products products)
        {

            string sql = "insert into Products(NID,Title,Description,CreateTime,FilePath,ImagePath,Category,IsTop,IsHot,Author) values (@nID,@title,@description,@createTime,@filePath,@imagePath,@category,@isHot,@isTop,@author)";
            OleDbParameter[] spms = new OleDbParameter[]
            {
                new OleDbParameter("@nID",OleDbType.Integer),
                new OleDbParameter("@title",OleDbType.VarChar),
                new OleDbParameter("@description",OleDbType.LongVarChar),
                new OleDbParameter("@createTime",OleDbType.Date),
                new OleDbParameter("@filePath",OleDbType.VarChar),
                new OleDbParameter("@imagePath",OleDbType.VarChar),
                new OleDbParameter("@category",OleDbType.Integer),
                new OleDbParameter("@isHot",OleDbType.Integer),
                new OleDbParameter("@isTop",OleDbType.Integer),
                new OleDbParameter("@author",OleDbType.VarChar)
            };
            spms[0].Value = products.NID ?? (object)DBNull.Value;
            spms[1].Value = products.Title ?? (object)DBNull.Value;
            spms[2].Value = products.Description;
            spms[3].Value = products.CreateTime.Value;
            spms[4].Value = products.FilePath ?? (object)DBNull.Value;
            spms[5].Value = products.ImagePath ?? (object)DBNull.Value;
            spms[6].Value = products.Category ?? (object)DBNull.Value;
            spms[7].Value = products.IsHot ?? (object)DBNull.Value;
            spms[8].Value = products.IsTop ?? (object)DBNull.Value;
            spms[9].Value = products.Author ?? (object)DBNull.Value;

            return OleDbHelper.ExecuteNonQuery(sql, spms);
        }
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
