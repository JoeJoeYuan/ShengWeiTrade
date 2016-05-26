using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IService;
using Models;
using System.Xml.Linq;

namespace yujiajunMVC.Controllers
{
    public class ExtensionsController : Controller
    {
        //
        // GET: /Extensions/
        private readonly INavigationService _service;
        private readonly ILinksService _linkService;
        private const string picturePath = "~/XML/poster.xml";

        public ExtensionsController(INavigationService service,ILinksService linkService)
        {
            _service = service;
            _linkService = linkService;
        }
        public ActionResult Navigation()
        {
            string path = string.Empty;
            List<Navigation> list = _service.GetAll().FindAll(g => g.ParentID == 0 && g.IsLock == 1).OrderBy(g => g.Sort.Value).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    string Navpath = string.IsNullOrEmpty(item.NPath) ? "javascript:void(0)" : "/Home/" + item.NPath;
                    path += "<li><a href=" + Navpath + ".html title=\"" + item.NName + "\">" + item.NName + "</a></li>";
                }
            }
            //path += "<li><a href=\"/Home/About.html\" title=\"关于我们\"><span>关于我们</span></a></li>";
            ViewBag.path = path;
            return View();
        }
        public ActionResult PictureRoll()
        {
            string picture = string.Empty;
            XElement xe = XElement.Load(Server.MapPath(picturePath));
            var query = from value in xe.Elements("img")
                        select value;
            foreach (var item in query)
            {
                //picture += "<li style=\"width: 100%;\"><a href=\"#\"><img src=\"/File/ScrollIamge/" + item.Element("path").Value + "\" title=\"" + item.Element("title").Value + "\" alt=\"" + item.Element("title").Value + "\" /></a></li>";
                //picture += "<img src=\"/File/ScrollIamge/" + item.Element("path").Value + "\" title=\"" + item.Element("title").Value + "\" alt=\"" + item.Element("title").Value + "\" />";
                picture += "<li><img src=\"/File/ScrollIamge/" + item.Element("path").Value + "\" title=\"" + item.Element("title").Value + "\" alt=\"" + item.Element("title").Value + "\" /></li>";
            }
            ViewBag.picture = picture;
            return View();
        }
        public ActionResult Link()
        {
            string link = string.Empty;
            List<Links> listLink = _linkService.GetAll();
            if (listLink.Count > 0)
            {
                foreach (var item in listLink)
                {
                    link += "<a href=" + item.LPath + " target=\"_blank\">" + item.LName + "</a> &nbsp;|&nbsp; ";
                }
            }
            ViewBag.link = link;
            return View();
        }

    }
}
