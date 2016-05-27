using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using IService;
using System.Text;

namespace yujiajunMVC
{
    public class NewsExtensions
    {
        private static List<Navigation> list = null;
        public static string GetNavigation(List<Navigation> listNav)
        {
            list = listNav;
            StringBuilder sb = new StringBuilder(5000);
            if (list != null)
            {
                sb.Append("<SCRIPT type=\"text/javascript\">var setting = {};var nodes=[");
                foreach (var item in list.FindAll(a => a.ParentID == 0))
                {
                    if (list.FindAll(g => g.ParentID == item.ID).Count > 0)//该大类下有子类
                    {
                        //string str = string.IsNullOrEmpty(item.NPath.Trim()) ? "" : ",url:\"NewsList.aspx?ID=" + item.ID + "&name="+item.NName+"\",target:\"newsMain\",icon:\"../img/folder_accept.png\"";
                        // sb.Append("{name:\"" + item.NName + "\",open:true" + GetParentID(item.ID) + str + "},");
                        //如需添加第一级下有新闻(如 资料下载为第一级) 请注释下面代码  使用上面代码
                        sb.Append("{name:\"" + item.NName + "\",open:true" + GetParentID(item.ID) + "},");
                    }
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append("];");
                sb.Append("$(document).ready(function () {$.fn.zTree.init($(\"#treeDemo\"), setting, nodes);});</SCRIPT>");
            }
            return sb.ToString();
        }
        private static string GetParentID(int? ID)
        {
            string str = string.Empty;
            List<Navigation> listChild = list.FindAll(a => a.ParentID == ID);
            if (listChild.Count > 0)
            {
                str += ",children:[";
                foreach (var item in listChild)
                {
                    str += "{name:\"" + item.NName + "\",url:\"NewsList?NID=" + item.ID + "\",target:\"newsMain\"},";
                }
                str = str.Substring(0, str.Length - 1);
                str += "]";
            }
            else //为空是否显示为文件夹样式
                str = ",icon:\"../img/folder_accept.png\"";
            //str = ",isParent:true";
            return str;
        }
    }
}