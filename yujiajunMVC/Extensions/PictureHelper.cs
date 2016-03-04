using System;
using System.Text.RegularExpressions;


namespace yujiajunMVC
{
    public class PictureHelper
    {
        /// <summary>
        /// 匹配src
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public static string GetHtmlImageUrlSingle(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            if (sUrlList.Length > 0)
            {
                string url = string.Empty;
                // 取得匹配项列表
                foreach (Match match in matches)
                    sUrlList[i++] = match.Groups["imgUrl"].Value;

                return sUrlList[0];

            }
            else
                return "";

        }
        //////////////////////////////获取所有img标签//////////////////////////////////////////
        /// <summary>
        /// 匹配src
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }
    }
}