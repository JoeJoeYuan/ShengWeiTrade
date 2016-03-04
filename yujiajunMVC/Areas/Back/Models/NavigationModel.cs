using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace yujiajunMVC.Areas.Back.Models
{
    public class NavigationModel
    {
        [ScaffoldColumn(false)]
        public int? ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        [DisplayName("父级"), UIHint("LoadNavigation")]
        public int? ParentID { get; set; }
        /// <summary>
        /// 导航名称
        /// </summary>
        [DisplayName("名称")]
        public string NName { get; set; }
        /// <summary>
        /// 是否在前台显示 0否 1是
        /// </summary>
        [DisplayName("是否启用"), UIHint("Lock")]
        public int? IsLock { get; set; }
        /// <summary>
        /// 连接路径
        /// </summary>
        [DisplayName("连接路径")]
        public string NPath { get; set; }
    }
}