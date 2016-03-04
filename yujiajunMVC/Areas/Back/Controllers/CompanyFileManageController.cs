using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Text;
using System.IO;

namespace yujiajunMVC.Areas.Back.Controllers
{
    public class CompanyFileManageController : Controller
    {
        //
        // GET: /Back/CompanyFileManage/
        private readonly CompanyExtensions _company;
        public CompanyFileManageController(CompanyExtensions company)
        {
            _company = company;
        }
        [BasePage]
        public ActionResult Index()
        {
            return View();
        }
        [BasePage]
        public ActionResult CompanyLeft()
        {
            ViewBag.file = _company.GetPath();
            return View();
        }
        [BasePage]
        public ActionResult CompanyMain()
        {
            string path = Request.QueryString["path"];
            using (StreamReader sr = new StreamReader(path))
            {
                ViewData["content"] = sr.ReadToEnd();
            }
            return View();
        }
        [ValidateInput(false)]
        public ActionResult CompanyFileUpdate(string FileContent,string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(FileContent);
            }
            return Content("<script>alert('修改成功');window.history.back();</script>");
        }
        [ValidateInput(false)]
        public void DownLoad(string path)
        {
            if (System.IO.File.Exists(path))
                DownLoadFile.ResponseFile(path, HttpUtility.HtmlEncode(ModuleName(path)));
            else
                throw new Exception(path+"\n该文件不存在");
           // return Content("<script>window.history.back();</script>");
        }
        private string ModuleName(string path)
        {
            string[] str = path.Split('\\');
            return str[str.Length - 1].Trim();
        }
    }
}
