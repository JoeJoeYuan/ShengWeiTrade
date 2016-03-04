using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 功能类
    /// </summary>

    [Serializable]
    public partial class Functions
    {
        public Functions() { }
        public Functions(int id, int? parentID, string fName, string fPath, int? isLock)
        {
            this.ID = id;
            this.ParentID = parentID;
            this.FName = fName;
            this.FPath = fPath;
            this.IsLock = isLock;
        }
        public Functions(IDataReader reader)
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
            if (ModelHelper.HasColumn(reader, "FName"))
            {
                if (reader["FName"] != DBNull.Value)
                {
                    this.FName = (string)reader["FName"];
                }
            }
            if (ModelHelper.HasColumn(reader, "FPath"))
            {
                if (reader["FPath"] != DBNull.Value)
                {
                    this.FPath = (string)reader["FPath"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsLock"))
            {
                if (reader["IsLock"] != DBNull.Value)
                {
                    this.IsLock = (int?)reader["IsLock"];
                }
            }
        }
        public int? ID { get; set; }
        /// <summary>
        /// 父级ID (Note:如功能较少 则默认值0,连接路径不能为空)
        /// </summary>
        public int? ParentID { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 路径 
        /// </summary>
        public string FPath { get; set; }
        /// <summary>
        /// 是否启用 0否 1是
        /// </summary>
        public int? IsLock { get; set; }
    }
}
