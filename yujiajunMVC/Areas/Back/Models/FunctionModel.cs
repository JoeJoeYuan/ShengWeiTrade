using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace yujiajunMVC.Areas.Back.Models
{
    public class FunctionModel
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("类别"), UIHint("LoadFunctionType")]
        public int? ParentID { get; set; }

        [DisplayName("名称")]
        public string FName { get; set; }

        [DisplayName("路径")]
        public string FPath { get; set; }

        [DisplayName("是否启用"),UIHint("Lock")]
        public int? IsLock { get; set; }
    }
}