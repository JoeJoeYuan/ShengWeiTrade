using System.Web.Mvc;

namespace yujiajunMVC.Areas.Back
{
    public class BackAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Back";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Back_default",
                "Back/{controller}/{action}/{id}",
                new {controller="Others", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
