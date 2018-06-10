using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "First", action = "MainPage", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Views",
                url: "{controller}/{action}/{year}/{month}/{file}",
                defaults: new { controller = "First", action = "ViewPage", year = UrlParameter.Optional, month = UrlParameter.Optional,
                file = UrlParameter.Optional}
            );
        }
    }
}
