using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoGradoUstaUtility;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Negocio
{
    public class ClienteController : Controller
    {
        #region [FIELDS]
        ClienteBl clienteBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            clienteBl = new ClienteBl();
            var gm = GetGmClientes();
            gm.DataSource = clienteBl.Get();            
            return View(gm);
        }
        #endregion

        #region [IG]
        public GridModel GetGmClientes()
        {
            var gm = new GridModel().Crud();
            gm.ID = "gmClientes";
            gm.PrimaryKey = "Id";
            gm.DataSourceUrl = Url.Action("HandlerGridClientes");

            var ftUpdating = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if (ftUpdating != null)
            {
                ftUpdating.AddClientEvent("rowDeleting", "rowDeletingGridClientes");
                ftUpdating.AddClientEvent("editRowEnding", "editRowEndingGridClientes");
                ftUpdating.AddClientEvent("editRowEnded", "editRowEndedGridClientes");
                ftUpdating.AddClientEvent("editCellStarted", "editCellStartedGridClientes");
                ftUpdating.AddClientEvent("editRowStarting", "editRowStartingGridClientes");
                
                ftUpdating.EditMode = GridEditMode.Row;

                var colNombreUpt = new ColumnUpdatingSetting();
                colNombreUpt.Required = true;
                colNombreUpt.ColumnKey = "Nombre";
                colNombreUpt.TextEditorOptions.ID = "txtIgNombreCliente";
                colNombreUpt.TextEditorOptions.AddClientEvent("textChanged", "textChangedEditorNombreCliente");

                var colCupoEmpleadoUpt = new ColumnUpdatingSetting();
                colCupoEmpleadoUpt.ColumnKey = "CupoEmpleado";
                colCupoEmpleadoUpt.ReadOnly = true;


                var colCupoAsignadoUpt = new ColumnUpdatingSetting();
                colCupoAsignadoUpt.ColumnKey = "CupoAsignado";                
                colCupoAsignadoUpt.DefaultValue = 0;
                colCupoAsignadoUpt.NumericEditorOptions.MinValue = 0;
                colCupoAsignadoUpt.NumericEditorOptions.MaxValue = 400000;
                colCupoAsignadoUpt.Required = true;

                var colFechaRegistroUpt = new ColumnUpdatingSetting();
                colFechaRegistroUpt.ColumnKey = "FechaRegistro";
                colFechaRegistroUpt.ReadOnly = true;

                var colFechaModificadoUpt = new ColumnUpdatingSetting();
                colFechaModificadoUpt.ColumnKey = "FechaModificado";
                colFechaModificadoUpt.ReadOnly = true;

                ftUpdating.ColumnSettings = new List<ColumnUpdatingSetting>()
                {
                    colNombreUpt, colCupoEmpleadoUpt, colCupoAsignadoUpt,
                    colFechaRegistroUpt, colFechaModificadoUpt
                };
            }

            gm.Columns.Add(new GridColumn() {  Key = "Id", DataType = "number", Hidden =true});
            gm.Columns.Add(new GridColumn() { HeaderText = "Nombre", Key = "Nombre", DataType = "string", Width = "20%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Email", Key = "Email", DataType = "string", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Telefono", Key = "Telefono", DataType = "string", Width = "15%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cupo Asignado", Key = "CupoAsignado", DataType = "number", Width = "15%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Cupo Empleado", Key = "CupoEmpleado", DataType = "number", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Fecha Registro", Key = "FechaRegistro", DataType = "date", Format = "d MMM yyyy h:mm tt", Width = "10%" });
            gm.Columns.Add(new GridColumn() { HeaderText = "Fecha Modificado", Key = "FechaModificado", DataType = "date", Format = "d MMM yyyy h:mm tt", Width = "10%"});
            gm.Columns.Add(new UnboundColumn() { HeaderText="", Template= "<input type='button' value='Saldar' onclick='clienNs.PromptSaldarDeudaCliente(\"${Nombre}\", ${CupoEmpleado}, ${Id} )' />", Width="10%"});
            return gm;
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridClientes()
        {
            return View(new ClienteBl().Get());
        }

        #endregion

        #region [API]
        [HttpPost]
        public JsonResult Add(List<ClientesProyectoUsta> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                clienteBl = new ClienteBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = clienteBl.Add(lstCandidates, idUsuario);
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
                clienteBl = new ClienteBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = clienteBl.Delete(idsToDelete, idUsuario);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult Update(List<ClientesProyectoUsta> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                clienteBl = new ClienteBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = clienteBl.Update(lstCandidates.FirstOrDefault(), idUsuario);                
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateSaldoDeuda(int idCliente, int valorConsignado)
        {
            var rp = new ResponseBasicVm();
            if (idCliente != 0 && idCliente > 0 && valorConsignado != 0 && valorConsignado > 0)
            {
                clienteBl = new ClienteBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = clienteBl.UpdateCuentaCliente(idCliente, valorConsignado, idUsuario);                
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult CheckNameAvailability(string name)
        {
            var rp = new ResponseBasicVm();
            if (name != null && name != string.Empty)
            {
                clienteBl = new ClienteBl();
                rp = clienteBl.CheckNameAvailability(name);
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