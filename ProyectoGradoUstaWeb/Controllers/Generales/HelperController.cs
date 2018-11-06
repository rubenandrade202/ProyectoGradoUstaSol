using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Generales
{
    public class HelperController : Controller
    {
        #region [FIELDS]
        static GeneralDomainBl gralBlDomain;
        #endregion

        #region [HandlersRemote]
        /// <summary>
        /// Demo de handler remoto para combo IG
        /// </summary>
        /// <returns></returns>
        public ActionResult HandlerSomeRemoteCmbTest()
        {
            var requesting = HttpContext.Request.QueryString["filter(Value)"];
            if (requesting != null)
            {
                gralBlDomain = gralBlDomain == null ? new GeneralDomainBl() : gralBlDomain;
                var token = requesting.Replace("startsWith(", string.Empty).Replace(")", string.Empty);
                var data = gralBlDomain.GetPruebaBorrar().Where(x => x.Value.ToLower().Contains(token.ToLower())).Take(10).Select(y => new BasicVm() { Id = (int)y.Id, Value = y.Value.Trim() }).ToList(); ;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult HandlerRemoteProductos()
        {
            var requesting = HttpContext.Request.QueryString["filter(Value)"];
            if (requesting != null)
            {
                gralBlDomain = gralBlDomain == null ? new GeneralDomainBl() : gralBlDomain;
                var token = requesting.Replace("startsWith(", string.Empty).Replace(")", string.Empty);
                var data = gralBlDomain.GetProductos().Where(x => x.Nombre.ToLower().Contains(token.ToLower())).Take(10).Select(y => new BasicDependantVm() { Id = (int)y.Id, Value = y.Nombre.Trim(), IdParent = y.Precio }).ToList(); ;
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


    }
}