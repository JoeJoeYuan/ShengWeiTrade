using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using IService;
using Webdiyer.WebControls.Mvc;
using System.IO;
using System.Web.Routing;

namespace yujiajunMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessagesService _messageService;
        private readonly INewsService _newsService;
        private readonly INavigationService _navService;

        public HomeController(IMessagesService messageService,INewsService newsService,INavigationService navService)
        {
            _messageService = messageService;
            _newsService = newsService;
            _navService = navService;
        }
        public ActionResult Index()
        {
           // return RedirectToRoute("Back", new RouteValueDictionary { { "Action", "Login" }, { "Controller", "Others" } });
            List<News> list = _newsService.GetByPage();
            return View(list);
        }
        public ActionResult About()
        {
            ViewBag.Title = "关于我们";
            if (Session["about"] == null)
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/File/config/About.txt"),System.Text.Encoding.Default))
                {
                    Session["about"] = reader.ReadToEnd();
                }
            }
            ViewData["about"] = Session["about"];
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        [ValidateInput(false)]
        public void DownLoad(int ID, string path)
        {
            _newsService.UpdateDownLoad(ID);
            if (System.IO.File.Exists(Server.MapPath("~/File/" + path)))
                DownLoadFile.ResponseFile(Server.MapPath("~/File/" + path), HttpUtility.UrlEncode(path));
            else
            {
                Response.Write("<font color='red'>未找到文件或该文件已删除</font>");
                Response.End();
            }
        }
        #region Message
        public ActionResult Message(int? Id=1)
        {
            List<News> listNews = _newsService.GetHot();
            string hotNews = string.Empty;
            foreach (var item in listNews)
            {
                hotNews += "<li><a href=\"NewsDetail/" + item.CreateTime.Value.ToString("yyyy-MM-dd") + "/" + item.ID + ".html\" target=\"_blank\">" + item.Title + "</a></li>";
            }
            ViewBag.hot = hotNews;
            
            Messages message = new Messages() { IsAudit = 1 };
            int pageSize = 5;
            int pageIndex = Id.Value;
            List<Messages> list = _messageService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", message);

            return View(new PagedList<Messages>(list, pageIndex, pageSize, _messageService.GetCount(message)));
        }
        [ValidateInput(false)]
        public ActionResult MessageInsert(string messageContent, string Name = "",string Code="")
        {
            if (string.IsNullOrWhiteSpace(Request.Cookies["validate"].Value))
                return Content("<script>alert('验证码已过期,请重新获取');window.history.back();</script>"); 
            if(Code!=Request.Cookies["validate"].Value)
                return Content("<script>alert('验证码错误');window.history.back();</script>");
            int num = _messageService.Insert(new Messages() { CreateName = string.IsNullOrWhiteSpace(Name) ? "匿名" : Name, IP = IPHelper.GetClientIP(), CreateTime = DateTime.Now, IsAudit = 0, MessageContent = messageContent });
            if (num > 0)
                return Content("<script>alert('留言成功,待审核');window.location='Message'</script>");
            return Content("<script>alert('留言失败');window.history.back();</script>");
        }
        #endregion

        #region News
        public ActionResult NewsList(int? NID = null, string name = null, int? Id = 1)
        {
            List<News> listNews = _newsService.GetHot();//热点新闻
            string hotNews = string.Empty;
            foreach (var item in listNews)
            {
                hotNews += "<li><a href=\"NewsDetail/" + item.CreateTime.Value.ToString("yyyy-MM-dd") + "/" + item.ID + ".html\" target=\"_blank\">" + item.Title + "</a></li>";
            }
            ViewBag.hot = hotNews;


            List<Navigation> listNav = _navService.GetAll().FindAll(a => a.ParentID == 1);
            if (listNav.Count > 0)
            {
                string navigation = string.Empty;

                foreach (var item in listNav)
                {
                    navigation += "<li><a href=\"" + item.NPath + "?NID=" + item.ID + "&name=" + item.NName + "\">" + item.NName + "</a></li>";
                }
                ViewBag.navigation = navigation;
            }


            News news = null;
            if (NID != null)
            {
                news = new News() { NID = NID };
            }
            ViewBag.Title = name == null ? "新闻列表 — 喻家军网络工作室" : name + " — 喻家军网络工作室";
            ViewBag.name = name == null ? "新闻列表" : name;
            int pageSize = 20;
            int pageIndex = Id.Value;
            List<News> list = _newsService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", news);
            return View(new PagedList<News>(list, pageIndex, pageSize, _newsService.GetCount(news)));
        }
        public ActionResult NewsDetail(int? ID)
        {
            News news = _newsService.GetById(ID ?? 0);

            ViewBag.Title = news == null ? "新闻详情" : news.Title + " — 喻家军网络工作室";
            return View(news);
        }
        #endregion

        #region products

        public ActionResult ProductsList(int? NID = null, string name = null, int? Id = 1)
        {
            List<News> listNews = _newsService.GetHot(); //热点新闻
            string hotNews = string.Empty;
            foreach (var item in listNews)
            {
                hotNews += "<li><a href=\"NewsDetail/" + item.CreateTime.Value.ToString("yyyy-MM-dd") + "/" + item.ID + ".html\" target=\"_blank\">" + item.Title + "</a></li>";
            }
            ViewBag.hot = hotNews;

            List<Navigation> listNav = _navService.GetByPage(100, 0, "ID DESC", new Navigation() { ParentID = 6 });
            if (listNav.Count > 0)
            {
                string navigation = string.Empty;
                foreach (var item in listNav)
                {
                    navigation += "<li><a href=\"" + item.NPath + "?NID=" + item.ID + "&name=" + item.NName + "\">" + item.NName + "</a></li>";
                }
                ViewBag.navigation = navigation;
            }

            News news = null;
            if (NID != null)
            {
                news = new News() { NID = NID, ImagePath = "0" };
            }
            else
                news = new News() { ImagePath = "0" };

            ViewBag.Title = name == null ? "产品中心 — 喻家军网络工作室" : name + " — 喻家军网络工作室";
            ViewBag.name = name == null ? "产品中心" : name;
            int pageSize = 20;
            int pageIndex = Id.Value;
            List<News> list = _newsService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", news);
            return View(new PagedList<News>(list, pageIndex, pageSize, _newsService.GetCount(news)));
        }
        #endregion
    }
}
