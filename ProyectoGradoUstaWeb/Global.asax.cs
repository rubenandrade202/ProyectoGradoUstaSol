using ProyectoGradoUstaWeb.Utility.Gral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProyectoGradoUstaWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());//Para garantizar la etiqueta [Authorize] a nivel de todos los controladores, comentar para pruebas
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MemberShipConfig.Initialize();//Para inicializar la utilidad de Membership                          
        }


        protected  void Application_Error()
        {
            //log error del sistema

        }
  }
}

