using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using IService;

namespace yujiajunMVC.Areas.Back.Controllers
{

    public class MainController : Controller
    {
        //
        // GET: /Back/Main/
        private readonly IFunctionsService _functionsService;
        private readonly ILimitService _limitService;
        private List<Functions> list = null;
        private List<Limit> listLimit = null;
        public MainController(IFunctionsService functionsService, ILimitService limitService)
        {
            _functionsService = functionsService;
            _limitService = limitService;
        }
        [BasePage]
        public ActionResult Index()
        {
            return View();
        }
        [BasePage]
        public ActionResult Header()
        {
            ViewBag.userName = Request.Cookies["user"].Values["userName"];
            return View();
        }
        public ActionResult Exit()
        {
            Response.Cookies.Clear();
            Session.Abandon();
            Session.Clear();
            return Content("<script>window.top.location='/Back/Others/Login'</script>");
        }
        [BasePage]
        public ActionResult Left()
        {
            list = _functionsService.GetAll();
            if (list != null)
            {
                List<Functions> listFun = list.FindAll(g => g.ParentID == 0 && g.IsLock == 1);//查询出所有启用的第一级大类
                listLimit = _limitService.GetOperate(int.Parse(Request.Cookies["user"].Values["ID"]));//该用户拥有的权限项
                list = (from item in list
                           where listLimit.FirstOrDefault(a=>a.FID==item.ID) !=null
                           select item).ToList();//查找当前用户所拥有功能的权限

                if (listFun.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(5000);
                    for (int i = 0; i < listFun.Count; i++)
                    {
                        string text = GetById(listFun[i].ID, (i + 1));
                        if (!string.IsNullOrWhiteSpace(text))//没有子节点就不显示当先节点
                        {
                            sb.AppendLine("<TABLE cellSpacing=0 cellPadding=0 width=\"100%\" border=0><TR><TD height=2></TD></TR></TABLE>");
                            sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"0\" width=\"150\" border=\"0\"><tr height=\"22\"><td style=\"padding-left: 30px\" background=\"/Areas/Back/images/menu_bt.jpg\"><a class=\"menuParent\" onclick=\"expand(" + (i + 1) + ")\" href=\"javascript:void(0);\">" + listFun[i].FName + "</a></td></tr><tr height=\"4\"><td></td></tr></table>");
                            sb.AppendLine(text);
                        }
                    }
                    ViewBag.funtion = sb.ToString();
                }
            }
            return View();
        }
        private string GetById(int? ID, int num)
        {
            string str = string.Empty;
            List<Functions> listFun = list.FindAll(g => g.ParentID == ID);
            if (listFun.Count > 0)
            {
                str += " <table id=\"child" + num + "\" style=\"display: none\" cellspacing=\"0\" cellpadding=\"0\" width=\"150\" border=\"0\">";
                foreach (var item in listFun)
                {
                    str += "<tr height=\"20\"><td align=\"middle\" width=\"30\"><img height=\"9\" src=\"/Areas/Back/images/menu_icon.gif\" width=\"9\"></td><td><a class=\"menuChild\" href=\"" + item.FPath + "\" target=\"main\">" + item.FName + "</a></td></tr>";
                }
                str += "<tr height=\"4\"><td colspan=\"2\"></td></tr></table>";
            }
            return str;
        }


    }
}
