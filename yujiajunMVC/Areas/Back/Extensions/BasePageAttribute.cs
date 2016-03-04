using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace yujiajunMVC
{
    
    public class BasePageAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies["user"] == null)//验证后台未登陆用户
                filterContext.Result = new RedirectResult("/Back/Others/Login");
        }
    }
}