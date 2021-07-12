using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace oxinhem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
              name: "DefaultWithCulture",
              url: "{culture}/{controller}/{action}/{id}",
              defaults: new { culture = "se", controller = "Home", action = "Index", id = UrlParameter.Optional },
              constraints: new { culture = "[a-z]{2}" }
              );// or maybe: "[a-z]{2}-[a-z]{2}

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { culture = "se", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
