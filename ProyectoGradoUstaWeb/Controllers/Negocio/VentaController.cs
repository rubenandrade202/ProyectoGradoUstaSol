using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoGradoUstaUtility;
using ProyectoGradoUstaWeb;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Negocio
{
    public class VentaController : Controller
    {
        #region [FIELDS]
        VentaBl ventaBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            var cmbVentaUno = Controls.CmbRemoteGeneric("cmbVenta_1", false, false, Url.Action("HandlerRemoteProductos", "Helper"));            
            cmbVentaUno.Width = "450px";
            cmbVentaUno.Height = "25px";
            cmbVentaUno.AddClientEvent("dropDownClosed", "dropDownClosedCmbVenta");
            cmbVentaUno.PreventSubmitOnEnter = false;            
                   
            var cmbVentaDos = Controls.CmbRemoteGeneric("cmbVenta_2", false, false, Url.Action("HandlerRemoteProductos", "Helper"));
            cmbVentaDos.Width = "450px";
            cmbVentaDos.Height = "25px";
            cmbVentaDos.AddClientEvent("dropDownClosed", "dropDownClosedCmbVenta");
            cmbVentaDos.PreventSubmitOnEnter = false;

            var cmbVentaTres = Controls.CmbRemoteGeneric("cmbVenta_3", false, false, Url.Action("HandlerRemoteProductos", "Helper"));
            cmbVentaTres.Width = "450px";
            cmbVentaTres.Height = "25px";
            cmbVentaTres.AddClientEvent("dropDownClosed", "dropDownClosedCmbVenta"); 
            cmbVentaTres.PreventSubmitOnEnter = false;

            var cmbVentaCuatro = Controls.CmbRemoteGeneric("cmbVenta_4", false, false, Url.Action("HandlerRemoteProductos", "Helper"));
            cmbVentaCuatro.Width = "450px";
            cmbVentaCuatro.Height = "25px";
            cmbVentaCuatro.AddClientEvent("dropDownClosed", "dropDownClosedCmbVenta");
            cmbVentaCuatro.PreventSubmitOnEnter = false;           

            var cmbBilleteUno = Controls.CmbBilletes("cmbBillete_1", false, false, "120px", "20px");
            cmbBilleteUno.ItemTemplate = "{{if ${Id} == 1}} <img src='" + Url.Content("~/Content/Images/Billetes/1000.png")  + "' /img><b>${Value}</b>{{elseif ${Id} == 2 }} <img src='" + Url.Content("~/Content/Images/Billetes/2000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 3 }} <img src='" + Url.Content("~/Content/Images/Billetes/5000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 4 }} <img src='" + Url.Content("~/Content/Images/Billetes/10000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 5 }} <img src='" + Url.Content("~/Content/Images/Billetes/20000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 6 }} <img src='" + Url.Content("~/Content/Images/Billetes/50000.png") + "' /img><b>${Value}</b>{{else}} <img src='" + Url.Content("~/Content/Images/Billetes/100000.png") + "' /img><b>${Value}</b> {{/if}}";
            cmbBilleteUno.AddClientEvent("selectionChanged", "CmbBilleteSelectionChanged");

            var cmbBilleteDos = Controls.CmbBilletes("cmbBillete_2", false, false, "120px", "20px");
            cmbBilleteDos.ItemTemplate = "{{if ${Id} == 1}} <img src='" + Url.Content("~/Content/Images/Billetes/1000.png") + "' /img><b>${Value}</b>{{elseif ${Id} == 2 }} <img src='" + Url.Content("~/Content/Images/Billetes/2000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 3 }} <img src='" + Url.Content("~/Content/Images/Billetes/5000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 4 }} <img src='" + Url.Content("~/Content/Images/Billetes/10000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 5 }} <img src='" + Url.Content("~/Content/Images/Billetes/20000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 6 }} <img src='" + Url.Content("~/Content/Images/Billetes/50000.png") + "' /img><b>${Value}</b>{{else}} <img src='" + Url.Content("~/Content/Images/Billetes/100000.png") + "' /img><b>${Value}</b> {{/if}}";
            cmbBilleteDos.AddClientEvent("selectionChanged", "CmbBilleteSelectionChanged");

            var cmbBilleteTres = Controls.CmbBilletes("cmbBillete_3", false, false, "120px", "20px");
            cmbBilleteTres.ItemTemplate = "{{if ${Id} == 1}} <img src='" + Url.Content("~/Content/Images/Billetes/1000.png") + "' /img><b>${Value}</b>{{elseif ${Id} == 2 }} <img src='" + Url.Content("~/Content/Images/Billetes/2000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 3 }} <img src='" + Url.Content("~/Content/Images/Billetes/5000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 4 }} <img src='" + Url.Content("~/Content/Images/Billetes/10000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 5 }} <img src='" + Url.Content("~/Content/Images/Billetes/20000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 6 }} <img src='" + Url.Content("~/Content/Images/Billetes/50000.png") + "' /img><b>${Value}</b>{{else}} <img src='" + Url.Content("~/Content/Images/Billetes/100000.png") + "' /img><b>${Value}</b> {{/if}}";
            cmbBilleteTres.AddClientEvent("selectionChanged", "CmbBilleteSelectionChanged");

            var cmbBilleteCuatro = Controls.CmbBilletes("cmbBillete_4", false, false, "120px", "20px");
            cmbBilleteCuatro.ItemTemplate = "{{if ${Id} == 1}} <img src='" + Url.Content("~/Content/Images/Billetes/1000.png") + "' /img><b>${Value}</b>{{elseif ${Id} == 2 }} <img src='" + Url.Content("~/Content/Images/Billetes/2000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 3 }} <img src='" + Url.Content("~/Content/Images/Billetes/5000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 4 }} <img src='" + Url.Content("~/Content/Images/Billetes/10000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 5 }} <img src='" + Url.Content("~/Content/Images/Billetes/20000.png") + "' /img><b>${Value}</b> {{elseif ${Id} == 6 }} <img src='" + Url.Content("~/Content/Images/Billetes/50000.png") + "' /img><b>${Value}</b>{{else}} <img src='" + Url.Content("~/Content/Images/Billetes/100000.png") + "' /img><b>${Value}</b> {{/if}}";
            cmbBilleteCuatro.AddClientEvent("selectionChanged", "CmbBilleteSelectionChanged");      

            var model = new VentaMainPageVm()
            {
                CmbBilleteUno = cmbBilleteUno,
                CmbBilleteDos = cmbBilleteDos,
                CmbBilleteTres = cmbBilleteTres,
                CmbBilleteCuatro = cmbBilleteCuatro,
                CmbVentaUno = cmbVentaUno,
                CmbVentaDos = cmbVentaDos,
                CmbVentaTres = cmbVentaTres,
                CmbVentaCuatro = cmbVentaCuatro,
                CmbClienteVentaUno = Controls.CmbClientes("cmbClienteVenta_1", false, false, "100px", "20px"),
                CmbClienteVentaDos = Controls.CmbClientes("cmbClienteVenta_2", false, false, "100px", "20px"),
                CmbClienteVentaTres = Controls.CmbClientes("cmbClienteVenta_3", false, false, "100px", "20px"),
                CmbClienteVentaCuatro = Controls.CmbClientes("cmbClienteVenta_4", false, false, "100px", "20px"),              
                GmVentaUno = GridVentaUno(),
                GmVentaDos = GridVentaDos(),
                GmVentaTres = GridVentaTres(),
                GmVentaCuatro = GridVentaCuatro()
            };
            return View(model);
        }
        #endregion

        #region [IG]
        private GridModel GridVentaUno()
        {
            var gm = new GridModel().Crud_Summaries(5);
            gm.AutoCommit = true;
            gm.ID = "gmVenta_1";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridGeneric");             
            gm.Columns.Add(new GridColumn() { HeaderText = "Id", Key = "Id", DataType = "number", Width="0%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width="48%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Precio", Key = "Precio", DataType = "number", Width = "12%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "Modificar", Key ="Controls", Template= "<a href='javascript:nsVen.ModificarQtyRowGrid(1, ${Id}, 0);' class='btn btn-xs default'><i class='fa fa-minus'></i></ a >&nbsp;<a href='javascript:nsVen.ModificarQtyRowGrid(1, ${Id}, 1);' class='btn btn-xs default'><i class='fa fa-plus'></i></ a >", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cantidad", Key = "Cantidad", DataType = "number", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "SubTotal", Key = "Subtotal", DataType = "number", Width = "20%" });            
            var ftUpt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpt != null)
            {
                ftUpt.EditMode = GridEditMode.None;
                ftUpt.EnableAddRow = false;
                ftUpt.AddClientEvent("rowDeleted", "rowDeletedGridVenta");         
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
                    new ColumnSummariesSetting {ColumnKey = "Cantidad", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Controls", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Precio", AllowSummaries = false},                    
                    new ColumnSummariesSetting {ColumnKey = "Subtotal", AllowSummaries = true,
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

        private GridModel GridVentaDos()
        {
            var gm = new GridModel().Crud_Summaries(5);
            gm.AutoCommit = true;
            gm.ID = "gmVenta_2";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridGeneric");
            gm.Columns.Add(new GridColumn() { HeaderText = "Id", Key = "Id", DataType = "number", Width = "0%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width = "48%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Precio", Key = "Precio", DataType = "number", Width = "12%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "Modificar", Key = "Controls", Template = "<a href='javascript:nsVen.ModificarQtyRowGrid(2, ${Id}, 0);' class='btn btn-xs default'><i class='fa fa-minus'></i></ a >&nbsp;<a href='javascript:nsVen.ModificarQtyRowGrid(2, ${Id}, 1);' class='btn btn-xs default'><i class='fa fa-plus'></i></ a >", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cantidad", Key = "Cantidad", DataType = "number", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "SubTotal", Key = "Subtotal", DataType = "number", Width = "20%" });
            var ftUpt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpt != null)
            {
                ftUpt.EditMode = GridEditMode.None;
                ftUpt.EnableAddRow = false;
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
                    new ColumnSummariesSetting {ColumnKey = "Cantidad", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Controls", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Precio", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Subtotal", AllowSummaries = true,
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

        private GridModel GridVentaTres()
        {
            var gm = new GridModel().Crud_Summaries(5);
            gm.AutoCommit = true;
            gm.ID = "gmVenta_3";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridGeneric");
            gm.Columns.Add(new GridColumn() { HeaderText = "Id", Key = "Id", DataType = "number", Width = "0%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width = "48%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Precio", Key = "Precio", DataType = "number", Width = "12%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "Modificar", Key = "Controls", Template = "<a href='javascript:nsVen.ModificarQtyRowGrid(3, ${Id}, 0);' class='btn btn-xs default'><i class='fa fa-minus'></i></ a >&nbsp;<a href='javascript:nsVen.ModificarQtyRowGrid(3, ${Id}, 1);' class='btn btn-xs default'><i class='fa fa-plus'></i></ a >", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cantidad", Key = "Cantidad", DataType = "number", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "SubTotal", Key = "Subtotal", DataType = "number", Width = "20%" });
            var ftUpt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpt != null)
            {
                ftUpt.EditMode = GridEditMode.None;
                ftUpt.EnableAddRow = false;
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
                    new ColumnSummariesSetting {ColumnKey = "Cantidad", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Controls", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Precio", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Subtotal", AllowSummaries = true,
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

        private GridModel GridVentaCuatro()
        {
            var gm = new GridModel().Crud_Summaries(5);
            gm.AutoCommit = true;
            gm.ID = "gmVenta_4";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridGeneric");
            gm.Columns.Add(new GridColumn() { HeaderText = "Id", Key = "Id", DataType = "number", Width = "0%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Producto", Key = "ProductoNombre", DataType = "string", Width = "48%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Precio", Key = "Precio", DataType = "number", Width = "12%" });
            gm.Columns.Add(new UnboundColumn() { HeaderText = "Modificar", Key = "Controls", Template = "<a href='javascript:nsVen.ModificarQtyRowGrid(4, ${Id}, 0);' class='btn btn-xs default'><i class='fa fa-minus'></i></ a >&nbsp;<a href='javascript:nsVen.ModificarQtyRowGrid(4, ${Id}, 1);' class='btn btn-xs default'><i class='fa fa-plus'></i></ a >", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cantidad", Key = "Cantidad", DataType = "number", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "SubTotal", Key = "Subtotal", DataType = "number", Width = "20%" });
            var ftUpt = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpt != null)
            {
                ftUpt.EditMode = GridEditMode.None;
                ftUpt.EnableAddRow = false;
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
                    new ColumnSummariesSetting {ColumnKey = "Cantidad", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Controls", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Precio", AllowSummaries = false},
                    new ColumnSummariesSetting {ColumnKey = "Subtotal", AllowSummaries = true,
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
        public ActionResult HandlerGridGeneric()
        {
            var nullObject = new List<EsquemaGridGenericoVm>().AsQueryable();
            return View(nullObject);
        }
        #endregion

        #region [API]

        [HttpPost]
        public JsonResult Add(VentaAddVm candidate)
        {
            var rp = new ResponseBasicVm();
            if (candidate != null && candidate.Venta.Count > 0)
            {
                ventaBl = new VentaBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = ventaBl.Add(candidate, idUsuario);
                if (rp.Success)
                {

                }
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }
            rp.Data = "Success";//??????????????????????????????????????????????????
            return Json(rp);
        }

        //public async Task<ActionResult> Add(VentaAddVm candidate)
        //{
        //    return Json(null);
        //}

        #endregion

        #region [Methods]
        #endregion
    }
}