using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sehel_MVCGiris
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("hakkimdaRoute","hakkimda",new {controller="Home",action="About" }, namespaces: new string[] { "Sehel_MVCGiris.Controllers" });
            routes.MapRoute("projectsRoute","projelerim",new {controller="Home",action="Projelerim" }, namespaces: new string[] { "Sehel_MVCGiris.Controllers" });
            routes.MapRoute("anasayfaRoute", "anasayfa", new { controller = "Home", action = "Index" }, namespaces: new string[] { "Sehel_MVCGiris.Controllers" });
            routes.MapRoute("iletisimRoute", "iletisim", new { controller = "Home", action = "Contact" }, namespaces: new string[] { "Sehel_MVCGiris.Controllers" });
            routes.MapRoute("kvkkRoute", "kvkk", new { controller = "Home", action = "Kvkk" }, namespaces: new string[] { "Sehel_MVCGiris.Controllers" });
            routes.MapRoute("privacyRoute", "privacyPolicy()", new { controller = "Home", action = "PrivacyPolicy" },
                 namespaces: new string[] { "Sehel_MVCGiris.Controllers" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] {"Sehel_MVCGiris.Controllers"}

            );
        }
    }
}
