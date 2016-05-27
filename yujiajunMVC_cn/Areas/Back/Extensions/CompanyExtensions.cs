using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;

namespace yujiajunMVC
{
    public class CompanyExtensions
    {
        private List<string> list = new List<string>(); //要修改文件后缀名
        private readonly string openFileType = ConfigurationManager.AppSettings["openFileType"].ToString();
        private readonly string config = ConfigurationManager.AppSettings["IsConfig"].ToString().ToLower();
        public StringBuilder sb1 = new StringBuilder();
        public CompanyExtensions()
        {
            string[] fileType = openFileType.Split(',');
            foreach (var item in fileType)
            {
                list.Add(item.ToLower());
            }
            if (config == "true")//允许修改配置文件
                list.Add(".config");
        }
        public string GetPath()
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("<SCRIPT type=\"text/javascript\">var setting = {};var nodes=[");
            sb.Append(GetFiles(HttpContext.Current.Server.MapPath("~/")));
            string str = GetChildFiles(HttpContext.Current.Server.MapPath("~/"));
            if (!string.IsNullOrEmpty(str))
                sb.Append(str);
            else
                sb = sb.Remove(sb1.Length - 1, 1);
            sb.Append("];");
            sb.Append("$(document).ready(function () {$.fn.zTree.init($(\"#treeDemo\"), setting, nodes);});</SCRIPT>");
            return sb.ToString();
        }
        public string GetFiles(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo[] files = info.GetDirectories();
            foreach (var item in files)
            {
                if (item.Name == "zTree" || item.Name == "kindeditor-4.0")
                    continue;
                sb1.Append("{name:\"" + item.Name + "\"");
                if (Exixts(item.FullName))
                {
                    sb1.Append(",children:[");
                    GetFiles(item.FullName);
                    string str = GetChildFiles(item.FullName);
                    if (!string.IsNullOrEmpty(str))
                        sb1.Append(GetChildFiles(item.FullName));
                    else
                        sb1 = sb1.Remove(sb1.Length - 1, 1);
                    sb1.Append("]},");
                }
                else
                {
                    string str = GetChildFiles(item.FullName);
                    if (!string.IsNullOrEmpty(str))
                    {
                        sb1.Append(",children:[");
                        sb1.Append(GetChildFiles(item.FullName));
                        sb1.Append("]},");
                    }
                    else
                        sb1.Append(",isParent:true},");
                }
            }
            return string.IsNullOrEmpty(sb1.ToString()) ? string.Empty : sb1.ToString();
        }
        private bool Exixts(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo[] fda = info.GetDirectories();
            if (fda.Length > 0)
            {
                return true;
            }
            return false;
        }
        public string GetChildFiles(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            FileInfo[] files = info.GetFiles();
            string text = string.Empty;
            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    string url = string.Empty;
                    string value = string.Empty;
                    if (item.FullName.LastIndexOf('.') > -1 && list.Count > 0)
                    {
                        string file = item.FullName.Substring(item.FullName.LastIndexOf('.'));
                        value = list.FirstOrDefault(g => g == file.ToLower());
                    }
                    if (!string.IsNullOrEmpty(value))
                    {
                        url = ",url:\"CompanyMain?path=" + item.FullName.Replace("\\", "\\\\") + "\",target:\"newsMain\",icon:\"/Areas/Back/img/accept_page.png\"";
                    }
                    text += "{name:\"" + item.Name + "\"" + url + "},";
                }
                text = text.Substring(0, text.Length - 1);
            }
            return text;
        }
    }
}