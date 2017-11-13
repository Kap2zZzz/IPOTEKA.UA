using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IPOTEKA.UA
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapRoute("MyRoute", "{action}", new { controller = "Home", action = "Index" });
            //routes.MapRoute("Admin", "{controller}/{action}", new { controller = "Admin" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

//            routes.MapRoute(
//name: "Default3",
//url: "",
//defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
//);


//            routes.MapRoute(
//    name: "Default1",
//    url: "{action}/{id}",
//    defaults: new { controller = "Home", id = UrlParameter.Optional }
//);

//            routes.MapRoute(
//name: "Default2",
//url: "{action}",
//defaults: new { controller = "Admin", action = "Admin", id = UrlParameter.Optional }
//);

         //   routes.MapRoute("Admin", "{controller}/User/{action}", new { controller = "Admin", action = "Index", id = UrlParameter.Optional });


            //            routes.MapRoute(
            //    name: "Default2",
            //    url: "{controller}/{action}/{id}"
            //    //defaults: new { controller = "Admin", action = "Admin", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );





            //routes.MapRoute("About", "About", new { controller = "Home", action = "About", id = UrlParameter.Optional });

            //routes.MapRoute("Admin", "Admin/Index", new { controller = "Admin", action = "Index", id = UrlParameter.Optional });



        }
    }
}