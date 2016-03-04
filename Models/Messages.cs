using System;
using System.Data;

namespace Models
{
    /// <summary>
    /// 留言类
    /// </summary>
    [Serializable]
    public partial class Messages
    {
        public Messages() { }
        public Messages(int id, string iP, DateTime? createTime, int? isAudit, string createName, string messageContent)
        {
            this.ID = id;
            this.IP = iP;
            this.CreateTime = createTime;
            this.IsAudit = isAudit;
            this.CreateName = createName;
            this.MessageContent = messageContent;
        }
        public Messages(IDataReader reader)
        {
            if (ModelHelper.HasColumn(reader, "ID"))
            {
                if (reader["ID"] != DBNull.Value)
                {
                    this.ID = (int)reader["ID"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IP"))
            {
                if (reader["IP"] != DBNull.Value)
                {
                    this.IP = (string)reader["IP"];
                }
            }
            if (ModelHelper.HasColumn(reader, "CreateTime"))
            {
                if (reader["CreateTime"] != DBNull.Value)
                {
                    this.CreateTime = (DateTime?)reader["CreateTime"];
                }
            }
            if (ModelHelper.HasColumn(reader, "IsAudit"))
            {
                if (reader["IsAudit"] != DBNull.Value)
                {
                    this.IsAudit = (int?)reader["IsAudit"];
                }
            }
            if (ModelHelper.HasColumn(reader, "CreateName"))
            {
                if (reader["CreateName"] != DBNull.Value)
                {
                    this.CreateName = (string)reader["CreateName"];
                }
            }
            if (ModelHelper.HasColumn(reader, "MessageContent"))
            {
                if (reader["MessageContent"] != DBNull.Value)
                {
                    this.MessageContent = (string)reader["MessageContent"];
                }
            }
        }
        
        public int? ID{get;set;}
        /// <summary>
        /// IP
        /// </summary>
        public string IP{get;set;}
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime? CreateTime{get;set;}
        /// <summary>
        /// 是否审核
        /// </summary>
        public int? IsAudit{get;set;}
        /// <summary>
        /// 留言姓名
        /// </summary>
        public string CreateName{get;set;}
        /// <summary>
        /// 留言内容
        /// </summary>
        public string MessageContent{get;set;}

    
    }
}
