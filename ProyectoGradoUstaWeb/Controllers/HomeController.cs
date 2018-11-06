using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            ViewBag.NombreNegocio = ConfigurationManager.AppSettings.Get("NombreNegocio");
            return View();            
        }
    }
}