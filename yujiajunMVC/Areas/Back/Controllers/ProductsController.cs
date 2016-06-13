using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using IService;
using Webdiyer.WebControls.Mvc;

namespace yujiajunMVC.Areas.Back.Controllers
{
    [BasePage]
    public class ProductsController : Controller
    {
        //
        // GET: /Back/News/
        private readonly INewsService _newsService;
        private readonly INavigationService _navService;
        private readonly IProductsService _productsService;
        private List<Navigation> listNav = null;
        private List<SelectListItem> listItem = new List<SelectListItem>();

        public ProductsController(INewsService newsService, INavigationService navService, IProductsService productsService)
        {
            _newsService = newsService;
            _navService = navService;
            _productsService = productsService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductsAdd(string NID, string img)
        {
            ViewData["NID"] = NID;
            ViewData["img"] = img;
            return View();
        }
        [ValidateInput(false)]
        public ActionResult InsertProducts(Products products)
        {
            if (string.IsNullOrEmpty(products.Description))
            {
                return Content("<script>alert('内容不能为空');window.history.back();</script>");
            }
            products.NID = products.NID;
            products.Title = products.Title;
            products.Author = string.IsNullOrEmpty(products.Author) ? "佚名" : products.Author;
            products.CreateTime = DateTime.Now;

            string fileName = string.Empty;
            //if (Request.Files.Count >0)
            //{
            //    HttpPostedFileBase file = Request.Files[0];
            //    if (!string.IsNullOrWhiteSpace(file.FileName))
            //    {
            //        //保存成自己的文件全路径,newfile就是你上传后保存的文件,　　　　　　
            //        //服务器上的UpLoadFile文件夹必须有读写权限　　　
            //        string suffix = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //        fileName = Guid.NewGuid() + suffix;
            //        file.SaveAs(Server.MapPath("~/File/" + fileName));
            //        products.IsFile = 1;
            //    }
            //    else
            //        products.IsFile = 0;
            //}
            //else
            //    products.IsFile = 0;
            products.FilePath = string.IsNullOrEmpty(fileName) ? "0" : fileName;
            //string picture = string.Empty;
            //if (products.ImagePath == "1")//图片新闻
            //{
            //    string imgPath = WebLog.SetPicture(Server.MapPath("~/File/"));
            //    picture = imgPath + "/" + Guid.NewGuid() + ".jpg";
            //    string imgName = PictureHelper.GetHtmlImageUrlSingle(products.Title);
            //    file.SaveAs(Server.MapPath("~/File/" + fileName));
            //    ImageHelper.MakeThumbnail(Server.MapPath(imgName), Server.MapPath("~/File/" + picture), 114, 144, "Cut");
            //}

            string imgPath = string.Empty;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (!string.IsNullOrWhiteSpace(file.FileName))
                {
                    //保存成自己的文件全路径,newfile就是你上传后保存的文件,　　　　　　
                    //服务器上的UpLoadFile文件夹必须有读写权限　　　
                    string suffix = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    imgPath = Guid.NewGuid() + suffix;
                    string imgName = PictureHelper.GetHtmlImageUrlSingle(products.Title);
                    file.SaveAs(Server.MapPath("~/File/" + imgPath));
                    ImageHelper.MakeThumbnail(Server.MapPath(imgName), Server.MapPath("~/File/" + imgPath), 114, 144, "Cut");
                }
            }

            products.ImagePath = string.IsNullOrEmpty(imgPath) ? "" : imgPath;
            products.Description = products.Description;
            products.IsHot = products.IsHot;
            products.IsTop = products.IsTop;
            int num = _productsService.Insert(products);
            return Content("<script>alert('添加成功');window.location.href='productsList?NID=" + products.NID + "'</script>");
        }

        public ActionResult LeftProducts()
        {
            ViewBag.navigation = ProductsExtensions.GetNavigation(_navService.GetAll());
            return View();
        }
        public ActionResult ProductsList(string NID, int? Id = 1)
        {
            Navigation nav = _navService.GetById(int.Parse(NID));
            if (nav != null)
            {
                ViewBag.img = nav.IsImg.Value;
                ViewBag.name = nav.NName;
            }
            ViewBag.NID = NID;
            int pageIndex = Id.Value;//当前页数
            int pageSize = 20;//每页条数
            List<Products> list = _productsService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", new Products() { NID = int.Parse(NID) });
            return View(new PagedList<Products>(list, pageIndex, pageSize, _productsService.GetCount(new Products() { NID = int.Parse(NID) })));
        }
        public ActionResult ProductsEDIT(string ID)
        {
            Products products = _productsService.GetById(int.Parse(ID));
            listNav = _navService.GetAll();
            GetID(0);
            ViewData["NID"] = new SelectList(listItem, "Value", "Text", products.NID.Value);
            return View(products);
        }
        [ValidateInput(false)]
        public ActionResult EDITProducts(Products products)
        {
            Products oldProducts = _productsService.GetById(products.ID.Value);
            if (oldProducts != null)
            {
                //if (Request.Files.Count > 0)//有附件
                //{
                //    HttpPostedFileBase file = Request.Files[0];
                //    if (!string.IsNullOrWhiteSpace(file.FileName))
                //    {
                //        if (oldProducts.IsFile != 0)
                //            System.IO.File.Delete(Server.MapPath("~/File/" + oldNews.FilePath));
                //        string fileName = string.Empty;
                //        //保存成自己的文件全路径,newfile就是你上传后保存的文件,　　　　　　
                //        //服务器上的UpLoadFile文件夹必须有读写权限　　　
                //        string suffix = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                //        fileName = Guid.NewGuid() + suffix;
                //        file.SaveAs(Server.MapPath("~/File/" + fileName));
                //        oldProducts.FilePath = string.IsNullOrWhiteSpace(fileName) ? "0" : fileName;
                //        oldProducts.IsFile = 1;
                //    }
                //}


                string picture = string.Empty;
                if (Request.Files.Count != 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (!string.IsNullOrWhiteSpace(file.FileName))
                    {
                        //保存成自己的文件全路径,newfile就是你上传后保存的文件,　　　　　　
                        //服务器上的UpLoadFile文件夹必须有读写权限　　　
                        string suffix = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        picture = Guid.NewGuid() + suffix;
                        string imgName = PictureHelper.GetHtmlImageUrlSingle(products.Title);
                        //string fileName = Guid.NewGuid() + suffix;
                        file.SaveAs(Server.MapPath("~/File/" + picture));
                        ImageHelper.MakeThumbnail(Server.MapPath("~/File/" + imgName), Server.MapPath("~/File/" + picture), 265, 300, "HW");
                        //System.IO.File.Delete(Server.MapPath("~/File/ScrollIamge/" + fileName));
                    }
                }

                int num = _productsService.Update(products);
                if (num > 0)
                    return Content("<script>alert('修改成功');window.location='NewsList?NID=" + products.NID + "'</script>");
                return Content("<script>alert('修改失败');window.history.back();</script>");
            }
            return Content("<script>alert('改记录已不存在,或已被删除');window.history.back();</script>");
        }
        public ActionResult NewsDetail(string ID)
        {
            News news = _newsService.GetById(int.Parse(ID));
            listNav = _navService.GetAll();
            GetID(0);
            ViewData["NID"] = new SelectList(listItem, "Value", "Text", news.NID.Value);
            return View(news);
        }
        public ActionResult DELNews(int? ID)
        {
            News news = _newsService.GetById(ID ?? 0);
            int num = 0;
            if (news != null)
            {
                if (news.FilePath != "0")
                    System.IO.File.Delete(Server.MapPath("~/File/" + news.FilePath));
                if (news.ImagePath != "0")
                    System.IO.File.Delete(Server.MapPath("~/File/" + news.ImagePath));
                num = _newsService.Delete(ID ?? 0);
            }
            if (num > 0)
                return Content("<script>alert('删除成功');window.location='NewsList?NID=" + news.NID + "'</script>");
            return Content("<script>alert('删除失败');window.history.back();</script>");
        }
        public void DownLoad(string filePath)
        {
            if (System.IO.File.Exists(Server.MapPath("~/File/" + filePath)))
            {
                DownLoadFile.ResponseFile(Server.MapPath("~/File/" + filePath), filePath);
            }
        }
        /// <summary>
        /// 递归加载dropdownlist 
        /// </summary>
        /// <param name="ID">父级ID (测试传入为0)</param>
        protected void GetID(int? ID)
        {
            string text = string.Empty;
            List<Navigation> lists = listNav.FindAll(g => g.ParentID == ID);//根据父类查找子类
            if (lists.Count > 0)//有子类
            {
                if (ID != 0)//第一次加载大类 默认不加载符号
                    text += "　";
                string mark = text;//保存同一级节点的运算符个数
                foreach (var item in lists)
                {
                    listItem.Add(new SelectListItem() { Value = item.ID.Value.ToString(), Text = text + "∟" + item.NName });//绑定
                    GetID(item.ID);//递归循环
                    text = mark;//同一级节点修改运算符
                }
            }
        }
    }
}
