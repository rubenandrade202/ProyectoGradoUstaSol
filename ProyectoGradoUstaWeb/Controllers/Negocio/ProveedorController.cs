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
    public class ProveedorController : Controller
    {
        #region [FIELDS]
        ProveedorBl proveedorBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            proveedorBl = new ProveedorBl();
            var gm = GmProveedoresMain();
            gm.DataSource = proveedorBl.Get();
            return View(gm);
        }

        #endregion

        #region [IG]
        private GridModel GmProveedoresMain()
        {
            var gm = new GridModel().Crud();
            var ftUpdating = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpdating != null)
            {
                ftUpdating.EnableAddRow = false;
                ftUpdating.AddClientEvent("rowDeleting", "rowDeletingGridProveedores");
                ftUpdating.EditMode = GridEditMode.None;
            }
            gm.DataSourceUrl = Url.Action("HandlerGridProveedores");
            gm.ID = "gmProveedores";
            gm.PrimaryKey = "Id";
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn() {Key="Id", DataType="number", Hidden=true },
                new GridColumn() {Key="Descripcion", DataType="string", Hidden=true },
                new GridColumn() {Key="Nit", DataType="string", Hidden=true },                                
                new GridColumn() {Key="Nombre", HeaderText= "Proveedor", DataType="string" },
                new GridColumn() {Key="Telefono", HeaderText= "Telefono", DataType="number" },
                new GridColumn() 
                {
                    HeaderText = "VISITAS",                    
                    Group = new List<GridColumn>()
                    {
                        new GridColumn() {Key="Visita_Lunes", HeaderText= "Lunes", DataType="bool", Template = "{{if ${Visita_Lunes} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Visita_Martes", HeaderText= "Martes", DataType="bool", Template = "{{if ${Visita_Martes} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Visita_Miercoles", HeaderText= "Miercoles", DataType="bool" , Template = "{{if ${Visita_Miercoles} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Visita_Jueves", HeaderText= "Jueves", DataType="bool" , Template = "{{if ${Visita_Jueves} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Visita_Viernes", HeaderText= "Viernes", DataType="bool" , Template = "{{if ${Visita_Viernes} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Visita_Sabado", HeaderText= "Sabado", DataType="bool" , Template = "{{if ${Visita_Sabado} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Visita_Domingo", HeaderText= "Domingo", DataType="bool" , Template = "{{if ${Visita_Domingo} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "}
                    }
                },
                new GridColumn() 
                {
                    HeaderText = "ENTREGAS",
                    Group = new List<GridColumn>()
                    {
                        new GridColumn() {Key="Entrega_Lunes", HeaderText= "Lunes", DataType="bool" , Template = "{{if ${Entrega_Lunes} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Entrega_Martes", HeaderText= "Martes", DataType="bool" , Template = "{{if ${Entrega_Martes} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Entrega_Miercoles", HeaderText= "Miercoles", DataType="bool" , Template = "{{if ${Entrega_Miercoles} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Entrega_Jueves", HeaderText= "Jueves", DataType="bool" , Template = "{{if ${Entrega_Jueves} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Entrega_Viernes", HeaderText= "Viernes", DataType="bool" , Template = "{{if ${Entrega_Viernes} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Entrega_Sabado", HeaderText= "Sabado", DataType="bool" , Template = "{{if ${Entrega_Sabado} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                        new GridColumn() {Key="Entrega_Domingo", HeaderText= "Domingo", DataType="bool" , Template = "{{if ${Entrega_Domingo} === true }} <img src='" + Url.Content("~/Content/Images/Icons/iconGreen.png") + "' />  {{else}} <img src='" + Url.Content("~/Content/Images/Icons/iconRed.png") + "' />  {{/if}}     "},
                    }
                },                               
                new UnboundColumn() { Key="ColEditar", HeaderText = "Editar", DataType="string", Template = "<input type='button' onclick='nsPro.LoadInfoEdit(${Id}, ${Visita_Lunes}, ${Visita_Martes}, ${Visita_Miercoles}, ${Visita_Jueves}, ${Visita_Viernes}, ${Visita_Sabado}, ${Visita_Domingo}, ${Entrega_Lunes},${Entrega_Martes}, ${Entrega_Miercoles}, ${Entrega_Jueves}, ${Entrega_Viernes}, ${Entrega_Sabado}, ${Entrega_Domingo} );' value='EDITAR' />" }

            };
            return gm;
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridProveedores()
        {
            return View(new ProveedorBl().Get());
        }
        #endregion

        #region [API]

        [HttpPost]
        public JsonResult Add(List<ProveedorAddVm> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                proveedorBl = new ProveedorBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = proveedorBl.Add(lstCandidates, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult Delete(List<short> idsToDelete)
        {
            var rp = new ResponseBasicVm();
            if (idsToDelete != null && idsToDelete.Count > 0)
            {
                proveedorBl = new ProveedorBl();
                rp = proveedorBl.Delete(idsToDelete);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult Update(List<ProveedorUpdateVm> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                proveedorBl = new ProveedorBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = proveedorBl.Update(lstCandidates.FirstOrDefault(), idUsuario);
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