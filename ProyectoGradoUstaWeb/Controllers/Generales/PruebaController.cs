using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Generales
{
    /// <summary>
    /// Es importante heredar a los controller de el base controller
    /// </summary>
    public class PruebaController : BaseController
    {
        
        /// <summary>
        /// Controller de prueba para visualizar como cargar elementos IG
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var model = new MainDemoPageVm()
            {
                CmbNormal = Controls.GetComboDemo("cmbDemoNormal", false, true),
                CmbRemoto = Controls.CmbRemoteGeneric("cmbDemoRemote", false, true, Url.Action("HandlerSomeRemoteCmbTest", "Helper")),
                DpkPrueba = Controls.GetDatePickerModel("dpkDemo", false)
            };

            return View(model);
        }
	}
}