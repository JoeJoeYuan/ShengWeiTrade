using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 权限类
    /// </summary>
    [Serializable]
    public partial class Limit
    {
        public Limit() { }
        public Limit(int id, int? fID, int? uID, string operate)
        {
            this.ID = id;
            this.FID = fID;
            this.UID = uID;
            this.Operate = operate;
        }
        public Limit(IDataReader reader)
        {
            if (ModelHelper.HasColumn(reader, "ID"))
            {
                if (reader["ID"] != DBNull.Value)
                {
                    this.ID = (int)reader["ID"];
                }
            }
            if (ModelHelper.HasColumn(reader, "FID"))
            {
                if (reader["FID"] != DBNull.Value)
                {
                    this.FID = (int?)reader["FID"];
                    this.Functions = new Functions(reader);
                }
            }
            if (ModelHelper.HasColumn(reader, "UID"))
            {
                if (reader["UID"] != DBNull.Value)
                {
                    this.UID = (int?)reader["UID"];
                    this.Users = new Users(reader);
                }
            }
            if (ModelHelper.HasColumn(reader, "Operate"))
            {
                if (reader["Operate"] != DBNull.Value)
                {
                    this.Operate = (string)reader["Operate"];
                }
            }
        }

        public int? ID { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        public int? FID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UID { get; set; }
        /// <summary>
        /// 拥有权限 (VIEW;EDIT;ADD;DEL)
        /// </summary>
        public string Operate { get; set; }
        /// <summary>
        /// 本实体关联的外键对象，仅作查询用。增删改请为其对应的外键属性赋值
        /// </summary>
        public Functions Functions { get; set; }
        /// <summary>
        /// 本实体关联的外键对象，仅作查询用。增删改请为其对应的外键属性赋值
        /// </summary>
        public Users Users { get; set; }

    
    }
}
