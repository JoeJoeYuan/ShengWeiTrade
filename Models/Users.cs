using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    [Serializable]
    public partial class Users
    {
        public Users() { }
        public Users(int id, string userName, string userPassword, int? isAdmin, int? isLock)
        {
            this.ID = id;
            this.UserName = userName;
            this.UserPassword = userPassword;
            this.IsAdmin = isAdmin;
            this.IsLock = isLock;
        }
        public Users(IDataReader reader)
        {
            if (ModelHelper.HasColumn(reader, "ID"))
            {
                if (reader["ID"] != DBNull.Value)
                {
                    this.ID = (int)reader["ID"];
                }
            }
            if (ModelHelper.HasColumn(reader, "UserName"))
            {
                if (reader["UserName"] != DBNull.Value)
                {
                    this.UserName = (string)reader["UserName"];
                }
            }
            if (ModelHelper.HasColumn(reader, "UserPassword"))
            {
                if (reader["UserPassword"] != DBNull.Value)
                {
                    this.UserPassword = (string)reader["UserPassword"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsAdmin"))
            {
                if (reader["IsAdmin"] != DBNull.Value)
                {
                    this.IsAdmin = (int?)reader["IsAdmin"];
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
        public int? ID{get;set;}
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName{get;set;}
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword{get;set;}
        /// <summary>
        /// 是否管理员 0否 1是
        /// </summary>
        public int? IsAdmin{get;set;}
        /// <summary>
        /// 是否启用 0否 1是
        /// </summary>
        public int? IsLock{get;set;}

 
    }
}
