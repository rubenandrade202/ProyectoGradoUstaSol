using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus.Negocio;
using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Negocio
{
    public class CajaController : Controller
    {
        #region [FIELDS]
        CajaBl cajaBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            var cmbMovimiento = Controls.CmbTipoMovimiento("cmbTipoMovimiento", true, false);            
            cmbMovimiento.AddClientEvent("selectionChanged", "cmbTipoMovimientoSelectionchanged");
            var cmbDevolucion = Controls.CmbRemoteGeneric("cmbDevolucion", false, false, Url.Action("HandlerRemoteProductos", "Helper"));
            cmbDevolucion.Width = "450px";
            cmbDevolucion.Height = "25px";
            cmbDevolucion.AddClientEvent("dropDownClosed", "dropDownClosedCmbDevolucion");
            cmbDevolucion.PreventSubmitOnEnter = false;

            var model = new CajaMainPageVm()
            {
                CmbDevolucion = cmbDevolucion,
                GmCaja = GetGmCaja(),
                CmbMovimiento = cmbMovimiento,
                FechaInicioFlt = null,
                FechaFinFlt = null
            };
            return View(model);
        }
        #endregion

        #region [API]
        [HttpPost]
        public JsonResult RegistrarMovimiento(BasicIntVm candidateRecord)
        {
            var rp = new ResponseBasicVm();
            if (candidateRecord != null && candidateRecord.Id > 0)
            {
                cajaBl = new CajaBl();
                rp = cajaBl.RegistrarMovimiento(candidateRecord);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("Se han enviado datos erroneos al servidor, contacte al administrador.");
            }
            return Json(rp);
        }
        [HttpPost]
        public JsonResult DevolverProducto(int idProducto)
        {
            var rp = new ResponseBasicVm();
            if(idProducto > 0)
            {
                cajaBl = new CajaBl();
                rp = cajaBl.RegistrarDevolucion(idProducto);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("Se han enviado datos erroneos al servidor, contacte al administrador.");
            }
            return Json(rp);
        }
        #endregion

        #region [IG]
        public GridModel GetGmCaja()
        {
            var gm = new GridModel().NoCrud_Summaries();
            gm.ID = "gmCaja";          
            gm.DataSourceUrl = Url.Action("HdlGmCaja");            
            gm.Columns.Add(new GridColumn() { Key = "IdTipoMovimiento", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { HeaderText="Tipo Movimiento",  Key = "NombreTipoMovimiento", DataType = "string", Width="50%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Valor", Format = "currency", Key = "Valor", DataType = "number", Width = "50%" });
            
            var ftSum = gm.Features.Where(x => x.Name == "Summaries").FirstOrDefault() as GridSummaries;
            if (ftSum != null)
            {
                ftSum.Type = OpType.Remote;         
                ftSum.ColumnSettings = new List<ColumnSummariesSetting>()
                {
                    new ColumnSummariesSetting {ColumnKey = "NombreTipoMovimiento", AllowSummaries = false},                  
                    new ColumnSummariesSetting {ColumnKey = "Valor", AllowSummaries = true,
                     Operands = new List<SummaryOperand>()
                     {
                        new SummaryOperand { RowDisplayLabel="Avg",Type= SummaryFunction.Avg,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Count",Type= SummaryFunction.Count,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Min",Type= SummaryFunction.Min,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Sum",Type= SummaryFunction.Sum,  Active=true}
                     }}
                };
            }             
            return gm;            
        }

        [GridDataSourceAction]
        public ActionResult HdlGmCaja()
        {
            return View(new CajaBl().Get());
        }
        #endregion
    }
}