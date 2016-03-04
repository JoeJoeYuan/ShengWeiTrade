using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 导航类
    /// </summary>
    [Serializable]
    public partial class Navigation
    {
        public Navigation() { }
        public Navigation(int id, int? parentID, string nName, int? isLock,int? isImg, string nPath,int? sort)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.NName = nName;
            this.IsLock = isLock;
            this.IsImg = IsImg;
            this.NPath = nPath;
            this.Sort = sort;
        }
        public Navigation(IDataReader reader)
        {
            if (ModelHelper.HasColumn(reader, "ID"))
            {
                if (reader["ID"] != DBNull.Value)
                {
                    this.ID = (int)reader["ID"];
                }
            }
            if (ModelHelper.HasColumn(reader, "ParentID"))
            {
                if (reader["ParentID"] != DBNull.Value)
                {
                    this.ParentID = (int?)reader["ParentID"];
                }
            }
            if (ModelHelper.HasColumn(reader, "NName"))
            {
                if (reader["NName"] != DBNull.Value)
                {
                    this.NName = (string)reader["NName"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsLock"))
            {
                if (reader["IsLock"] != DBNull.Value)
                {
                    this.IsLock = (int?)reader["IsLock"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsImg"))
            {
                if (reader["IsImg"] != DBNull.Value)
                {
                    this.IsImg = (int?)reader["IsImg"];
                }
            }
            if (ModelHelper.HasColumn(reader, "Sort"))
            {
                if (reader["Sort"] != DBNull.Value)
                {
                    this.Sort = (int?)reader["Sort"];
                }
            }
            if (ModelHelper.HasColumn(reader, "NPath"))
            {
                if (reader["NPath"] != DBNull.Value)
                {
                    this.NPath = (string)reader["NPath"];
                }
            }
        }
        public int? ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// 导航名称
        /// </summary>
        public string NName { get; set; }
        /// <summary>
        /// 是否在前台显示 0否 1是
        /// </summary>
        public int? IsLock { get; set; }
        /// <summary>
        /// 是否保存图片地址(主要用于区分图片新闻或普通新闻，图片新闻会默认提取内容第一张图片为封面)0:否 1:是
        /// </summary>
        public int? IsImg { get; set; }
        /// <summary>
        /// 排序索引 界面显示的排序
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 连接路径
        /// </summary>
        public string NPath { get; set; }


    }
}
