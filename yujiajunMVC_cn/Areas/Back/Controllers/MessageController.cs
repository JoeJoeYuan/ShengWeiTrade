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
    [ValidateInput(false)]
    public class MessageController : Controller
    {
        //
        // GET: /Back/Message/
        private readonly IMessagesService _messageService;

        public MessageController(IMessagesService messageService)
        {
            _messageService = messageService;
        }
        public ActionResult Index(int? Id=1)
        {
            int pageIndex = Id.Value;//当前页数
            int pageSize = 20;//每页条数
            var list = _messageService.GetByPage(pageSize, (pageIndex - 1) * pageSize, "ID DESC", null);
            return View(new PagedList<Messages>(list, pageIndex, pageSize, _messageService.GetCount(null)));
        }
        public ActionResult DELMessage(int? ID)
        {
            int num = _messageService.Delete(ID ?? 0);
            if (num > 0)
                return Content("<script>alert('删除成功');window.location='Index'</script>");
            return Content("<script>alert('删除失败');window.history.back();</script>");
        }
        public ActionResult MessageDetail(string ID)
        {
            Messages message = null;
            if (!string.IsNullOrWhiteSpace(ID))
            {
                message = _messageService.GetById(int.Parse(ID));
            }
            return View(message);
        }
        public ActionResult MessageEDIT(string ID)
        {
            Messages message = null;
            if (!string.IsNullOrWhiteSpace(ID))
            {
                message = _messageService.GetById(int.Parse(ID));
                if (message.IsAudit == 1)
                {
                    ViewBag.disabled = "disabled";
                    ViewBag.value = "已通过审核";
                }
                else
                    ViewBag.value = "提 交";
            }
            return View(message);
        }
        public ActionResult MessageUpdate(Messages message)
        {
            int num = _messageService.Update(new Messages() { ID = message.ID, IsAudit = 1 });
           if (num > 0)
               return Content("<script>alert('审核成功');window.location='Index'</script>");
           return Content("<script>alert('审核失败');window.history.back();</script>");
        }
    }
}
