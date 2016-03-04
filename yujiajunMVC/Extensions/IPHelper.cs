using System;
using System.Net; 
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yujiajunMVC
{
    /// <summary>
    /// <summary>  
    /// IP操作相关类【TODO 此类有待重构】  
    /// </summary>
    public class IPHelper
    {
        /// <summary>																																		
        /// 得到主机IP
        /// </summary>
        /// <param name="hostname">要解析的主机名称或IP地址</param>
        /// <returns></returns>
        public static string GetHostIP(string hostname)																							
       {																																
           try																																	
           {																																	
               System.Net.IPHostEntry IP = System.Net.Dns.GetHostEntry(hostname);  
               IPAddress[] address = IP.AddressList;																		
               String namehost = IP.HostName;																												
               return address[0].ToString();																												
           }																																
           catch (Exception ex)																															
           {																															
               throw new Exception(ex.Message);
           }																																		
       }
        ///<summary>
        /// 得到当前访问用户的IP
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string UserIp = string.Empty;
            try
            {
                //得到用户ip		
                if (HttpContext.Current != null)
                {
                    string strIp = string.Empty;
                    if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    {
                        strIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    }
                    else
                    {
                        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        {
                            strIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                        }
                    }
                    UserIp = strIp;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return UserIp;
        }
        ///<summary>	
        ///  根据域名解析出IP地址列表
        /// </summary>
        /// <param name="hostname">要解析的主机名称或IP地址</param>
        /// <returns></returns> 
        public static IPAddress[] ResolveIpByHostName(string hostname)
        {
            if (hostname == null || hostname.Length < 3) return null;
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(hostname);
                IPAddress[] address = hostInfo.AddressList;
                return (address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary> 																																	
        /// 描述：IP转化成整型 
        ///编制：yangqj																																		
        /// </summary>																																		
        /// <param name="IP">IP地址</param> 
        /// <returns>整型IP</returns> 
        public static long IpToInt(string IP)
        {
            if (IP == "") return -1;    //无效的IP
            string[] btyIP = IP.Split('.');
            long intReturn = 0;
            long intValue = 0;

            if (btyIP.GetUpperBound(0) != 3) return -2;
            for (int i = 0; i <= btyIP.GetUpperBound(0); i++)
            {
                intValue = Convert.ToInt32(btyIP[i]);
                if (intValue < 0 && intValue > 255) return -3;
                intReturn += intValue * (long)Math.Pow(256, (3 - i));
            }
            return intReturn;
        }

        #region 对Ip地址进行转换
        /// <summary>
        /// 对Ip地址进行转换
        /// </summary>
        /// <param name="strIp">需要转换的Ip地址</param>
        /// <returns>返回将字符型IP地址转换成整型</returns>
        public static uint ConvertToIIP(string strIp)
        {
            uint uIp = 0;
            try
            {
                System.Net.IPAddress ipaddr = System.Net.IPAddress.Parse(strIp);
                byte[] addbyte = ipaddr.GetAddressBytes();
                if (addbyte != null)
                {
                    byte[] IPBytes = new byte[4];
                    IPBytes[0] = addbyte[3];
                    IPBytes[1] = addbyte[2];
                    IPBytes[2] = addbyte[1];
                    IPBytes[3] = addbyte[0];
                    uIp = System.BitConverter.ToUInt32(IPBytes, 0);
                    IPBytes = null;
                }
                addbyte = null;
            }
            catch
            {
                return 0;
            }
            return uIp;
        }

        public static string ConvertToSIP(uint uIp)
        {
            string strIp = "";
            try
            {
                byte[] ipbyte = System.BitConverter.GetBytes(uIp);
                byte[] convbyte = new byte[4];
                convbyte[0] = ipbyte[3];
                convbyte[1] = ipbyte[2];
                convbyte[2] = ipbyte[1];
                convbyte[3] = ipbyte[0];
                uint ipp = System.BitConverter.ToUInt32(convbyte, 0);
                System.Net.IPAddress myip = new System.Net.IPAddress((long)ipp);
                strIp = myip.ToString();
                ipbyte = null;
                convbyte = null;
            }
            catch
            {
                strIp = "";
            }
            return strIp;
        }
        #endregion
    }
}
