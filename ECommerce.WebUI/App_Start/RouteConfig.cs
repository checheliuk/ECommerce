using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "sitemap",
                url: "sitemap.xml",
                defaults: new { controller = "Post", action = "Sitemap" }
            );

            routes.MapRoute(
               name: "Catalog-Type-Sex-Category-Page",
               url: "{controller}/{action}/{arg1}/{arg2}/{arg3}/{arg4}/{arg5}",
               defaults: new { controller = "Catalog", action = "Grid", arg1 = UrlParameter.Optional, arg2 = UrlParameter.Optional, arg3 = UrlParameter.Optional, arg4 = UrlParameter.Optional, arg5 = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Catalog",
                url: "{controller}/{action}/{type}/{sex}/{category}",
                defaults: new { controller = "Catalog", action = "Grid", type = UrlParameter.Optional, sex = UrlParameter.Optional, category = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{arg1}",
                defaults: new { controller = "Catalog", action = "Grid", arg1 = UrlParameter.Optional }
            );
        }
    }
}
