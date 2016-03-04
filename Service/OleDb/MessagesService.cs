using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Models;
using IService;
using System.Data.OleDb;

namespace Service.OleDb
{
    /// <summary>
    /// 本类由软件生成生成
    /// 禁止修改
    /// 若需要扩展，请另建一个partial class完成。
    /// </summary>
    public class MessagesService:IMessagesService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Messages> GetAll()
        {
            string sql="select * from Messages";
            return OleDbHelper.GetList<Messages>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Messages GetById(int id)
        {
            string sql="select * from Messages where ID=@id";
            OleDbParameter spm=new OleDbParameter("@id",id);
            return OleDbHelper.GetSingle<Messages>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="messages">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Messages> GetByPage(int startIndex,int count,string sortColumn,Messages messages)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql = string.Empty;
            string where = GetConditions(messages);
            if (count == 0)
            {
                sql = "select top " + startIndex + " * from Messages where 1=1 " + where + " order by " + sortColumn;
            }
            else
            {
                sql = @"
            select top " + startIndex + " * from Messages where ID not in (select top " + count + " ID from Messages where 1=1 " + where + " order by " + sortColumn + ") " + where + " order by " + sortColumn;
            }
            return OleDbHelper.GetList<Messages>(sql);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="messages">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Messages messages)
        {
            string sql="select count(*) from Messages where 1=1"+GetConditions(messages);
            return OleDbHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="messages">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Messages messages)
        {
            string condition="";
            if (messages != null)
            {
                if (messages.ID != null)
                {
                    condition += " and ID = " + messages.ID + "";
                }
                if (messages.IP!=null)
                {
                    condition += " and IP like '%" + OleDbHelper.GetParameterValue(messages.IP) + "%'";
                }
                if (messages.CreateTime != null)
                {
                    condition += " and CreateTime = #" + messages.CreateTime + "#";
                }
                if (messages.IsAudit != null)
                {
                    condition += " and IsAudit = " + messages.IsAudit + "";
                }
                if (messages.CreateName!=null)
                {
                    condition += " and CreateName like '%" + OleDbHelper.GetParameterValue(messages.CreateName) + "%'";
                }
                if (!string.IsNullOrEmpty(messages.MessageContent))
                {
                    condition += " and MessageContent like '%" + OleDbHelper.GetParameterValue(messages.MessageContent) + "%'";
                }
            }
            
            
            
            
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="messages">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(Messages messages)
        //{
        //    string sql="update Messages set IP=@iP,CreateTime=@createTime,IsAudit=@isAudit,CreateName=@createName,MessageContent=@messageContent where ID=@id";
        //    OleDbParameter[] spms=new OleDbParameter[]
        //    {
        //        new OleDbParameter("@id",messages.ID),new OleDbParameter("@iP",messages.IP??(object)DBNull.Value),new OleDbParameter("@createTime",messages.CreateTime??(object)DBNull.Value),new OleDbParameter("@isAudit",messages.IsAudit??(object)DBNull.Value),new OleDbParameter("@createName",messages.CreateName??(object)DBNull.Value),new OleDbParameter("@messageContent",messages.MessageContent??(object)DBNull.Value)
        //    };
        //    return OleDbHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Messages messages)
        {
            List<OleDbParameter> spms=new List<OleDbParameter>();
            string sql="update Messages set ";
            if (messages != null)
            {
                if (messages.IP != null)
                {
                    sql += "IP=@iP,";
                    spms.Add(new OleDbParameter("@iP", messages.IP));
                }
                if (messages.CreateTime != null)
                {
                    sql += "CreateTime=@createTime,";
                    spms.Add(new OleDbParameter("@createTime", messages.CreateTime));
                }
                if (messages.IsAudit != null)
                {
                    sql += "IsAudit=@isAudit,";
                    spms.Add(new OleDbParameter("@isAudit", messages.IsAudit));
                }
                if (messages.CreateName != null)
                {
                    sql += "CreateName=@createName,";
                    spms.Add(new OleDbParameter("@createName",messages.CreateName));
                }
                if (messages.MessageContent != null)
                {
                    sql += "MessageContent=@messageContent,";
                    spms.Add(new OleDbParameter("@messageContent", messages.MessageContent));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new OleDbParameter("@ID", messages.ID));
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
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
            string sql="delete from Messages where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new OleDbParameter("@id"+i,ids[i]));
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return OleDbHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="messages">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Messages messages)
        {
            string sql="insert into Messages(IP,CreateTime,IsAudit,CreateName,MessageContent) values ("+(messages.IP==null?"null":"@iP")+","+(messages.CreateTime==null?"null":"@createTime")+","+(messages.IsAudit==null?"null":"@isAudit")+","+(messages.CreateName==null?"null":"@createName")+","+(messages.MessageContent==null?"null":"@messageContent")+")";
            OleDbParameter[] spms=new OleDbParameter[]
            {
                new OleDbParameter("@iP",OleDbType.VarChar,20),
                new OleDbParameter("@createTime",OleDbType.Date),
                new OleDbParameter("@isAudit",OleDbType.Integer),
                new OleDbParameter("@createName",OleDbType.VarChar),
                new OleDbParameter("@messageContent",OleDbType.VarChar)
            };
            spms[0].Value = messages.IP;
            spms[1].Value = messages.CreateTime.Value;
            spms[2].Value = messages.IsAudit.Value;
            spms[3].Value = messages.CreateName;
            spms[4].Value = messages.MessageContent;
            return OleDbHelper.ExecuteNonQuery(sql,spms);
        }
    }
}
