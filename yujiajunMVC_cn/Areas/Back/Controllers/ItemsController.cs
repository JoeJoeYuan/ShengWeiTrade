using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using IService;
using System.Web.UI.WebControls;
using Webdiyer.WebControls.Mvc;
using yujiajunMVC.Areas.Back.Models;

namespace yujiajunMVC.Areas.Back.Controllers
{
    [BasePage]
    public class ItemsController : Controller
    {
        //
        // GET: /Back/Items/
        private readonly IFunctionsService _functionsService;
        private readonly ILinksService _linksService;
        private readonly INavigationService _navigationService;
        private readonly IUsersService _userService;
        private readonly ILimitService _limitService;

        public ItemsController(IFunctionsService functionsService, ILinksService linksService, INavigationService navigationService, IUsersService userService, ILimitService limitService)
        {
            _functionsService = functionsService;
            _linksService = linksService;
            _navigationService = navigationService;
            _userService = userService;
            _limitService = limitService;
        }
        #region Functions
        /// <summary>
        /// 加载大类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoadDropDownList()
        {
            List<Functions> listFun = new List<Functions>();
            listFun.Add(new Functions() { ID = 0, FName = "--无上级--" });
            listFun.AddRange(_functionsService.GetAll().FindAll(g => g.ParentID == 0));
            return Json(listFun);
        }
        public ActionResult FunctionList(int? id = 1)
        {
            int pageIndex = id.Value;//当前页数
            int pageSize = 20;//每页条数
            List<Functions> list = _functionsService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", null);
            return View(new PagedList<Functions>(list, pageIndex, pageSize, _functionsService.GetCount(null)));
        }
        public ActionResult FunctionADD()
        {
            return View();
        }
        public ActionResult InsertFunction(FunctionModel model)
        {
            int num = _functionsService.Insert(model.ToEntity());
            if (num > 0)
                return Content("<script>alert('添加成功');window.parent.location='FunctionList'</script>");
            return Content("<script>alert('添加失败');window.history.back();</script>");
        }
        public ActionResult FunctionEDIT(int? ID)
        {
            Functions function = _functionsService.GetById(ID ?? 0);
            if (function != null)
                return View(function.ToModel());
            return View();
        }
        public ActionResult EDITFunction(FunctionModel model)
        {
            int num = _functionsService.Update(model.ToEntity());
            if (num > 0)
                return Content("<script>alert('编辑成功');window.parent.location='FunctionList'</script>");
            return Content("<script>alert('编辑失败');window.history.back();</script>");
        }
        
        public ActionResult DELFunction(int? ID)
        {
            if (_functionsService.GetCount(new Functions() { ParentID = ID.Value }) > 0)//有子类
                return Content("<script>alert('请先删除该类下子类');window.history.back();</script>");
            int num = _functionsService.Delete(ID ?? 0);
            if (num > 0)
                return Content("<script>window.location='FunctionList'</script>");
            return Content("<script>alert('删除失败');window.history.back();</script>");
        }
        #endregion

        #region Links
        public ActionResult LinkList(int? Id = 1)
        {
            int pageIndex = Id.Value;//当前页数
            int pageSize = 20;//每页条数
            List<Links> list = _linksService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", null);
            return View(new PagedList<Links>(list, pageIndex, pageSize, _linksService.GetCount(null)));
        }
        public ActionResult LinkADD()
        {
            return View();
        }
        public ActionResult InsertLink(Links links)
        {
            int num = _linksService.Insert(links);
            if (num > 0)
                return Content("<script>alert('添加成功');window.parent.location='LinkList'</script>");
            return Content("<script>alert('添加失败');window.history.back();</script>");
        }

        public ActionResult LinkEDIT(int? ID)
        {
            Links links = _linksService.GetById(ID ?? 0);
            return View(links);
        }

        public ActionResult EDITLink(Links links)
        {
            int num = _linksService.Update(links);
            if (num > 0)
                return Content("<script>alert('编辑成功');window.parent.location='LinkList'</script>");
            return Content("<script>alert('编辑失败');window.history.back();</script>");
        }

        public ActionResult DELLink(int? ID)
        {
            int num = _linksService.Delete(ID ?? 0);
            if (num > 0)
                return Content("<script>window.location='LinkList'</script>");
            return Content("<script>alert('删除失败');window.history.back();</script>");
        }
        #endregion

        #region NavigationList
        /// <summary>
        /// 加载大类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoadNavigationDropDownList()
        {
            List<Navigation> list = _navigationService.GetAll().FindAll(a => a.ParentID == 0);
            if (list == null)
                list = new List<Navigation>();
            list.Insert(0, new Navigation() { ID = 0, NName = "--无上级--" });
            return Json(list);
        }
        public ActionResult NavigationList(int? Id = 1)
        {
            int pageIndex = Id.Value;//当前页数
            int pageSize = 20;//每页条数
            List<Navigation> list = _navigationService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", null);
            return View(new PagedList<Navigation>(list, pageIndex, pageSize, _navigationService.GetCount(null)));
        }
        public ActionResult NavigationEDIT(int? ID)
        {
            Navigation nav = _navigationService.GetById(ID ?? 0);
            if (nav != null)
                return View(nav.ToNavModel());

            return View();
        }

        public ActionResult EDITNavigation(NavigationModel model)
        {
            int num = _navigationService.Update(model.ToEntity());
            if (num > 0)
                return Content("<script>alert('编辑成功');window.parent.location='NavigationList'</script>");

            return Content("<script>alert('编辑失败');window.history.back();</script>");
        }
        public ActionResult NavigationADD()
        {
            return View();
        }
        public ActionResult NavigationInsert(NavigationModel model)
        {
            int num = _navigationService.Insert(model.ToEntity());
            if (num > 0)
            {
                return Content("<script>alert('添加成功');window.parent.location='NavigationList'</script>");
            }
            return Content("<script>alert('添加失败');window.history.back();</script>");
        }

        public ActionResult DELNavigation(int? ID)
        {
            int num = _navigationService.Delete(ID ?? 0);
            if (num > 0)
            {
                return Content("<script>window.location='NavigationList'</script>");
            }
            return Content("<script>alert('删除失败');window.history.back();</script>");
        }
        #endregion

        #region User
        public ActionResult UserList(int? Id = 1)
        {
            int pageIndex = 1;//当前页数
            int pageSize = 20;//每页条数
            List<Users> list = _userService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", null);
            return View(new PagedList<Users>(list, pageIndex, pageSize, _userService.GetCount(null)));
        }
        public ActionResult UserADD()
        {
            return View();
        }

        public ActionResult InsertUser(UserModel model)
        {
            if(string.IsNullOrWhiteSpace(model.UserName)||string.IsNullOrWhiteSpace(model.UserPassword))
                return Content("<script>alert('页面不能有空值');window.history.back();</script>");
           int count = _userService.GetCount(new Users() { UserName=model.UserName});
            if(count>0)
                return Content("<script>alert('该用户名已存在');window.history.back();</script>");
            var user = model.ToEntity();
            user.UserPassword = SourceIO.MD5(model.UserPassword);
            int num = _userService.Insert(user);
            if (num > 0)
                return Content("<script>alert('添加成功');window.parent.location='UserList'</script>");

            return Content("<script>alert('添加失败');window.history.back();</script>");
        }
        public ActionResult UserEDIT(int? ID)
        {
            Users user = _userService.GetById(ID ?? 0);
            if (user != null)
            {
                return View(user.ToModel());
            }
            return View();
        }
        public ActionResult EDITUser(UserModel model)
        {
            int num = _userService.Update(model.ToEntity());
            if (num > 0)
                return Content("<script>alert('编辑成功');window.parent.location='UserList'</script>");

            return Content("<script>alert('编辑失败');window.history.back();</script>");
        }
        public ActionResult DELUser(int? ID)
        {
            int num = _userService.Delete(ID ?? 0);
            if (num > 0)
            {
                return Content("<script>window.location='UserList'</script>");
            }
            return Content("<script>window.history.back();</script>");
        }
        public ActionResult EDITPWD(int ID)
        {
            int num = _userService.Update(new Users() { ID = ID, UserPassword = SourceIO.MD5("111111") });
            if (num > 0)
            {
                return Content("<script>alert('密码重置成功');window.location='UserList'</script>");
            }
            return Content("<script>alert('删除重置失败');window.history.back();</script>");
        }

        #endregion

        #region LimitOperate
        private List<Limit> listItem = null;
        private List<Functions> listFun = null;
        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public ActionResult LimitOperate(int UID)
        {
            listItem = _limitService.GetOperate(UID);//查询该用户的权限
            string operate = string.Empty;
            operate += "<script type=\"text/javascript\">var setting = {check: {enable: true},data: {simpleData: {enable: true}}};var zNodes =[";
            listFun = _functionsService.GetAll();
            foreach (var item in listFun.FindAll(s => s.ParentID == 0))//循环一级节点
            {
                operate += "{ id:" + item.ID + ", pId:0, name:\"" + item.FName + "\", open:true,nocheck:true},";
                operate += GetChild(item.ID.Value);
            }
            if (listFun != null) //有功能项 去除最后一个逗号
                operate = operate.Substring(0, operate.Length - 1);
            operate += "];$(document).ready(function(){$.fn.zTree.init($(\"#treeDemo\"), setting, zNodes);});</script>";

            ViewData["uid"] = UID;
            ViewData["tree"] = operate;
            return View();
        }
        /// <summary>
        /// 查找下级节点
        /// </summary>
        /// <returns></returns>
        private string GetChild(int FID)
        {
            string child = string.Empty;
            var items = listFun.Where(a => a.ParentID == FID);
            foreach (var item in items)
            {
                child += "{ id:" + item.ID + ", pId:" + item.ParentID + ", name:\"" + item.FName + "\", open:true " + GetChecked(item.ID.Value) + "},";
            }
            return child;
        }
        /// <summary>
        /// 判断是否有权限 有复选框 勾选
        /// </summary>
        /// <param name="FID"></param>
        /// <returns></returns>
        private string GetChecked(int FID)
        {
            if (listItem != null)
            {
                var value = listItem.FirstOrDefault(a => a.FID == FID);
                if (value != null)
                    return ",checked:true";
            }
           return string.Empty;
        }
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="FID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetOperate()
        {
            int UID = int.Parse(Request["UID"]);
            string FID = Request["FID"];
            List<Limit> list = new List<Limit>();
            FID = FID.Substring(0, FID.Length - 1);
            string[] items = FID.Split(',');

            foreach (var item in items)
            {
                list.Add(new Limit() { UID = UID, FID = int.Parse(item), Operate = string.Empty });
            }

            _limitService.DeleteUID(UID);//删除原有权限
            int num = _limitService.Insert(list);//重新添加权限

            if (num > 0)
                return Json("success");
            return Json("fail");
        }
        #endregion
    }
}
