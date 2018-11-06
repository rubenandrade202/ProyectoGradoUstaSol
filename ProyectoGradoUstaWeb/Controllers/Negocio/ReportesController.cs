using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Negocio
{
    public class ReportesController : Controller
    {
        #region [FIELDS]
        ClienteBl clienteBl;
        CreditosBl creditosBl;
        ReporteBl reporteBl;
        InventarioBl inventarioBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            Session["FechasFilterReporteVentas"] = null;
            Session["FechasFilterReporteMovimientoCaja"] = null;
            var model = new ReportesMainPageVm()
            {
                Gm_Creditos = GmCreditos(),
                Gm_LogCreditos = GmLogCreditos(),
                Gm_MovimientoCaja = GmMovimientoCaja(),
                Gm_VentasGral = GmVentasProductos(),
                FechaDesdeMovimientoCaja = Controls.GetDatePickerModel("dpkDesdeMovimientoCaja", true),
                FechaHastaMovimientoCaja = Controls.GetDatePickerModel("dpkHastaMovimientoCaja", true),
                FechaDesdeVentas = Controls.GetDatePickerModel("dpkDesdeVentas", true),
                FechaHastaVentas = Controls.GetDatePickerModel("dpkHastaVentas", true)                              
            };
            return View(model);            
        }

        [HttpPost]
        public JsonResult SetFechasFilterMovimientoCaja(DateTime fechaInicial, DateTime fechaFinal)
        {
            var rp = new ResponseBasicVm();
            if(fechaInicial == default(DateTime) || fechaFinal == default(DateTime))
            {
                Session["FechasFilterReporteMovimientoCaja"] = null;
                rp.Success = false;
                rp.MessageBad.Add("No se han recibido las fechas correctamente");
            }
            else
            {
                Session["FechasFilterReporteMovimientoCaja"] = new Dictionary<DateTime, DateTime> {
                    { fechaInicial, fechaFinal }
                };
                rp.Success = true;
                rp.MessageBad.Add("Fechas seteadas en servidor");
            }
            return Json(rp);
            
        }

        [HttpPost]
        public JsonResult SetFechasFilterReporteventas(DateTime fechaInicial, DateTime fechaFinal)
        {
            var rp = new ResponseBasicVm();
            if (fechaInicial == default(DateTime) || fechaFinal == default(DateTime))
            {
                Session["FechasFilterReporteVentas"] = null;
                rp.Success = false;
                rp.MessageBad.Add("No se han recibido las fechas correctamente");
            }
            else
            {
                Session["FechasFilterReporteVentas"] = new Dictionary<DateTime, DateTime> {
                    { fechaInicial, fechaFinal }
                };
                rp.Success = true;
                rp.MessageBad.Add("Fechas seteadas en servidor");
            }
            return Json(rp);

        }

        [HttpPost]
        public JsonResult ClearFechasFilterMovimientoCaja()
        {
            var rp = new ResponseBasicVm();
            Session["FechasFilterReporteMovimientoCaja"] = null;
            rp.Success = true;
            rp.MessageOk.Add("Fechas seteadas en servidor");            
            return Json(rp);

        }

        [HttpPost]
        public JsonResult ClearFechasFilterReporteventas()
        {
            var rp = new ResponseBasicVm();
            Session["FechasFilterReporteVentas"] = null;
            rp.Success = true;
            rp.MessageOk.Add("Fechas seteadas en servidor");
            return Json(rp);            
        }

        [HttpPost]
        public JsonResult GetTotalValorInventario()
        {
            var rp = new ResponseBasicVm();
            inventarioBl = new InventarioBl();
            rp.Data = inventarioBl.GetValorTotalInventario();                   
            rp.Success = true;
            rp.MessageOk.Add("Valor entregado correctamente");
            return Json(rp);
        }
        #endregion

        #region [IG]
        private GridModel GmCreditos()
        {
            var gm = new GridModel().NoCrud_Summaries(5);
            gm.ID = "gmCreditos";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridCreditos");
            gm.Columns.Add(new GridColumn() { Key = "Id", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { HeaderText = "Nombre", Key = "Nombre", DataType = "string", Width = "30%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Telefono", Key = "Telefono", DataType = "string", Width = "15%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cupo Asignado", Key = "CupoAsignado", DataType = "number", Width = "20%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cupo Empleado", Key = "CupoEmpleado", DataType = "number", Width = "20%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Fecha Modificado", Key = "FechaModificado", DataType = "date", Format = "d MMM yyyy h:mm tt", Width = "15%" });

            var ftSum = gm.Features.Where(x => x.Name == "Summaries").FirstOrDefault() as GridSummaries;
            if (ftSum != null)
            {
                ftSum.Type = OpType.Remote;         
                ftSum.ColumnSettings = new List<ColumnSummariesSetting>()
                {
                    new ColumnSummariesSetting {ColumnKey = "Id", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Nombre", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Telefono", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "FechaModificado", AllowSummaries = false},                    
                    new ColumnSummariesSetting {ColumnKey = "CupoAsignado", AllowSummaries = true,
                     Operands = new List<SummaryOperand>()
                     {
                        new SummaryOperand { RowDisplayLabel="Avg",Type= SummaryFunction.Avg,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Count",Type= SummaryFunction.Count,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Min",Type= SummaryFunction.Min,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Sum",Type= SummaryFunction.Sum,  Active=true}
                     }},
                    new ColumnSummariesSetting {ColumnKey = "CupoEmpleado", AllowSummaries = true,
                     Operands = new List<SummaryOperand>()
                     {
                        new SummaryOperand { RowDisplayLabel="Avg",Type= SummaryFunction.Avg,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Count",Type= SummaryFunction.Count,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Min",Type= SummaryFunction.Min,  Active=false},
                        new SummaryOperand { RowDisplayLabel="Sum",Type= SummaryFunction.Sum,  Active=true}
                     }},
                };
            }
            return gm;           
        }

        private GridModel GmLogCreditos()
        {
            var gm = new GridModel().NoCrud(5);
            gm.ID = "gmLogCreditos";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridLogCreditos");
            gm.Columns.Add(new GridColumn() { Key = "Id", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { HeaderText = "Valor", Key = "Valor", DataType = "number", Width = "25%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cliente", Key = "Cliente", DataType = "string", Width = "25%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Vendedor", Key = "Vendedor", DataType = "string", Width = "25%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Fecha Registro", Key = "FechaRegistro", DataType = "date", Format = "d MMM yyyy h:mm tt", Width = "25%" });
            return gm;         
        }
        
        private GridModel GmMovimientoCaja()
        {
            var gm = new GridModel().NoCrud_Summaries(10);
            gm.ID = "gmMovimientosCaja";
            //gm.PrimaryKey = "NombreMovimiento";
            gm.DataSourceUrl = Url.Action("HandlerGridMovCaja");            
            gm.Columns.Add(new GridColumn() { HeaderText = "Tipo Movimiento", Key = "NombreMovimiento", DataType = "string", Width = "25%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Fecha Movimiento", Key = "FechaMovimiento", DataType = "date", Format = "d MMM yyyy h:mm tt", Width = "25%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Total", Key = "Total", DataType = "number", Width = "25%" });
            var ftSum = gm.Features.Where(x => x.Name == "Summaries").FirstOrDefault() as GridSummaries;
            if (ftSum != null)
            {
                ftSum.Type = OpType.Remote;
                ftSum.ColumnSettings = new List<ColumnSummariesSetting>()
                {
                    new ColumnSummariesSetting {ColumnKey = "NombreMovimiento", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "FechaMovimiento", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Total", AllowSummaries = true,
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

        private GridModel GmVentasProductos()
        {
            var gm = new GridModel().NoCrud_Summaries(10);
            gm.ID = "gmReporteVentas";
            //gm.PrimaryKey = "IdProducto";
            gm.DataSourceUrl = Url.Action("HandlerGridReporteVentas");
            gm.Columns.Add(new GridColumn() { Key = "IdProducto", DataType = "int", Hidden = true });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "NombreProducto", DataType = "string", Width = "40%" });
            //gm.Columns.Add(new GridColumn() { HeaderText = "Proveedor", Key = "ProveedorNombre", DataType = "string", Width = "20%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cantidad Vendida", Key = "Cantidad", DataType = "number", Width = "10%" });            
            gm.Columns.Add(new GridColumn() { HeaderText = "Cliente", Key = "ClienteNombre", DataType = "string", Width = "15%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Vendedor", Key = "VendedorNombre", DataType = "string", Width = "15%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Fecha Registro", Key = "FechaRegistro", DataType="date", Format="d MMM yyyy h:mm tt", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Total", Key = "Total", DataType = "number", Width = "10%" });

            
            var ftSum = gm.Features.Where(x => x.Name == "Summaries").FirstOrDefault() as GridSummaries;
            if (ftSum != null)
            {
                ftSum.Type = OpType.Remote;
                ftSum.ColumnSettings = new List<ColumnSummariesSetting>()
                {
                    new ColumnSummariesSetting {ColumnKey = "IdProducto", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "NombreProducto", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "ProveedorNombre", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Cantidad", AllowSummaries = true,
                        Operands = new List<SummaryOperand>()
                         {
                            new SummaryOperand { RowDisplayLabel="Avg",Type= SummaryFunction.Avg,  Active=false},
                            new SummaryOperand { RowDisplayLabel="Count",Type= SummaryFunction.Count,  Active=false},
                            new SummaryOperand { RowDisplayLabel="Min",Type= SummaryFunction.Min,  Active=false},
                            new SummaryOperand { RowDisplayLabel="Sum",Type= SummaryFunction.Sum,  Active=true}
                         }
                    },
                    new ColumnSummariesSetting {ColumnKey = "ClienteNombre", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "VendedorNombre", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "FechaRegistro", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Total", AllowSummaries = true,
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
        public ActionResult HandlerGridCreditos()
        {
            return View(new ClienteBl().Get().Where(x => x.CupoEmpleado != 0).AsQueryable());
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridLogCreditos()
        {
            return View(new CreditosBl().GetLogCreditos());
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridMovCaja()
        {
            var fechaInicial = new DateTime();
            var fechaFinal = new DateTime();
            if(Session["FechasFilterReporteMovimientoCaja"] != null)
            {
                var dictionary = Session["FechasFilterReporteMovimientoCaja"] as Dictionary<DateTime, DateTime>;
                fechaInicial = dictionary.Keys.FirstOrDefault(); 
                fechaFinal = dictionary.Values.FirstOrDefault();
            }
            return View(new ReporteBl().GetReporteMovimiento(fechaInicial, fechaFinal));
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridReporteVentas()
        {
            var fechaInicial = new DateTime();
            var fechaFinal = new DateTime();
            if (Session["FechasFilterReporteVentas"] != null)
            {
                var dictionary = Session["FechasFilterReporteVentas"] as Dictionary<DateTime, DateTime>;
                fechaInicial = dictionary.Keys.FirstOrDefault();
                fechaFinal = dictionary.Values.FirstOrDefault();
            }
            return View(new ReporteBl().GetReporteVentas(fechaInicial, fechaFinal));
        }        
        #endregion

        #region [API]
        #endregion

        #region [Methods]
        #endregion
    }
}