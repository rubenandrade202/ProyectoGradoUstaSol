using Infragistics.Web.Mvc;
using Newtonsoft.Json;
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
    public class ProductoController : Controller
    {
        #region [FIELDS]
        ProductoBl productoBl;
        ProveedorBl proveedorBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            Session["fltProductPageCb"] = null;
            Session["fltProductPagePv"] = null;            
            productoBl = new ProductoBl();
            proveedorBl = new ProveedorBl();
            var gm = GmProductosMain();            
            gm.DataSource = productoBl.Get(null, null);
            var gmProviders = GmProveedoresProducto();
            gmProviders.DataSource = productoBl.GetProveedoresProducto(0);
            var gmCodeBars = GmCodigosBarrasProducto();
            gmCodeBars.DataSource = productoBl.GetCodeBarProducto(0);
            var model = new ProductoMainPageVm()
            {
                GridProductos = gm,
                GridProveedoresProducto = gmProviders,
                GridCodigosBarrasProducto = gmCodeBars,
                CmbUbicacionNegocio = Controls.CmbUbicacionesNegocio("cmbUbicacionNegocio", false, false),
                CmbUbicacionStock = Controls.CmbUbicacionesStock("cmbUbicacionStock", false, false),
                CmbProveedores = Controls.CmbProveedores("cmbProveedores", false, true)
            };
            ViewBag.ProveedoresJson = JsonConvert.SerializeObject(proveedorBl.Get().Select(x => new { x.Id, x.Nombre}));            
            return View(model);
        }      

        [HttpPost]
        public JsonResult LimpiarFiltroCb()
        {
            Session["fltProductPageCb"] = null;
            var rp = new ResponseBasicVm();
            rp.Success = true;
            return Json(rp);
        }

        [HttpPost]
        public JsonResult LimpiarFiltroPv()
        {
            Session["fltProductPagePv"] = null;
            var rp = new ResponseBasicVm();
            rp.Success = true;
            return Json(rp);
        }

        [HttpPost]
        public JsonResult FilterCb(string codeBar)
        {
            var rp = new ResponseBasicVm();
            if(codeBar != null && codeBar != string.Empty && codeBar.Length > 0)
            {
                productoBl = new ProductoBl();
                rp = productoBl.CheckCodeBar(codeBar);
                if(rp.Success)
                {
                    Session["fltProductPageCb"] = codeBar;
                }
                else
                {
                    Session["fltProductPageCb"] = null;
                }
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se ha enviado correctamente el parametro al servidor");
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult FilterPv(string provedor)
        {
            var rp = new ResponseBasicVm();
            if (provedor != null && provedor != string.Empty && provedor.Length > 0)
            {
                rp.Success = true;
                Session["fltProductPagePv"] = provedor.Trim().ToUpper();
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se ha enviado correctamente el parametro al servidor");
            }
            return Json(rp);
        }
        #endregion

        #region [IG]
        private GridModel GmProductosMain()
        {
            var gm = new GridModel().Crud();
            var ftUpdating = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpdating != null)
            {
                ftUpdating.EnableAddRow = false;
                ftUpdating.AddClientEvent("rowDeleting", "rowDeletingGridProductos");
                ftUpdating.EditMode = GridEditMode.None;
            }
            gm.DataSourceUrl = Url.Action("HandlerGridProductos");
            gm.ID = "gmProductos";
            gm.PrimaryKey = "Id";
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn() {Key="Id", DataType="number", Hidden=true },
                new GridColumn() {Key="IdUbicacionStock", DataType="number", Hidden=true },
                new GridColumn() {Key="IdUbicacionNegocio", DataType="number", Hidden=true },
                new GridColumn() {Key="IdUsuarioRegistro", DataType="number", Hidden=true },
                new GridColumn() {Key="IdUsuarioModificador", DataType="number", Hidden=true },
                new GridColumn() {Key="Proveedores",  Hidden=true },
                new GridColumn() {Key="CodigosBarras",  Hidden=true },
                new GridColumn() {Key="Nombre", HeaderText="Nombre", DataType="string", Width="24%" },
                new GridColumn() {Key="NombreUbicacionStock", HeaderText="Stock", DataType="string", Width="5%" },
                new GridColumn() {Key="NombreUbicacionNegocio", HeaderText="Negocio", DataType="string", Width="5%" },
                new GridColumn() {Key="NombreUsuarioRegistro", HeaderText= "UsuarioRegistro", DataType="string", Width="7%" },
                new GridColumn() {Key="NombreUsuarioModificador", HeaderText= "UsuarioModificador", DataType="string", Width="8%" },
                new GridColumn() {Key="Precio", HeaderText= "Precio", DataType="number", Width="5%" },
                new GridColumn() {Key="CantidadActual", HeaderText= "Qty", DataType="number", Width="5%" },
                new GridColumn() {Key="CantidadUmbral", HeaderText= "Umbral", DataType="number", Width="5%" },
                new GridColumn() {Key="FechaRegistro", HeaderText= "FechaRegistro", DataType="date",Format="d MMM yyyy h:mm tt", Width="7%" },
                new GridColumn() {Key="FechaModificado", HeaderText= "FechaModificado", DataType="date",Format="d MMM yyyy h:mm tt", Width="7%" },
                new UnboundColumn() { Key="Proveedores", HeaderText= "Proveedores", Template = "{{each ${Proveedores} }} ${Proveedores.Value} <br /> {{/each}}", Width="15%" },
                new UnboundColumn() { Template = "<input type='button' onclick='nsProduc.ReadDataGrid(${Id});' value='EDITAR' />", Width="7%" }
            };
            gm.AddClientEvent("rendered", "gmProductosRendered");
            return gm;
        }

        private GridModel GmProveedoresProducto()
        {
            var gm = new GridModel().Crud();
            gm.AutoCommit = true;
            var ftUpdating = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpdating != null)
            {                
                var updClmNombreProveedor = new ColumnUpdatingSetting();
                updClmNombreProveedor.ColumnKey = "NombreProveedor";
                updClmNombreProveedor.Required = true;
                updClmNombreProveedor.EditorType = ColumnEditorType.Combo;

                var updClmIdProveedor = new ColumnUpdatingSetting();
                updClmIdProveedor.ReadOnly = true;
                updClmIdProveedor.ColumnKey = "IdProveedor";
                //updClmNombreProveedor.ComboEditorOptions.ID = "cmbComboEditingGridProveedores";
                //updClmNombreProveedor.ComboEditorOptions.DataSource = new GeneralDomainBl().GetProveedores();
                //updClmNombreProveedor.ComboEditorOptions.ValueKey = "Id";
                //updClmNombreProveedor.ComboEditorOptions.TextKey = "Nombre";                

                var updClmPrecio = new ColumnUpdatingSetting();
                updClmPrecio.ColumnKey = "Precio";
                updClmPrecio.Required = true;
                updClmPrecio.EditorType = ColumnEditorType.Currency;
                updClmPrecio.NumericEditorOptions.MinDecimals = 0;
                updClmPrecio.NumericEditorOptions.MinValue = 0;
                updClmPrecio.NumericEditorOptions.MaxValue = 99999;
                updClmPrecio.NumericEditorOptions.MaxDecimals = 0;
                updClmPrecio.NumericEditorOptions.DataMode = NumericEditorDataMode.Int;                
                                
                ftUpdating.AddClientEvent("rowDeleting", "rowDeletingGridProductosProveedor");
                ftUpdating.AddClientEvent("rowAdding", "rowAddingGridProductosProveedor");
                ftUpdating.AddClientEvent("editRowStarted", "editRowStartedGridProveedorProducto");
                
                ftUpdating.EditMode = GridEditMode.None;
                ftUpdating.ColumnSettings = new List<ColumnUpdatingSetting>()
                {                    
                     updClmNombreProveedor, updClmPrecio, updClmIdProveedor
                };
                
            }
            gm.DataSourceUrl = Url.Action("HandlerGridProductosProveedor");
            gm.ID = "gmProductosProveedor";
            gm.PrimaryKey = "IdProveedor";
            gm.Columns = new List<GridColumn>()
            {
                //new GridColumn() {Key="IdRecord", DataType="string", Hidden=true },
                new GridColumn() {Key="IdProveedor", DataType="number", Width="25%"},
                new GridColumn() {Key="NombreProveedor", DataType="string", Width="50%"},
                new GridColumn() {Key="Precio", DataType="number", Width="25%"},               
            };
            return gm;
        }

        private GridModel GmCodigosBarrasProducto()
        {
            var gm = new GridModel().Crud();
            gm.AutoCommit = true;
            var ftUpdating = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpdating != null)
            {
                ftUpdating.EnableAddRow = false;
                ftUpdating.AddClientEvent("rowDeleting", "rowDeletingGridProductosCodigoBarras");
                ftUpdating.EditMode = GridEditMode.None;
            }
            gm.DataSourceUrl = Url.Action("HandlerGridProductosCodigosBarras");
            gm.ID = "gmProductosCodigoBarras";
            gm.PrimaryKey = "Codigo";
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn() {Key="Codigo", DataType="string", Width="100%" }                
            };
            return gm;
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridProductos()
        {
            return View(new ProductoBl().Get(Session["fltProductPageCb"] == null ? null : Session["fltProductPageCb"].ToString(), Session["fltProductPagePv"] == null ? null : Session["fltProductPagePv"].ToString()));
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridProductosProveedor()
        {
            var idProducto = 0;
            return View(new ProductoBl().GetProveedoresProducto(idProducto));
        }


        [GridDataSourceAction]
        public ActionResult HandlerGridProductosCodigosBarras()
        {
            var idProducto = 0;
            return View(new ProductoBl().GetCodeBarProducto(idProducto));
        }

        #endregion

        #region [API]

        [HttpPost]
        public JsonResult Get(int? idProducto)
        {
            var rp = new ResponseBasicVm();
            productoBl = new ProductoBl();
            if (idProducto == null)
            {
                rp.Data = productoBl.Get(null, null);
            }
            else
            {
                rp.Data = productoBl.Get(null, null).Where(x => x.Id == idProducto).AsQueryable();
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult Add(List<ProductoAddVm> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                productoBl = new ProductoBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = productoBl.Add(lstCandidates, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult Delete(List<int> idsToDelete)
        {
            var rp = new ResponseBasicVm();
            if (idsToDelete != null && idsToDelete.Count > 0)
            {
                productoBl = new ProductoBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = productoBl.Delete(idsToDelete, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult Update(List<ProductoUpdateVm> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                productoBl = new ProductoBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = productoBl.Update(lstCandidates.FirstOrDefault(), idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateGeneric(List<ProductosProyectoUsta> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                productoBl = new ProductoBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = productoBl.UpdateGeneric(lstCandidates, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateProductoProveedor(List<int> productos, List<int> proveedores)
        {
            var rp = new ResponseBasicVm();
            if (productos != null && productos.Count > 0 && proveedores != null && proveedores.Count >  0)
            {
                productoBl = new ProductoBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = productoBl.UpdateProductoProveedor(productos, proveedores, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateDesasociarProductoProveedor(List<BasicIntVm> productosproveedor)
        {
            var rp = new ResponseBasicVm();
            if (productosproveedor != null && productosproveedor.Count > 0)
            {
                productoBl = new ProductoBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = productoBl.UpdateDesasociarProductoProveedor(productosproveedor, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult CheckCodeBarExist(string codeBar)
        {
            var rp = new ResponseBasicVm();
            if (codeBar != null && codeBar != string.Empty)
            {
                productoBl = new ProductoBl();
                rp = productoBl.CheckCodeBar(codeBar);
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
        #endregion
	}
}