using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using yujiajunMVC.Areas.Back.Models;
using IService;
using Models;
using System.IO;

namespace yujiajunMVC.Areas.Back.Controllers
{
    public class OthersController : Controller
    {
        private const string path = "~/XML/poster.xml";
        private readonly IUsersService _userService;
        public OthersController(IUsersService userService)
        {
            _userService = userService;
        }
        //
        // GET: /Back/Others/
        #region About
        [BasePage]
        public ActionResult About()
        {
            if (!System.IO.File.Exists(Server.MapPath("~/File/config/About.txt")))
                System.IO.File.Create(Server.MapPath("~/File/config/About.txt"));
            using (StreamReader reader = new StreamReader(Server.MapPath("~/File/config/About.txt"), System.Text.Encoding.Default))
            {
                ViewData["content"] = reader.ReadToEnd();
            }
            return View();
        }
        [ValidateInput(false)]
        [BasePage]
        public ActionResult AboutValue(string AboutContent)
        {
            if(string.IsNullOrWhiteSpace(SourceIO.NoHTML(AboutContent)))
                return Content("<script>alert('提交内容不能为空');window.history.back();</script>");
            if (!System.IO.File.Exists(Server.MapPath("~/File/config/About.txt")))
                System.IO.File.Create(Server.MapPath("~/File/config/About.txt"));
            using (StreamWriter writer = new StreamWriter(Server.MapPath("~/File/config/About.txt"),false,System.Text.Encoding.Default))
            {
                writer.WriteLine(AboutContent);
            }
            
            return Content("<script>alert('编辑成功');window.history.back();</script>");
        }
        #endregion

        #region RollPicture
        [BasePage]
        public ActionResult RollPicture()
        {
            XElement xe = XElement.Load(Server.MapPath(path));
            var query = from value in xe.Elements("img")
                        select value;
            List<Picture> list = new List<Picture>();
            foreach (var item in query)
            {
                Picture pic = new Picture();
                pic.ID = item.Element("id").Value;
                pic.Title = item.Element("title").Value;
                pic.Path = item.Element("path").Value;
                list.Add(pic);
            }
            return View(list);
        }
        [BasePage]
        public ActionResult RollPictureAdd()
        {
            return View();
        }
        [BasePage]
        public ActionResult RollPictureInsert(string Title = null)
        {
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
                    string fileName = Guid.NewGuid() + suffix;
                    file.SaveAs(Server.MapPath("~/File/ScrollIamge/" + fileName));
                    //ImageHelper.MakeThumbnail(Server.MapPath("~/File/ScrollIamge/" + fileName), Server.MapPath("~/File/ScrollIamge/" + image), 950, 240, "Cut");
                    ImageHelper.MakeThumbnail(Server.MapPath("~/File/ScrollIamge/" + fileName), Server.MapPath("~/File/ScrollIamge/" + picture), 500, 400, "H");
                    System.IO.File.Delete(Server.MapPath("~/File/ScrollIamge/" + fileName));
                }
            }
            XElement xe = XElement.Load(Server.MapPath(path));
            XElement element = new XElement(XName.Get("img"));
            element.SetElementValue("id", DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            element.SetElementValue("title", Title);
            element.SetElementValue("path", picture);
            xe.Add(element);
            xe.Save(Server.MapPath(path));
            return Content("<script>alert('添加成功');window.location='RollPicture'</script>");
        }
        [BasePage]
        public ActionResult RollPictureEDIT(string ID)
        {
            Picture picture = null;
            XElement xe = XElement.Load(Server.MapPath(path));
            var query = from value in xe.Elements("img")
                        select value;
            foreach (var item in query)
            {
                if (item.Element("id").Value == ID)
                {
                    picture = new Picture();
                    picture.ID = item.Element("id").Value;
                    picture.Title = item.Element("title").Value;
                    picture.Path = item.Element("path").Value;
                    break;
                }
            }
            return View(picture);
        }
        [BasePage]
        public ActionResult RollPictureUpdate(Picture picture)
        {
            string image = string.Empty;
            if (Request.Files.Count != 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (!string.IsNullOrWhiteSpace(file.FileName))
                {
                    //保存成自己的文件全路径,newfile就是你上传后保存的文件,　　　　　　
                    //服务器上的UpLoadFile文件夹必须有读写权限　
                    string suffix = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    image = Guid.NewGuid() + suffix;
                    string fileName = Guid.NewGuid().ToString() + suffix;
                    file.SaveAs(Server.MapPath("~/File/ScrollIamge/" + fileName));
                    ImageHelper.MakeThumbnail(Server.MapPath("~/File/ScrollIamge/" + fileName), Server.MapPath("~/File/ScrollIamge/" + image), 950, 240, "Cut");
                    System.IO.File.Delete(Server.MapPath("~/File/ScrollIamge/" + fileName));
                }
            }
            XElement xe = XElement.Load(Server.MapPath(path));
            var query = from value in xe.Elements("img")
                        select value;
            foreach (var item in query)
            {
                if (item.Element("id").Value == picture.ID)
                {
                    if (!string.IsNullOrEmpty(item.Element("path").Value))
                        System.IO.File.Delete(Server.MapPath("~/File/ScrollIamge/" + item.Element("path").Value));
                    item.SetElementValue("title", picture.Title);
                    if (!string.IsNullOrEmpty(image))
                        item.SetElementValue("path", image);
                    break;
                }
            }
            xe.Save(Server.MapPath(path));
            return Content("<script>alert('修改成功');window.location='RollPicture'</script>");
        }
        [BasePage]
        public ActionResult DELRollPicture(string ID)
        {
            if (!string.IsNullOrWhiteSpace(ID))
            {
                XElement xe = XElement.Load(Server.MapPath(path));
                var query = from value in xe.Elements("img")
                            select value;
                foreach (var item in query)
                {
                    if (item.Element("id").Value == ID)
                    {
                        if (!string.IsNullOrEmpty(item.Element("path").Value))
                            System.IO.File.Delete(Server.MapPath("~/File/ScrollIamge/" + item.Element("path").Value));
                        item.Remove();
                        break;
                    }
                }
                xe.Save(Server.MapPath(path));
                return Content("<script>alert('删除成功');window.location='RollPicture'</script>");
            }
            return Content("<script>alert('删除失败');window.history.back();</script>");
        }
        #endregion

        #region updatepwd
        [BasePage]
        public ActionResult UpdatePwd()
        {
            return View();
        }
        [BasePage]
        public ActionResult UpdatePassword(UserPassWordModel model)
        {
            Users user = _userService.GetSingle(new Users() { UserName = Request.Cookies["user"].Values["userName"], UserPassword = SourceIO.MD5(model.Pwd) });
            if (user == null)
                return Content("<script>alert('原始密码或用户名不匹配');window.history.back();</script>");
            int num = _userService.Update(new Users() { ID = int.Parse(Request.Cookies["user"].Values["ID"]), UserPassword = SourceIO.MD5(model.Pass) });
            if (num > 0)
                return Content("<script>alert('密码修改成功,下次登录生效');window.history.back();</script>");
            return Content("<script>alert('密码修改失败');window.history.back();</script>");
        }

        #endregion

        #region Login
        public ActionResult Login()
        {
            return View();

        }
        public ActionResult UserLogin(UserLogin login)
        {
            if (login == null) return Content("<script>window.history.back();</script>");
            if (login.Validate.ToLower() != Request.Cookies["validate"].Value.ToString())
            {
                return Content("<script>alert('验证码错误');window.history.back();</script>");
            }
            Users user = _userService.GetSingle(new Users() { UserName = login.LoginName, UserPassword = SourceIO.MD5(login.LoginPassWord) });
            if (user != null)
            {
                if (user.IsLock == 0)
                    return Content("<script>alert('该用户已被禁用');window.history.back();</script>");
                if (user.IsAdmin != 1)
                    return Content("<script>alert('你不是管理员,系统无法完成登录');window.history.back();</script>");
                HttpCookie cookie = new HttpCookie("user");
                cookie.Values.Add("userName", user.UserName);
                cookie.Values.Add("ID", user.ID.Value.ToString());
                Response.Cookies.Add(cookie);
                return Redirect("/Back/Main/Index");
            }
            return Content("<script>alert('用户名或密码不匹配');window.history.back();</script>");
        }
        #endregion
    }

    #region Extensions
    public class Picture
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
    }
    #endregion
}
