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
    public class LimitService:ILimitService
    {
        
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<Limit> GetAll()
        {
            string sql="select * from Limit";
            return SqlHelper.GetList<Limit>(sql);
        }
        /// <summary>
        /// 根据主键ID查询单条记录
        /// </summary>
        /// <param name="id">主键id</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Limit GetById(int id)
        {
            string sql="select * from Limit where ID=@id";
            SqlParameter spm=new SqlParameter("@id",id);
            return SqlHelper.GetSingle<Limit>(sql,spm);
        }
        /// <summary>
        /// 按照分页条件查询记录集
        /// </summary>
        /// <param name="startIndex">起始行号，从0开始</typeparam>
        /// <param name="count">需要查询的记录条数</param>
        /// <param name="sortColumn">排序列名，若要降序请用列名+" DESC"，传入空默认按照主键降序排列</param>
        /// <param name="limit">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public List<Limit> GetByPage(int startIndex,int count,string sortColumn,Limit limit)
        {
            if(string.IsNullOrEmpty(sortColumn))
                sortColumn="ID DESC";
            string sql="exec proc_GetByPage @startIndex,@count,@order,@tableName,@pkName,@where";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@startIndex",startIndex),
                new SqlParameter("@count",count),
                new SqlParameter("@order",sortColumn),
                new SqlParameter("@tableName","Limit"),
                new SqlParameter("@pkName","ID"),
                new SqlParameter("@where",GetConditions(limit))
            };
            return SqlHelper.GetList<Limit>(sql,spms);
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="limit">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns></returns>
        public int GetCount(Limit limit)
        {
            string sql="select count(*) from Limit where 1=1"+GetConditions(limit);
            return SqlHelper.GetCount(sql);
        }
        /// <summary>
        /// 生成查询条件的sql语句
        /// </summary>
        /// <param name="limit">查询条件，若无此条件请保持该属性默认值</typeparam>
        /// <returns>sql语句中where后面的部门，以" and"开始，sql语句中最后部分应是" where 1=1"</returns>
        private string GetConditions(Limit limit)
        {
            string condition="";
            if (limit != null)
            {
                if (limit.ID != null)
                {
                    condition += " and ID = '" + limit.ID + "'";
                }
                if (limit.FID != null)
                {
                    condition += " and FID = '" + limit.FID + "'";
                }
                if (limit.UID != null)
                {
                    condition += " and UID = '" + limit.UID + "'";
                }
                if (!string.IsNullOrEmpty(limit.Operate))
                {
                    condition += " and Operate like '%" + SqlHelper.GetParameterValue(limit.Operate) + "%'";
                }
            }
            
            
            
            return condition;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="limit">需要更新的数据实体</param>
        /// <returns></returns>
        //public int Update(Limit limit)
        //{
        //    string sql="update Limit set FID=@fID,UID=@uID,Operate=@operate where ID=@id";
        //    SqlParameter[] spms=new SqlParameter[]
        //    {
        //        new SqlParameter("@id",limit.ID),new SqlParameter("@fID",limit.FID??(object)DBNull.Value),new SqlParameter("@uID",limit.UID??(object)DBNull.Value),new SqlParameter("@operate",limit.Operate??(object)DBNull.Value)
        //    };
        //    return SqlHelper.ExecuteNonQuery(sql,spms);
        //}
       /// <summary>
        /// 更新数据，只传入需要更新的字段，可批量更新
        /// </summary>
        /// <param name="ids">需要更新的数据主键</param>
        /// <returns></returns>
        public int Update(Limit limit)
        {
            List<SqlParameter> spms=new List<SqlParameter>();
            string sql="update Limit set ";
            if (limit != null)
            {
                if (limit.FID != null)
                {
                    sql += "FID=@fID,";
                    spms.Add(new SqlParameter("@fID", limit.FID));
                }
                if (limit.UID != null)
                {
                    sql += "UID=@uID,";
                    spms.Add(new SqlParameter("@uID", limit.UID));
                }
                if (limit.Operate != null)
                {
                    sql += "Operate=@operate,";
                    spms.Add(new SqlParameter("@operate", limit.Operate));
                }
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=" where ID =@ID";
            spms.Add(new SqlParameter("@ID", limit.ID));
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
            string sql="delete from Limit where ID in (";
            for(int i=0;i<ids.Length;i++)
            {
                sql+="@id"+i+",";
                spms.Add(new SqlParameter("@id"+i,ids[i]));
            }
            sql=sql.Substring(0,sql.Length-1);
            sql+=")";
            return SqlHelper.ExecuteNonQuery(sql,spms.ToArray());
        }
        public int DeleteUID(int UID)
        {
            string sql = "delete from Limit where UID=@UID";
            return SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@UID", UID));
        }
        /// <summary>
        /// 插入数据，自增列的值对应更新在实体类参数对象中
        /// </summary>
        /// <param name="limit">需要插入的数据实体</param>
        /// <returns>影响行数</returns>
        public int Insert(Limit limit)
        {
            string sql="insert into Limit(FID,UID,Operate) values ("+(limit.FID==null?"null":"@fID")+","+(limit.UID==null?"null":"@uID")+","+(limit.Operate==null?"null":"@operate")+");Select @@IDENTITY";
            SqlParameter[] spms=new SqlParameter[]
            {
                new SqlParameter("@fID",limit.FID??(object)DBNull.Value),new SqlParameter("@uID",limit.UID??(object)DBNull.Value),new SqlParameter("@operate",limit.Operate??(object)DBNull.Value)
            };
            return int.Parse(SqlHelper.ExecuteScalar(sql,spms).ToString());
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Insert(List<Limit> list)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("insert into Limit(UID,FID,Operate)");
            int num = 0;//记录循环次数
            int count = list.Count;//总数
            foreach (var item in list)
            {
                num += 1;
                sb.Append("select " + item.UID + "," + item.FID + ",'" + item.Operate + "'");
                if (num != count)//不是最后一条
                {
                    sb.Append(" union all ");
                }
            }
            return SqlHelper.ExecuteNonQuery(sb.ToString());
        }
        /// <summary>
        /// 根据用户ID获取该用户权限
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public List<Limit> GetOperate(int UID)
        {
            string sql = @"select * from Limit as a left join Functions as b on a.FID=b.ID where a.UID=@UID";
            return SqlHelper.GetList<Limit>(sql, new SqlParameter("@UID", UID));

        }
    }
    
}
