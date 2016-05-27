using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace yujiajunMVC.Areas.Back.Models
{
    public class UserModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("用户名")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        public string UserPassword { get; set; }

        [DisplayName("是否管理员"), UIHint("Lock")]
        public int IsAdmin { get; set; }

        [DisplayName("是否锁定"), UIHint("Lock")]
        public int IsLock { get; set; }
    }
    public class UserLogin
    {
        public string LoginName { get; set; }
        public string LoginPassWord { get; set; }
        public string Validate { get; set; }
    }
    public class UserPassWordModel
    {
        [DisplayName("原密码")]
        public string Pwd { get; set; }

        [DisplayName("新密码")]
        public string PassWord { get; set; }

        [DisplayName("确认密码")]
        public string Pass { get; set; }
    }
}