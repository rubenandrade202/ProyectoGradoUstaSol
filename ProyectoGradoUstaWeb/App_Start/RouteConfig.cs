using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoGradoUstaWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Security", action = "LogIn", id = UrlParameter.Optional }
                //defaults: new { controller = "Producto", action = "Index", id = UrlParameter.Optional }//Para realizar pruebas especificas e inice en la vista deseada saltandose el login
            );
        }
    }
}
