using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 新闻类
    /// </summary>
    [Serializable]
    public partial class News
    {
        public News() { }
        public News(int id, int? nID, string title, string author, DateTime? createTime, string source, int? click, int? downLoadNum, int? commentNum, int? isFile, string filePath, string imagePath, string fileDescription, string newsContent, int? isHot, int? isTop)
        {
            this.ID = id;
            this.NID = nID;
            this.Title = title;
            this.Author = author;
            this.CreateTime = createTime;
            this.Source = source;
            this.Click = click;
            this.DownLoadNum = downLoadNum;
            this.CommentNum = commentNum;
            this.IsFile = isFile;
            this.FilePath = filePath;
            this.ImagePath = imagePath;
            this.FileDescription = fileDescription;
            this.NewsContent = newsContent;
            this.IsHot = isHot;
            this.IsTop = isTop;
        }
        public News(IDataReader reader)
        {
            if (ModelHelper.HasColumn(reader, "ID"))
            {
                if (reader["ID"] != DBNull.Value)
                {
                    this.ID = (int)reader["ID"];
                }
            }
            if (ModelHelper.HasColumn(reader, "NID"))
            {
                if (reader["NID"] != DBNull.Value)
                {
                    this.NID = (int?)reader["NID"];
                    this.Navigation = new Navigation(reader);
                }
            }
            if (ModelHelper.HasColumn(reader, "Title"))
            {
                if (reader["Title"] != DBNull.Value)
                {
                    this.Title = (string)reader["Title"];
                }
            }
            if (ModelHelper.HasColumn(reader, "Author"))
            {
                if (reader["Author"] != DBNull.Value)
                {
                    this.Author = (string)reader["Author"];
                }
            }
            if (ModelHelper.HasColumn(reader, "CreateTime"))
            {
                if (reader["CreateTime"] != DBNull.Value)
                {
                    this.CreateTime = (DateTime?)reader["CreateTime"];
                }
            }
            if (ModelHelper.HasColumn(reader, "Source"))
            {
                if (reader["Source"] != DBNull.Value)
                {
                    this.Source = (string)reader["Source"];
                }
            }
            if (ModelHelper.HasColumn(reader, "Click"))
            {
                if (reader["Click"] != DBNull.Value)
                {
                    this.Click = (int?)reader["Click"];
                }
            }
            if (ModelHelper.HasColumn(reader, "DownLoadNum"))
            {
                if (reader["DownLoadNum"] != DBNull.Value)
                {
                    this.DownLoadNum = (int?)reader["DownLoadNum"];
                }
            }
            if (ModelHelper.HasColumn(reader, "CommentNum"))
            {
                if (reader["CommentNum"] != DBNull.Value)
                {
                    this.CommentNum = (int?)reader["CommentNum"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsFile"))
            {
                if (reader["IsFile"] != DBNull.Value)
                {
                    this.IsFile = (int?)reader["IsFile"];
                }
            }
            if (ModelHelper.HasColumn(reader, "FilePath"))
            {
                if (reader["FilePath"] != DBNull.Value)
                {
                    this.FilePath = (string)reader["FilePath"];
                }
            }
            if (ModelHelper.HasColumn(reader, "ImagePath"))
            {
                if (reader["ImagePath"] != DBNull.Value)
                {
                    this.ImagePath = (string)reader["ImagePath"];
                }
            }
            if (ModelHelper.HasColumn(reader, "FileDescription"))
            {
                if (reader["FileDescription"] != DBNull.Value)
                {
                    this.FileDescription = (string)reader["FileDescription"];
                }
            }
            if (ModelHelper.HasColumn(reader, "NewsContent"))
            {
                if (reader["NewsContent"] != DBNull.Value)
                {
                    this.NewsContent = (string)reader["NewsContent"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsHot"))
            {
                if (reader["IsHot"] != DBNull.Value)
                {
                    this.IsHot = (int?)reader["IsHot"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsTop"))
            {
                if (reader["IsTop"] != DBNull.Value)
                {
                    this.IsTop = (int?)reader["IsTop"];
                }
            }
        }

        public int? ID { get; set; }
        /// <summary>
        /// 导航ID
        /// </summary>
        public int? NID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布人
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 文章来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int? Click { get; set; }
        /// <summary>
        /// 下载数
        /// </summary>
        public int? DownLoadNum { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int? CommentNum { get; set; }
        /// <summary>
        /// 附件类型(0:否 1:视频 2:其它 在config中配置)
        /// </summary>
        public int? IsFile { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 图片地址(防止单独显示产品图片)
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 附件描述
        /// </summary>
        public string FileDescription { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? IsHot { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? IsTop { get; set; }
        /// <summary>
        /// 本实体关联的外键对象，仅作查询用。增删改请为其对应的外键属性赋值
        /// </summary>
        public Navigation Navigation{get;set;}

    
    }
}
