using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace yujiajunMVC
{
    public class DownLoadFile
    {
        private static readonly object obj = new object();
        /// <summary>
        /// 以指定的ContentType输出指定文件文件 ResponseFile(Server.MapPath("~/Setting/Student/StudentExcel/学生导入模板.xls"), HttpUtility.UrlDecode("学生导入模板.xls"));
        /// </summary>
        public static void ResponseFile(string filepath, string filename)
        {
            lock (obj)
            {
                Stream iStream = null;

                // 缓冲区为10k
                byte[] buffer = new Byte[10000];
                // 文件长度
                int length;
                // 需要读的数据长度
                long dataToRead;

                try
                {
                    if (File.Exists(filepath))
                    {
                        // 打开文件
                        iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                        // 需要读的数据长度
                        dataToRead = iStream.Length;

                        HttpContext.Current.Response.ContentType = "application/octet-stream";
                        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename.Trim()));

                        while (dataToRead > 0)
                        {
                            // 检查客户端是否还处于连接状态
                            if (HttpContext.Current.Response.IsClientConnected)
                            {
                                length = iStream.Read(buffer, 0, 10000);
                                HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                                HttpContext.Current.Response.Flush();
                                buffer = new Byte[10000];
                                dataToRead = dataToRead - length;
                            }
                            else
                            {
                                // 如果不再连接则跳出死循环
                                dataToRead = -1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Write("Error : " + ex.Message);
                }
                finally
                {
                    if (iStream != null)
                    {
                        // 关闭文件
                        iStream.Close();
                    }
                }
                HttpContext.Current.Response.End();

            }
        }
    }
}