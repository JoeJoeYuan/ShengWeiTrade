using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using yujiajunMVC.Extensions;

namespace yujiajunMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.Add("fdasfdsa", new Route("{controller}/{action}/{time}/{id}.html", new RouteValueDictionary(new { controller = "Home", action = "Index" }), new RouteValueDictionary(new { id = "^[\\d]+$" }), new MvcRouteHandler()));
            routes.MapRoute(
                "Default1", // Route name
                "{controller}/{action}/{time}/{id}.html", // URL with parameters
                new { controller = "Home", action = "Index" }, // Parameter defaults
                new { id = "^[\\d]+$" }
            );
            routes.MapRoute(
                "Default2", // Route name
                "{controller}/{action}.html", // URL with parameters
                new { controller = "Home", action = "Index"} // Parameter defaults
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ApplicationStart.Run();
            AutoMapperModel.Mapper();
            GlobalFilters.Filters.Add(new ApplicationError());
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}