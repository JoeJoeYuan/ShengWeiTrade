using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Models;
using IService;

namespace Service.Sqlserver
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
            return SqlHelper.GetList<Messages>(sql);
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
            SqlParameter spm=new SqlParameter("@id",id);
            return SqlHelper.GetSingle<Messages>(sql,spm);
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
            string sql="exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","Messages"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(messages))
            };
            return SqlHelper.GetList<Messages>(sql,spms);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="messages">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Messages messages)
        {
            string sql="select count(*) from Messages where 1=1"+GetConditions(messages);
            return SqlHelper.GetCount(sql);
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
                    condition += " and ID = '" + messages.ID + "'";
                }
                if (!string.IsNullOrEmpty(messages.IP))
                {
                    condition += " and IP like '%" + SqlHelper.GetParameterValue(messages.IP) + "%'";
                }
                if (messages.CreateTime != null)
                {
                    condition += " and CreateTime = '" + messages.CreateTime + "'";
                }
                if (messages.IsAudit != null)
                {
                    condition += " and IsAudit = '" + messages.IsAudit + "'";
                }
                if (!string.IsNullOrEmpty(messages.CreateName))
                {
                    condition += " and CreateName like '%" + SqlHelper.GetParameterValue(messages.CreateName) + "%'";
                }
                if (!string.IsNullOrEmpty(messages.MessageContent))
                {
                    condition += " and MessageContent like '%" + SqlHelper.GetParameterValue(messages.MessageContent) + "%'";
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
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
        //        new SqlParameter("@id",messages.ID),new SqlParameter("@iP",messages.IP??(object)DBNull.Value),new SqlParameter("@createTime",messages.CreateTime??(object)DBNull.Value),new SqlParameter("@isAudit",messages.IsAudit??(object)DBNull.Value),new SqlParameter("@createName",messages.CreateName??(object)DBNull.Value),new SqlParameter("@messageContent",messages.MessageContent??(object)DBNull.Value)
        //    };
        //    return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Messages messages)
        {
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="update Messages set ";
            if(messages!=null)
            {
            if(messages.IP!=null)
            {
                sql+="IP=@iP,";
                spms.Add(new SqlParameter("@iP",messages.IP));
            }
            if(messages.CreateTime!=null)
            {
                sql+="CreateTime=@createTime,";
                spms.Add(new SqlParameter("@createTime",messages.CreateTime));
            }
            if(messages.IsAudit!=null)
            {
                sql+="IsAudit=@isAudit,";
                spms.Add(new SqlParameter("@isAudit",messages.IsAudit));
            }
            if(messages.CreateName!=null)
            {
                sql+="CreateName=@createName,";
                spms.Add(new SqlParameter("@createName",messages.CreateName));
            }
            if(messages.MessageContent!=null)
            {
                sql+="MessageContent=@messageContent,";
                spms.Add(new SqlParameter("@messageContent",messages.MessageContent));
            }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new SqlParameter("@ID", messages.ID));
            return SqlHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">需要删除的数据主键id列表，可传单个ID，也可传数组</param>
        /// <returns></returns>
        public int Delete(params int[] ids)
        {
            if(ids.Length==0)return 0;
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="delete from Messages where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new SqlParameter("@id"+i,ids[i]));
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return SqlHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="messages">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Messages messages)
        {
            string sql="insert into Messages(IP,CreateTime,IsAudit,CreateName,MessageContent) values ("+(messages.IP==null?"null":"@iP")+","+(messages.CreateTime==null?"null":"@createTime")+","+(messages.IsAudit==null?"null":"@isAudit")+","+(messages.CreateName==null?"null":"@createName")+","+(messages.MessageContent==null?"null":"@messageContent")+");Select @@IDENTITY";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@iP",messages.IP??(object)DBNull.Value),new SqlParameter("@createTime",messages.CreateTime??(object)DBNull.Value),new SqlParameter("@isAudit",messages.IsAudit??(object)DBNull.Value),new SqlParameter("@createName",messages.CreateName??(object)DBNull.Value),new SqlParameter("@messageContent",messages.MessageContent??(object)DBNull.Value)
            };
            return int.Parse(SqlHelper.ExecuteScalar(sql,spms).ToString());
        }
    }
}
