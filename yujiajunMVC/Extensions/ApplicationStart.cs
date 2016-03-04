using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Configuration;
using System.Web.Mvc;

namespace yujiajunMVC
{
    public class ApplicationStart
    {
        private static string dataType = ConfigurationManager.AppSettings["dataBaseType"].ToString().ToLower();
        public static void Run()
        {
            var builder = new ContainerBuilder();
            
            var data = Assembly.Load("Service");
            switch (dataType)
            {
                case "sqlserver":
                    builder.RegisterAssemblyTypes(data)
                        .Where(a => a.FullName.Contains("Sqlserver")).AsImplementedInterfaces();
                    break;
                case "oledb":
                    builder.RegisterAssemblyTypes(data)
                        .Where(a => a.FullName.Contains("OleDb")).AsImplementedInterfaces();
                    break;
                default:
                    builder.RegisterAssemblyTypes(data)
                        .Where(a => a.FullName.Contains("Sqlserver")).AsImplementedInterfaces();
                    break;
            }
            builder.RegisterType<CompanyExtensions>();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var contain = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(contain));
        }
    }
}