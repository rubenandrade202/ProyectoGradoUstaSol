using ProyectoGradoUstaBus;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemporalMigracion;

namespace ProyectoGradoUstaWeb.Controllers
{
    public class MigracionTmpController : Controller
    {
        // GET: MigracionTmp
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult MigrarProductos()
        {
            var blProductos = new ProductoBl();
            var ctxOld = new POS_V1Entities();
            var data = (from productosOld in ctxOld.Productos
                        select new ProductoAddVm
                        {                            
                            Nombre = productosOld.Nombre,
                            CantidadActual = 0,
                            CantidadUmbral = 0,
                            IdUbicacionNegocio = null,
                            IdUbicacionStock = null,
                            Precio = productosOld.Precio,                           
                            CodigosBarras = (from codigosOld in ctxOld.CodigosDeBarraProductos
                                             where codigosOld.IdProducto == productosOld.Id
                                             select codigosOld.Codigo).ToList()                                                       
                        }).ToList();
            var rp = blProductos.Add(data, 1);
            return null;        
        }
    }
}