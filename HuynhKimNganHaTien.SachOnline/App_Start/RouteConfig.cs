using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HuynhKimNganHaTien.SachOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                            name: "Trang tin",
                            url: "{metatitle}",
                            defaults: new { controller = "SachOnline", action = "TrangTin", metatitle = UrlParameter.Optional },
                            namespaces: new string[] { "SachOnline.Controllers" }
                        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SachOnline", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "SachOnline.Controllers" }
            );
        }
    }
}
