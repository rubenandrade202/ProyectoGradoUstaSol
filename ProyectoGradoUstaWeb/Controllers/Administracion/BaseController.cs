using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers
{
    public class BaseController : Controller
    {        
        #region [PREVIOUS]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Pruebas
            //Session["UserIdIdioma"] = 2;
            //Session["UserId"] = 1;
            //Valida si es necesario cargar el layout de la pagina o si la pagina se esta solicitando por medio de el metodo controlado JS
            if (filterContext.HttpContext.Request.QueryString["thereIsL"] == null)
            {
                ViewBag.LayoutSet = true;
            }
        }
        #endregion
    }
}