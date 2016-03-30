using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 产品类
    /// </summary>
    [Serializable]
    public partial class Products
    {
        public Products() { }
        public Products(int id, int? nID, string title, string description, DateTime? createTime, string filePath, string imagePath, int? category, int? isHot, int? isTop, string author)
        {
            this.ID = id;
            this.NID = nID;
            this.Title = title;
            this.Description = description;
            this.CreateTime = createTime;
            this.FilePath = filePath;
            this.ImagePath = imagePath;
            this.Category = category;
            this.IsHot = isHot;
            this.IsTop = isTop;
            this.Author = author;
        }
        public Products(IDataReader reader)
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
                    //this.Navigation = new Navigation(reader);
                }
            }
            if (ModelHelper.HasColumn(reader, "Title"))
            {
                if (reader["Title"] != DBNull.Value)
                {
                    this.Title = (string)reader["Title"];
                }
            }
            if (ModelHelper.HasColumn(reader, "Description"))
            {
                if (reader["Description"] != DBNull.Value)
                {
                    this.Description = (string)reader["Description"];
                }
            }
            if (ModelHelper.HasColumn(reader, "CreateTime"))
            {
                if (reader["CreateTime"] != DBNull.Value)
                {
                    this.CreateTime = (DateTime?)reader["CreateTime"];
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
            if (ModelHelper.HasColumn(reader, "Author"))
            {
                if (reader["Author"] != DBNull.Value)
                {
                    this.Author = (string)reader["Author"];
                }
            }
        }

        public int? ID { get; set; }
        /// <summary>
        /// 导航ID
        /// </summary>
        public int? NID { get; set; }
        /// <summary>
        /// 产品标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 附件地址
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 图片地址(防止单独显示产品图片)
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 产品类别
        /// </summary>
        public int? Category { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? IsHot { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? IsTop { get; set; }
        ///// <summary>
        ///// 本实体关联的外键对象，仅作查询用。增删改请为其对应的外键属性赋值
        ///// </summary>
        //public Navigation Navigation { get; set; }

        /// <summary>
        /// 产品作者
        /// </summary>
        public string Author { get; set; }
    }
}
