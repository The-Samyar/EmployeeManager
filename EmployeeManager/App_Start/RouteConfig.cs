using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Auth",
                url: "auth/{action}/",
                defaults: new { controller = "Auth", action = "Login" }
            );
            
            routes.MapRoute(
                name: "Manager",
                url: "manager/{action}/",
                defaults: new { controller = "Manager", action = "Home" }
            );
            
            routes.MapRoute(
                name: "Employee",
                url: "Employee/",
                defaults: new { controller = "Manager", action = "Home" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
