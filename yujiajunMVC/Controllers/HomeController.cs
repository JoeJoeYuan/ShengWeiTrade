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
        private readonly IProductsService _productService;

        public HomeController(IMessagesService messageService, INewsService newsService, INavigationService navService, IProductsService productsService)
        {
            _messageService = messageService;
            _newsService = newsService;
            _navService = navService;
            _productService = productsService;
        }
        public ActionResult Index()
        {
            // return RedirectToRoute("Back", new RouteValueDictionary { { "Action", "Login" }, { "Controller", "Others" } });
            ViewBag.productCategoty = ProductCategoty();
            //ViewBag.productHotList = _productService.GetHot();
            ViewBag.productHotList = _productService.GetHot();
            ViewBag.aboutUs = GetAbout();
            List<News> list = _newsService.GetByPage();
            return View(list);
        }
        public ActionResult About()
        {
            ViewBag.productCategoty = ProductCategoty();
            ViewBag.Title = "About us";
            if (Session["about"] == null)
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/File/config/About.txt"), System.Text.Encoding.Default))
                {
                    Session["about"] = reader.ReadToEnd();
                }
            }
            ViewData["about"] = Session["about"];
            //ViewBag.aboutUs = GetAbout();
            return View();
        }
        protected string GetAbout()
        {
            string aboutUs = "";
            if (Session["about"] == null)
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/File/config/About.txt"), System.Text.Encoding.Default))
                {
                    Session["about"] = reader.ReadToEnd();
                }
            }
            if (Session["about"].ToString().Length > 400)
            {
                aboutUs = Session["about"].ToString().Substring(0, 400) + "...";
            }
            else
            {
                aboutUs = Session["about"].ToString();
            }
            return aboutUs;
            //ViewData["about"] = Session["about"];
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
        public ActionResult Message(int? Id = 1)
        {
            List<News> listNews = _newsService.GetHot();
            string hotNews = string.Empty;
            foreach (var item in listNews)
            {
                hotNews += "<li><a href=\"NewsDetail/" + item.CreateTime.Value.ToString("yyyy-MM-dd") + "/" + item.ID + ".html\" target=\"_blank\">" + item.Title + "</a></li>";
            }
            ViewBag.hot = hotNews;

            ViewBag.productCategoty = ProductCategoty();

            Messages message = new Messages() { IsAudit = 1 };
            int pageSize = 5;
            int pageIndex = Id.Value;
            List<Messages> list = _messageService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", message);

            return View(new PagedList<Messages>(list, pageIndex, pageSize, _messageService.GetCount(message)));
        }
        [ValidateInput(false)]
        public ActionResult MessageInsert(string messageContent, string Name = "", string Code = "")
        {
            if (string.IsNullOrWhiteSpace(Request.Cookies["validate"].Value))
                return Content("<script>alert('验证码已过期,请重新获取');window.history.back();</script>");
            if (Code != Request.Cookies["validate"].Value)
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
            //List<News> listNews = _newsService.GetHot();//热点新闻
            //string hotNews = string.Empty;
            //foreach (var item in listNews)
            //{
            //    hotNews += "<li><a href=\"NewsDetail/" + item.CreateTime.Value.ToString("yyyy-MM-dd") + "/" + item.ID + ".html\" target=\"_blank\">" + item.Title + "</a></li>";
            //}
            //ViewBag.hot = hotNews;

            //List<Navigation> listNav = _navService.GetAll().FindAll(a => a.ParentID == 1);
            //if (listNav.Count > 0)
            //{
            //    string navigation = string.Empty;

            //    foreach (var item in listNav)
            //    {
            //        navigation += "<li><a href=\"" + item.NPath + "?NID=" + item.ID + "&name=" + item.NName + "\">" + item.NName + "</a></li>";
            //    }
            //    ViewBag.navigation = navigation;
            //}

            ViewBag.productCategoty = ProductCategoty();

            News news = null;
            if (NID != null)
            {
                news = new News() { NID = NID };
            }
            ViewBag.Title = name == null ? "News — Shen Zhen Sheng Wei Trade CO." : name + " — Shen Zhen Sheng Wei Trade CO.";
            ViewBag.name = name == null ? "News List" : name;
            int pageSize = 3;
            int pageIndex = Id.Value;
            List<News> list = _newsService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", news);
            return View(new PagedList<News>(list, pageIndex, pageSize, _newsService.GetCount(news)));
        }
        public ActionResult NewsDetail(int? ID)
        {
            News news = _newsService.GetById(ID ?? 0);

            ViewBag.productCategoty = ProductCategoty();

            ViewBag.Title = news == null ? "News Detail" : news.Title + " — Sheng Wei Trade CO.";
            return View(news);
        }
        #endregion

        #region products

        public ActionResult ProductsList(int? NID = null, string name = null, int? Id = 1)
        {
            //List<Products> listProducts = _productService.GetAll(); //产品列表
            //string products = string.Empty;
            //foreach (var item in listProducts)
            //{
            //    products += "<li><a href=\"ProductsDetail/" + item.CreateTime.Value.ToString("yyyy-MM-dd") + "/" + item.ID + ".html\" target=\"_blank\">" + item.Title + "</a></li>";
            //}
            //ViewBag.hot = products;

            ViewBag.productCategoty = ProductCategoty();

            Products product = null;
            if (NID != null)
            {
                product = new Products() { NID = NID, ImagePath = "0" };
            }
            else
                product = new Products() { ImagePath = "0" };

            ViewBag.Title = name == null ? "Products List" : name + " — Sheng Wei Trade CO.";
            ViewBag.name = name == null ? "Products List" : name;
            int pageSize = 3;
            int pageIndex = Id.Value;
            List<Products> list = _productService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", product);
            return View(new PagedList<Products>(list, pageIndex, pageSize, _productService.GetCount(product)));
        }

        public ActionResult ProductsDetail(int? ID)
        {
            Products products = _productService.GetById(ID ?? 0);

            ViewBag.productCategoty = ProductCategoty();

            ViewBag.Title = products == null ? "Products Detail" : products.Title + " — Sheng Wei Trade CO.";
            return View(products);
        }

        public string ProductCategoty()
        {
            string navigation = string.Empty;
            List<Navigation> listNav = _navService.GetByPage(100, 0, "ID DESC", new Navigation() { ParentID = 1 });
            if (listNav.Count > 0)
            {
                foreach (var item in listNav)
                {
                    navigation += "<li><a href=\"/Home/" + item.NPath + "?NID=" + item.ID + "&name=" + item.NName + "\">" + item.NName + "</a></li>";
                }
            }
            return navigation;
        }
        #endregion
    }
}
