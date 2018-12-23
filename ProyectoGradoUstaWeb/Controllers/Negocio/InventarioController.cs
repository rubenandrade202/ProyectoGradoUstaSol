using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Negocio
{
    public class InventarioController : BaseController
    {
        #region [FIELDS]
        ProveedorBl proveedorBl;
        InventarioBl inventarioBl;
        #endregion

        #region [MVC]
        /// <summary>
        /// Pagina principal programar pedidos y visitas
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {            
            Session["filterUmbral"] = false;
            Session["filterProviderProductosPedidos"] = new List<int>();
            inventarioBl = new InventarioBl();
            inventarioBl.DeleteOldProductosAPedir();            
            var providersProyection = GetDataProviders();
            var cmbProveedores = Controls.CmbProveedores("cmbProveedores", false, true);
            cmbProveedores.Width = "100px";
            var cmbProveedoresProPedidosFlt = Controls.CmbProveedores("cmbProveedoresProductosPedFlt", false, true);
            cmbProveedoresProPedidosFlt.Width = "100px";
            cmbProveedoresProPedidosFlt.AddClientEvent("selectionChanged", "cmbProveedoresFltSelectionChanged");
            var model = new InventarioMainPageVm()
            {
                GridProveedoresProductosFaltantes = GmProveedoresProductosFaltantes(),
                GridproductosAPedir = GmProductosAPedir(),                
                ProveedoresProyection = providersProyection,
                CmbProveedores = cmbProveedores,
                CmbProveedoresProductosPedidosFlt = cmbProveedoresProPedidosFlt
            };
            return View(model);
        }

        public ActionResult RealizarInventario()
        {
            var cmbProductoSearch = Controls.CmbRemoteGeneric("cmbSearchProduct", false, false, Url.Action("HandlerRemoteProductos", "Helper"));
            cmbProductoSearch.AddClientEvent("dropDownClosed", "dropDownClosedCmbSearch");
            cmbProductoSearch.PreventSubmitOnEnter = false;

            var cmbUbicaciones = Controls.CmbUbicacionesNegocio("cmbUbicacionNeogcio", true, false);
            cmbUbicaciones.AddClientEvent("selectionChanged", "cmbUbicacionesSelectionChanged");
            
            var model = new RealizarInventarioPageVm()
            {
                CmbUbicacionesNegocio = cmbUbicaciones,
                CmbProductosSearch = cmbProductoSearch,
                GridProductosAInventario = GmProductosAInventario()
            };
            return View(model);
        }

        /// <summary>
        /// Pagina para ingresar los items recibidos a inventario
        /// </summary>
        /// <returns></returns>
        public ActionResult IngresarPedidosSistema()
        {
            Session["ProviderSelected"] = null;
            Session["DateSelected"] = null;
            var cmbProveedores = Controls.CmbProveedores("cmbProveedores", true, false);
            cmbProveedores.AddClientEvent("selectionChanged", "cmbProveedoresSelectionchanged");
            var cmbProductoSearch = Controls.CmbRemoteGeneric("cmbSearchProduct", false, false, Url.Action("HandlerRemoteProductos", "Helper"));
            cmbProductoSearch.AddClientEvent("dropDownClosed", "dropDownClosedCmbSearch");
            cmbProductoSearch.PreventSubmitOnEnter = false;
            var dpkFecha = Controls.GetDatePickerModel("dpkFechaPedido", true);
            dpkFecha.AddClientEvent("valueChanged", "dpkFechaValueChanged");
            var model = new InventarioIngresarPedidosPageVm()
            {
                CmbProveedores = cmbProveedores,
                CmbProductosSearch = cmbProductoSearch,
                GridItemsPedidos = GmProductosBufferAInventario(),
                FechaPedido = dpkFecha
            };
            return View(model);
        }

        /// <summary>
        /// Vista parcial que entrega el resumen de visitas y pedidos del día
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PedidosVisitasPv()
        {
            inventarioBl = new InventarioBl();
            inventarioBl.DeleteOldProductosAPedir();
            return PartialView(inventarioBl.GetInfoPedidos());
        }

        /// <summary>
        /// Filtro para la selección del umbral, por sistema o por valor usuario
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetFilter(bool value)
        {
            Session["filterUmbral"] = value;
            var rp = new ResponseBasicVm();
            rp.Success = true;
            rp.MessageOk.Add("Valor Configurado en server");
            return Json(rp);
        }

        /// <summary>
        /// Filtro para ver unicamente pedidos programados de un proveedor en particular
        /// </summary>
        /// <param name="idProveedores"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetFilterProviderProductosPedidos(List<int> idProveedores)
        {            
            Session["filterProviderProductosPedidos"] = (idProveedores == null || idProveedores.Count == 0) ? new List<int>() : idProveedores;           
            var rp = new ResponseBasicVm();
            rp.Success = true;
            rp.MessageOk.Add("Valor Configurado en server");
            return Json(rp);
        }

        /// <summary>
        /// Filtro para la pagina de ingreso de pedidos al inventario. Se hace por proveedor
        /// </summary>
        /// <param name="idProvider"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetProviderAndDateSelected(int idProvider, DateTime fechaReferenciaPedido)
        {
            Session["ProviderSelected"] = idProvider;
            Session["DateSelected"] = new DateTime(fechaReferenciaPedido.Year, fechaReferenciaPedido.Month, fechaReferenciaPedido.Day, 0, 0, 0);
            var rp = new ResponseBasicVm();
            rp.Success = true;
            rp.MessageOk.Add("Valores Configurado en server");
            return Json(rp);
        }        
        #endregion

        #region [IG]  
        private GridModel GmProveedoresProductosFaltantes()
        {
            //var gm = new GridModel().NoCrud_Selection(10, true, true);
            var gm = new GridModel().Crud_Selection(10, true, true);
            gm.ID = "gmProductosFaltantes";
            gm.PrimaryKey = "IdRecord";
            gm.DataSourceUrl = Url.Action("HandlerGridProveedoresFaltantes");
            gm.Columns.Add(new GridColumn() { Key = "IdRecord", DataType = "string", Hidden = true });
            gm.Columns.Add(new GridColumn() { Key = "ProductoId", DataType = "number", Hidden=true });
            gm.Columns.Add(new GridColumn() { Key = "ProveedorId", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key= "ProductoNombre", DataType="string", Width="26%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Proveedor", Key = "ProveedorNombre", DataType = "string", Width = "20%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "V30", Key = "CantidadVendida30Dias", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "V21", Key = "CantidadVendida21Dias", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "V15", Key = "CantidadVendida15Dias", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "V7", Key = "CantidadVendida7Dias", DataType = "number", Width = "6%" }); 
            gm.Columns.Add(new GridColumn() { HeaderText = "U-U", Key = "CantidadUmbral", DataType = "number", Width = "5%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "U-S", Key = "CantidadUmbralSistema", DataType = "number", Width = "5%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Act", Key = "CantidadActual", DataType = "number", Width = "5%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Sts", Key = "Programado", FormatterFunction = "nsGral.FormatterNumberToColor", DataType = "number", Width = "5%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Qty", Key = "QtyAPedir", DataType = "number", Width = "5%" });            
            gm.Columns.Add(new UnboundColumn() { HeaderText = "Go", Key = "Pedir", Template = "<input type='button' value='->' onclick='nsInven.ProgramarPedidoItem(${ProveedorId}, ${ProductoId}, ${Programado}, ${QtyAPedir});' />", Width = "5%" });

            var updFt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if(updFt != null)
            {
                //updFt.AddClientEvent("rowDeleting", "rowDeletingProductosAPedir");
                updFt.AddClientEvent("editRowEnding", "editRowEndingGridProductosFaltantes");
                updFt.EnableAddRow = false;
                updFt.EnableDeleteRow = false;
                updFt.EditMode = GridEditMode.Row;

                var colUpdProductoNombre = new ColumnUpdatingSetting();
                colUpdProductoNombre.ColumnKey = "ProductoNombre";
                colUpdProductoNombre.ReadOnly = true;

                var colUpdProveedorNombre = new ColumnUpdatingSetting();
                colUpdProveedorNombre.ColumnKey = "ProveedorNombre";
                colUpdProveedorNombre.ReadOnly = true;

                var colUpdCantidadVendida30Dias = new ColumnUpdatingSetting();
                colUpdCantidadVendida30Dias.ColumnKey = "CantidadVendida30Dias";
                colUpdCantidadVendida30Dias.ReadOnly = true;

                var colUpdCantidadVendida21Dias = new ColumnUpdatingSetting();
                colUpdCantidadVendida21Dias.ColumnKey = "CantidadVendida21Dias";
                colUpdCantidadVendida21Dias.ReadOnly = true;

                var colUpdCantidadVendida15Dias = new ColumnUpdatingSetting();
                colUpdCantidadVendida15Dias.ColumnKey = "CantidadVendida15Dias";
                colUpdCantidadVendida15Dias.ReadOnly = true;

                var colUpdCantidadVendida7Dias = new ColumnUpdatingSetting();
                colUpdCantidadVendida7Dias.ColumnKey = "CantidadVendida7Dias";
                colUpdCantidadVendida7Dias.ReadOnly = true;

                var colUpdCantidadUmbralSistema = new ColumnUpdatingSetting();
                colUpdCantidadUmbralSistema.ColumnKey = "CantidadUmbralSistema";
                colUpdCantidadUmbralSistema.ReadOnly = true;

                var colUpdPedir = new ColumnUpdatingSetting();
                colUpdPedir.ColumnKey = "Pedir";
                colUpdPedir.ReadOnly = true;

                var colUpdProgramado = new ColumnUpdatingSetting();
                colUpdProgramado.ColumnKey = "Programado";
                colUpdProgramado.ReadOnly = true;

                var colUpdQtyAPedir = new ColumnUpdatingSetting();
                colUpdQtyAPedir.ColumnKey = "QtyAPedir";
                colUpdQtyAPedir.ReadOnly = true;

                var colUpdCantidadUmbral = new ColumnUpdatingSetting();
                colUpdCantidadUmbral.ColumnKey = "CantidadUmbral";
                colUpdCantidadUmbral.NumericEditorOptions.MinValue = 0;
                colUpdCantidadUmbral.NumericEditorOptions.MaxValue = 100;

                var colUpdCantidadActual = new ColumnUpdatingSetting();
                colUpdCantidadActual.ColumnKey = "CantidadActual";
                colUpdCantidadActual.NumericEditorOptions.MinValue = 0;
                colUpdCantidadActual.NumericEditorOptions.MaxValue = 100;

                updFt.ColumnSettings = new List<ColumnUpdatingSetting>()
                {
                    colUpdProductoNombre, colUpdProveedorNombre, colUpdCantidadVendida30Dias, colUpdCantidadVendida15Dias, colUpdCantidadVendida7Dias,
                    colUpdCantidadUmbralSistema, colUpdProgramado, colUpdQtyAPedir, colUpdCantidadUmbral, colUpdCantidadActual, colUpdCantidadVendida21Dias,
                    colUpdPedir
                };
            }

            var fltFt = gm.Features.Where(x => x.Name == "Filtering").FirstOrDefault() as GridFiltering;
            if (fltFt != null)
            {
                fltFt.AddClientEvent("dataFiltered", "GrProgramarProductosDatafiltered");
            }

            return gm;
        }
        private GridModel GmProductosAPedir()
        {
            var gm = new GridModel().Crud_Selection_Summaries(10, true, true);
            var updFt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if(updFt != null)
            {
                updFt.AddClientEvent("rowDeleting", "rowDeletingProductosAPedir");
                updFt.AddClientEvent("editRowEnding", "editRowEndingGridProductosAPedir");
                updFt.EnableAddRow = false;
                updFt.EditMode = GridEditMode.Row;

                var colProductoUpt = new ColumnUpdatingSetting();
                colProductoUpt.ColumnKey = "ProductoNombre";
                colProductoUpt.ReadOnly = true;

                var colProveedorUpt = new ColumnUpdatingSetting();
                colProveedorUpt.ColumnKey = "ProveedorNombre";
                colProveedorUpt.ReadOnly = true;

                var colSubtotalUpt = new ColumnUpdatingSetting();
                colSubtotalUpt.ColumnKey = "Subtotal";
                colSubtotalUpt.ReadOnly = true;

                var colDiaVisitaUpt = new ColumnUpdatingSetting();
                colDiaVisitaUpt.ColumnKey = "DiaVisita";
                colDiaVisitaUpt.ReadOnly = true;   

                var colFechaVisitaUpt = new ColumnUpdatingSetting();
                colFechaVisitaUpt.ColumnKey = "FechaVisita";
                colFechaVisitaUpt.ReadOnly = true;

                var colDiaEntregaUpt = new ColumnUpdatingSetting();
                colDiaEntregaUpt.ColumnKey = "DiaEntrega";
                colDiaEntregaUpt.ReadOnly = true;

                var colFechaEntregaUpt = new ColumnUpdatingSetting();
                colFechaEntregaUpt.ColumnKey = "FechaEntrega";
                colFechaEntregaUpt.ReadOnly = true;

                var colConfirmadoUpt = new ColumnUpdatingSetting();
                colConfirmadoUpt.ColumnKey = "Confirmado";
                colConfirmadoUpt.ReadOnly = true;

                var colQtyUpt = new ColumnUpdatingSetting();
                colQtyUpt.ColumnKey = "Qty";
                colQtyUpt.NumericEditorOptions.MinValue = 1;
                colQtyUpt.NumericEditorOptions.MaxValue = 450;
                colQtyUpt.Required = true;

                updFt.ColumnSettings = new List<ColumnUpdatingSetting>()
                {
                    colProductoUpt, colProveedorUpt, colDiaVisitaUpt, colFechaVisitaUpt, colQtyUpt, colConfirmadoUpt, colFechaEntregaUpt, colDiaEntregaUpt,colSubtotalUpt
                };
            }

            var ftSum = gm.Features.Where(x => x.Name == "Summaries").FirstOrDefault() as GridSummaries;
            if (ftSum != null)
            {
                ftSum.ColumnSettings = new List<ColumnSummariesSetting>()
                    {
                        new ColumnSummariesSetting {ColumnKey = "ProductoNombre", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "ProveedorNombre", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "DiaVisita", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "FechaVisita", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "DiaEntrega", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "FechaEntrega", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "Qty", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "Confirmado", AllowSummaries = false},
                        new ColumnSummariesSetting {ColumnKey = "Subtotal", AllowSummaries = true,
                         Operands = new List<SummaryOperand>()
                         {
                            new SummaryOperand { RowDisplayLabel="Avg",Type= SummaryFunction.Avg,  Active=false},
                            new SummaryOperand { RowDisplayLabel="Count",Type= SummaryFunction.Count,  Active=false},
                            new SummaryOperand { RowDisplayLabel="Min",Type= SummaryFunction.Min,  Active=false},                            
                            new SummaryOperand { RowDisplayLabel="Sum",Type= SummaryFunction.Sum,  Active=true/*, DecimalDisplay = 0*/ }
                         }},
                    };
            }

            var fltFt = gm.Features.Where(x => x.Name == "Filtering").FirstOrDefault() as GridFiltering;
            if (fltFt != null)
            {
                fltFt.AddClientEvent("dataFiltered", "GrProductosAPedirDatafiltered");
            }



            gm.ID = "gmProductosAPedir";
            gm.PrimaryKey = "IdRecord";
            gm.DataSourceUrl = Url.Action("HandlerProductosAPedir");
            gm.Columns.Add(new GridColumn() { Key = "IdRecord", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { Key = "IdProveedor", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { Key = "IdProducto", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width = "21%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Qty", Key = "Qty", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Proveedor", Key = "ProveedorNombre", DataType = "string", Width = "14%" });            
            gm.Columns.Add(new GridColumn() { HeaderText = "DÍA VIS", Key = "DiaVisita", DataType = "string", Width = "11%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "FEC VIS", Key = "FechaVisita", Format = "d MMM yyyy", DataType = "date", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "DÍA ENT", Key = "DiaEntrega", DataType = "string", Width = "11%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "FEC ENT", Key = "FechaEntrega", Format = "d MMM yyyy", DataType = "date", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "CN", Key = "Confirmado", FormatterFunction = "nsGral.FormatterBoolToColor", DataType = "bool", Width = "4%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Subtotal", Key = "Subtotal", DataType = "number", Width = "13%" });
            return gm;
        }            
        private GridModel GmProductosBufferAInventario()
        {
            var gm = new GridModel().Crud_Selection(10, true, true);
            gm.ID = "gmProductosBufferAInventario";
            gm.PrimaryKey = "Id";
            gm.AutoCommit = true;
            gm.DataSource = new InventarioBl().GetProductosBufferAInventario(-1, new DateTime(1990,1,1));
            gm.DataSourceUrl = Url.Action("HandlerGridProductosBufferAinventario");
            gm.Columns.Add(new GridColumn() { Key = "Id", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { Key = "IdProveedor", DataType = "number", Hidden = true });
            gm.Columns.Add(new GridColumn() { Key = "IdProducto", DataType = "number", Hidden = true });
            //los informativos
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width = "21%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Qty Pedida", Key = "QtyPedida", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Qty Actual", Key = "QtyActual", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Qty A Ingresar", Key = "QtyRealAIngresar", DataType = "number", Width = "6%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Precio A Dejar", Key = "PrecioAsignado", DataType = "number", Width = "6%" });
            //--Los importantes
            gm.Columns.Add(new GridColumn() { HeaderText = "Costo Item", Key = "CostoItem", DataType = "number", Width = "6%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "% Ganancia Actual", Key="GananciaActual", Formula = "calcularGananciaActual", Width = "6%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "% Precio al 20%", Key = "PrecioAl20", Formula = "calcularPrecioAl20", Width = "6%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "% Precio al 25%", Key = "PrecioAl25", Formula = "calcularPrecioAl25", Width = "6%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "% Precio al 30%", Key = "PrecioAl30", Formula = "calcularPrecioAl30", Width = "6%" });
            var ftUpt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpt != null)
            {
                ftUpt.AddClientEvent("editCellEnded", "gridProductosBufferEditCellEnded");
                ftUpt.EditMode = GridEditMode.Cell;
                ftUpt.EnableAddRow = false;
                ftUpt.EnableDeleteRow = false;          
                

                //var colProductoUpt = new ColumnUpdatingSetting();
                //colProductoUpt.ColumnKey = "ProductoNombre";
                //colProductoUpt.ReadOnly = true;

                var colQtyPedidaUpt = new ColumnUpdatingSetting();
                colQtyPedidaUpt.ColumnKey = "QtyPedida";
                colQtyPedidaUpt.ReadOnly = true;

                var colQtyActualUpt = new ColumnUpdatingSetting();
                colQtyActualUpt.ColumnKey = "QtyActual";
                colQtyActualUpt.ReadOnly = true;

                ftUpt.ColumnSettings = new List<ColumnUpdatingSetting>()
                {
                    /*colProductoUpt,*/ colQtyPedidaUpt, colQtyActualUpt
                };
            }

            var ftPaging = gm.Features.Where(x => x.Name == "Paging").FirstOrDefault() as GridPaging;
            if (ftPaging != null)
            {
                ftPaging.Type = OpType.Local;
            }

            var ftFiltering = gm.Features.Where(x => x.Name == "Filtering").FirstOrDefault() as GridFiltering;
            if (ftFiltering != null)
            {
                ftFiltering.Type = OpType.Local;
            }

            var ftSorting = gm.Features.Where(x => x.Name == "Sorting").FirstOrDefault() as GridSorting;
            if (ftSorting != null)
            {
                ftSorting.Type = OpType.Local;
            }

            return gm;

        }

        /// <summary>
        /// Productos reales leidos en proceso de realización de inventario
        /// </summary>
        /// <returns></returns>
        private GridModel GmProductosAInventario()
        {
            var gm = new GridModel().Crud_Selection(10, true, true);
            gm.AutoCommit = true;
            gm.ID = "gmProductosAInventario";
            gm.PrimaryKey = "Id";            
            gm.DataSourceUrl = Url.Action("HandlerGridProductosAInventarioGeneric");
            gm.Columns.Add(new GridColumn() { HeaderText = "Id", Key = "Id", DataType = "number", Width = "0%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "IdUbicacion", Key = "IdUbicacion", DataType = "number", Width = "0%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width = "60%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Ubicacion", Key = "UbicacionNombre", DataType = "string", Width = "20%" });            
            gm.Columns.Add(new GridColumn() { HeaderText = "Qty", Key = "Qty", DataType = "number", Width = "20%" }); 
            var ftUpt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpt != null)
            {
                //ftUpt.EditMode = GridEditMode.None;
                ftUpt.EditMode = GridEditMode.Row;                
                ftUpt.EnableAddRow = false;
                ftUpt.EnableDeleteRow = false;
                

                var colUptProducto = new ColumnUpdatingSetting();
                colUptProducto.ColumnKey = "ProductoNombre";
                colUptProducto.ReadOnly = true;

                var colUptUbicacion = new ColumnUpdatingSetting();
                colUptUbicacion.ColumnKey = "UbicacionNombre";
                colUptUbicacion.ReadOnly = true;

                var colUptQty = new ColumnUpdatingSetting();
                colUptQty.ColumnKey = "Qty";
                colUptQty.Required = true;
                colUptQty.NumericEditorOptions.MinValue = 1;
                colUptQty.NumericEditorOptions.MaxValue = 900;

                ftUpt.ColumnSettings = new List<ColumnUpdatingSetting>()
                {
                    colUptProducto, colUptUbicacion,colUptQty
                };
            }

            var ftPaging = gm.Features.Where(x => x.Name == "Paging").FirstOrDefault() as GridPaging;
            if (ftPaging != null)
            {
                ftPaging.Type = OpType.Local;
            }

            var ftFiltering = gm.Features.Where(x => x.Name == "Filtering").FirstOrDefault() as GridFiltering;
            if (ftFiltering != null)
            {
                ftFiltering.Type = OpType.Local;
            }

            var ftSorting = gm.Features.Where(x => x.Name == "Sorting").FirstOrDefault() as GridSorting;
            if (ftSorting != null)
            {
                ftSorting.Type = OpType.Local;
            }

            var ftSum = gm.Features.Where(x => x.Name == "Summaries").FirstOrDefault() as GridSummaries;
            if (ftSum != null)
            {
                ftSum.Type = OpType.Local;
                ftSum.ColumnSettings = new List<ColumnSummariesSetting>()
                {
                    new ColumnSummariesSetting {ColumnKey = "Id", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "ProductoNombre", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "UbicacionNombre", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "CodigoBarras", AllowSummaries = false},                    
                    new ColumnSummariesSetting {ColumnKey = "QtyAIngresar", AllowSummaries = true,
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

        [GridDataSourceAction]
        public ActionResult HandlerGridProductosBufferAinventario()
        {
            var idProvider = -1;
            var fechaReferencia = new DateTime(1990, 1, 1);
            idProvider = Session["ProviderSelected"] == null ? idProvider : Convert.ToInt32(Session["ProviderSelected"]);
            fechaReferencia = Session["DateSelected"] == null ? fechaReferencia : Convert.ToDateTime(Session["DateSelected"]);
            return View(new InventarioBl().GetProductosBufferAInventario(idProvider, fechaReferencia));
        }


        [GridDataSourceAction]
        public ActionResult HandlerGridProveedoresFaltantes()
        {
            return View(new ProductoBl().GetProductosProveedorFaltantes(Convert.ToBoolean(Session["filterUmbral"]))); 
        }

        [GridDataSourceAction]
        public ActionResult HandlerProductosAPedir()
        {
            return View(new InventarioBl().GetProductosAPedir((List<int>)Session["filterProviderProductosPedidos"]));
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridProductosAInventarioGeneric()
        {
            var nullObject = new List<EsqGridGenProductoAInventarioVm>().AsQueryable();
            return View(nullObject);
        }


        #endregion

        #region [API]
        public JsonResult AddProductosAPedir(List<ProductosAPedirProyectoUsta> lstCandidates, bool takeCareDay)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.AddProductosAPedir(lstCandidates, takeCareDay, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        /// <summary>
        /// Int value => idRecord 
        /// </summary>
        /// <param name="lstCandidates"></param>
        /// <returns></returns>
        [HttpPost]        
        public JsonResult DeleteProductosAPedir(List<int> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.DeleteProductosAPedir(lstCandidates);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult DeleteProductosBufferInventario(List<int> idRecords)
        {
            var rp = new ResponseBasicVm();
            if (idRecords != null && idRecords.Count > 0)
            {
                inventarioBl = new InventarioBl();
                rp = inventarioBl.DeleteProductosBufferInventario(idRecords);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);

        }

        /// <summary>
        /// BasicIntVm structure
        /// Id = IdRecord
        /// Value = Qty        
        /// </summary>
        /// <param name="recordToUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateValorProductoAPedir(BasicIntVm recordToUpdate)
        {
            var rp = new ResponseBasicVm();
            if (recordToUpdate != null && recordToUpdate.Id > 0 && recordToUpdate.Value >= 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.UpdateCantidadProductoAPedir(recordToUpdate, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }
            return Json(rp);
        }

        /// <summary>
        /// Id = IdRecord
        /// State = Valor del estado (falso(No confirmado),true(Confirmado))        
        /// </summary>
        /// <param name="candidates"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatusConfimrmadoProductosAPedir(List<BasicBooleanVm> candidates, bool confirmPv = false)
        {
            var rp = new ResponseBasicVm();
            if (candidates != null && candidates.Count > 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.UpdateStatusConfirmadoProductosAPedir(candidates, idUsuario, confirmPv);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult AddPedidoLog(int idProveedor, string nombreProveedor, int valor)
        {
            var rp = new ResponseBasicVm();
            if (valor > 0 && nombreProveedor != null)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.AddLogPedidoRecibido(idProveedor, nombreProveedor, valor, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos al servidor correctamente!");
            }
            return Json(rp);
        }

        /// <summary>
        /// Se agrega un registro al buffer que va para el inventario y el producto se ha seleccionado desde la caja de busqueda de CB o nombre
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="idProveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddproductoABufferParaInventario(int idProducto)
        {
            var rp = new ResponseBasicVm();
            if (idProducto > 0 && Session["ProviderSelected"] != null)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.AddProductoABufferInventario(idProducto, Convert.ToInt32(Session["ProviderSelected"]), Convert.ToDateTime(Session["DateSelected"]),idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos al servidor correctamente!");
            }
            return Json(rp);
        }

        /// <summary>
        /// Actualiza el valor del campo de la tabla buffer. Se hace de esta manera para poder aprovechar el procesamiento en el cliente
        /// </summary>
        /// <param name="idCampo"></param>
        /// <param name="valor"></param>
        /// <param name="idRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateCampoRecordProductoBufferAinventario(int idCampo, string valor, int idRecord)
        {
            var rp = new ResponseBasicVm();
            if (idRecord > 0 && idCampo > 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.UpdateRecordProductoBufferAInventario(idCampo, valor, idRecord, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos al servidor correctamente!");
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult ProcesarRegistrosBufferAInventario(List<int> idRecords)
        {
            var rp = new ResponseBasicVm();
            if (idRecords != null && idRecords.Count > 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.ProcesarRecordsBufferAinventario(idRecords, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        /// <summary>
        /// Se sobreescribe la cantidad del producto mediante un inventario realizado
        /// El tipo de la colección que se envia corresponde a:
        /// IdParent => El id del producto en cuestión
        /// Id => La cantidad de unidades leidas
        /// Value => El id de la ubicación donde se realiza el inventario
        /// </summary>
        /// <param name="recordsToProcess"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateQtyProductosPorInventario(List<BasicDependantIntVm> recordsToProcess)
        {
            var rp = new ResponseBasicVm();
            if (recordsToProcess != null && recordsToProcess.Count > 0)
            {
                inventarioBl = new InventarioBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = inventarioBl.UpdateQtyProductosPorInventario(recordsToProcess, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        #endregion

        #region [Methods]
        public List<BasicStrVm> GetDataProviders()
        {
            var providers = new ProveedorBl().Get().Select(x => new
            {
                x.Nombre,
                x.Entrega_Lunes,
                x.Entrega_Martes,
                x.Entrega_Miercoles,
                x.Entrega_Jueves,
                x.Entrega_Viernes,
                x.Entrega_Sabado,
                x.Entrega_Domingo,
                x.Visita_Lunes,
                x.Visita_Martes,
                x.Visita_Miercoles,
                x.Visita_Jueves,
                x.Visita_Viernes,
                x.Visita_Sabado,
                x.Visita_Domingo
            }).ToList();

            var collectionProyection = new List<BasicStrVm>();

            providers.ForEach(x =>
            {
                if (x.Entrega_Lunes)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Lunes";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Entrega_Martes)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Martes";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Entrega_Miercoles)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Miercoles";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Entrega_Jueves)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Jueves";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Entrega_Viernes)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Viernes";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Entrega_Sabado)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Sabado";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Entrega_Domingo)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Entrega_Domingo";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Lunes)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Lunes";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Martes)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Martes";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Miercoles)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Miercoles";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Jueves)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Jueves";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Viernes)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Viernes";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Sabado)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Sabado";
                    collectionProyection.Add(currentBuffer);
                }
                if (x.Visita_Domingo)
                {
                    var currentBuffer = new BasicStrVm();
                    currentBuffer.Id = x.Nombre;
                    currentBuffer.Value = "Visita_Domingo";
                    collectionProyection.Add(currentBuffer);
                }
            });

            return collectionProyection;
        }
        #endregion
    }
}