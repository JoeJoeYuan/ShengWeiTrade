using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Models;

namespace Service.Sqlserver
{
    public class SqlHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string strConn = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
        /// <summary>
        /// 准备Command对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        private static SqlCommand PrepareCommand(string sql, params SqlParameter[] spms)
        {
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (spms != null)
                cmd.Parameters.AddRange(spms);
            return cmd;
        }
        /// <summary>
        /// 提交sql语句执行（增删改）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] spms)
        {
            int result = 0;
            SqlCommand cmd = PrepareCommand(sql, spms);
            try
            {
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 提交sql语句返回首行首列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] spms)
        {
            object result = null;
            SqlCommand cmd = PrepareCommand(sql, spms);
            try
            {
                cmd.Connection.Open();
                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 提交sql语句执行（增删改）,bayistuta新增,2011/03/21 17:13
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType type, params SqlParameter[] spms)
        {
            int result = 0;
            SqlCommand cmd = PrepareCommand(sql, spms);
            cmd.CommandType = type;
            try
            {
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 提交sql语句返回读取器,bayistuta新增,2011/03/25 21:26
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType type, params SqlParameter[] spms)
        {
            SqlDataReader reader = null;
            SqlCommand cmd = PrepareCommand(sql, spms);
            cmd.CommandType = type;
            try
            {
                cmd.Connection.Open();
                //关闭reader对象，其对应的连接对象自动关闭
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                throw new Exception(ex.Message);
            }
            return reader;
        }
        /// <summary>
        /// 提交sql语句返回读取器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] spms)
        {
            SqlDataReader reader = null;
            SqlCommand cmd = PrepareCommand(sql, spms);
            try
            {
                cmd.Connection.Open();
                //关闭reader对象，其对应的连接对象自动关闭
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (reader != null)
                    reader.Close();
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                throw new Exception(ex.Message);
            }
            return reader;
        }
        /// <summary>
        /// 查询实体类对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(string sql, params SqlParameter[] spms)
        {
            List<T> list = new List<T>();
            SqlDataReader reader = ExecuteReader(sql, spms);
            while (reader.Read())
            {
                T t = CreateInstance<T>(reader);
                list.Add(t);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 查询单个实体类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        public static T GetSingle<T>(string sql, params SqlParameter[] spms)
        {
            T t = default(T);
            SqlDataReader reader = ExecuteReader(sql, spms);
            if (reader.Read())
            {
                t = CreateInstance<T>(reader);
            }
            reader.Close();
            return t;
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="spms">参数</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] spms)
        {
            SqlCommand cmd = PrepareCommand(sql, spms);

            SqlDataAdapter da = new SqlDataAdapter(cmd); //创建DataAdapter数据适配器实例
            DataSet ds = new DataSet();//创建DataSet实例
            da.Fill(ds, "tables");//使用DataAdapter的Fill方法(填充)，调用SELECT命令
            if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            return ds.Tables[0];
        }
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="spms">参数</param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql,CommandType cmdType, params SqlParameter[] spms)
        {
            SqlCommand cmd = PrepareCommand(sql, spms);
            cmd.CommandType = cmdType;
            SqlDataAdapter da = new SqlDataAdapter(cmd); //创建DataAdapter数据适配器实例
            DataSet ds = new DataSet();//创建DataSet实例
            da.Fill(ds, "tables");//使用DataAdapter的Fill方法(填充)，调用SELECT命令
			if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="spms"></param>
        /// <returns></returns>
        public static int GetCount(string sql, params SqlParameter[] spms)
        {
            return (int)ExecuteScalar(sql, spms);
        }
        /// <summary>
        /// 使用反射根据实体类的构造函数创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static T CreateInstance<T>(IDataReader reader)
        {
            Type type = typeof(T);
            T t = (T)Activator.CreateInstance(type, reader);
            return t;
        }

        
        /// <summary>
        /// 防sql注入，替换字符串
        /// </summary>
        /// <param name="val">需要替换的值</param>
        /// <returns>替换后的值</returns>
        public static string GetParameterValue(string val)
        {
            return val.Replace("'", "''").Replace("-","[-]");
        }
    }
}
