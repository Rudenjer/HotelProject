using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute("MyRoute", "{controller}/{action}",
            //    namespaces: new[] { "HotelProjects.Controllers" });
            //routes.MapRoute("MyOtherRoute", "App/{action}", new { controller = "Home" },
            //    namespaces: new[] { "HotelProject.Controllers" });

            routes.MapRoute(namespaces: new[] { "HotelProject.Controllers" },
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }


            );

            //routes.MapRoute("MyOtherRoute", "App/{action}", new { controller = "Home" },
            //    namespaces: new[] { "HotelProject.Controllers" });
        }
    }
}
