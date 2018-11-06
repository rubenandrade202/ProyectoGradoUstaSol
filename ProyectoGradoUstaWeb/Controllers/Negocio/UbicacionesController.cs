using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ProyectoGradoUstaWeb.Controllers.Negocio
{
    public class UbicacionesController : BaseController
    {
        #region [FIELDS]
        UbicacionesBl ubicacionesBl;
        #endregion

        #region [MVC]
        public ActionResult Index()
        {
            ubicacionesBl = new UbicacionesBl();
            var nameFilePicture = string.Empty;
            var nameFilesDirectory = Directory.GetFiles(HostingEnvironment.MapPath(@"~\Content\Images\Ubicaciones"));
            foreach (var nameFileDirectory in nameFilesDirectory)
            {
                if(nameFileDirectory.Contains("marcador.png"))
                {
                    continue;
                }
                if(nameFileDirectory.Contains("pruebaPlano.jpg"))
                {
                    continue;
                }
                nameFilePicture = Path.GetFileName(nameFileDirectory);
            }
            
            if(nameFilePicture != string.Empty)
            {
                ViewBag.UrlImagenSupermercado = Url.Content("~/Content/Images/Ubicaciones/" + nameFilePicture);
            }
            else
            {
                ViewBag.UrlImagenSupermercado = Url.Content("~/Content/Images/Ubicaciones/pruebaPlano.jpg");
            }            
            var gm = GmUbicacionesMain();
            gm.DataSource = ubicacionesBl.Get();
            var model = new UbicacionesMainPageVm()
            {
                GmMainUbicaciones = gm,
                CmbTiposUbicacion = Controls.CmbTiposUbicacion("cmbTiposUbicacion", true, false)
            };
            return View(model);
        }         
        
        #endregion

        #region [IG]
        private GridModel GmUbicacionesMain()
        {
            var gm = new GridModel().Crud();
            var ftUpdating = gm.Features.Where(x => x.Name == "Updating").FirstOrDefault() as GridUpdating;
            if(ftUpdating != null)
            {
                ftUpdating.EnableAddRow = false;
                ftUpdating.AddClientEvent("rowDeleting", "rowDeletingGridUbicaciones");
                ftUpdating.EditMode = GridEditMode.None;              
            }
            gm.DataSourceUrl = Url.Action("HandlerGridUbicaciones");
            gm.ID = "gmUbicaciones";
            gm.PrimaryKey = "Id";
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn() {Key="Id", DataType="number", Hidden=true },
                new GridColumn() {Key="UrlImagen", DataType="string", Hidden = true },
                new GridColumn() {Key="NombreUbicacion", HeaderText= "Ubicacion", DataType="string" },
                new GridColumn() {Key="IdTipoUbicacion", DataType="number", Hidden=true },
                new GridColumn() {Key="NombreTipoUbicacion", HeaderText= "Tipo", DataType="string" },
                new GridColumn() {Key="Descripcion", HeaderText= "Descripción", DataType="string" },                
                new GridColumn() {Key="FechaRegistro", HeaderText= "Fecha Creado",Format="d MMM yyyy h:mm tt", DataType="date" },
                new GridColumn() {Key="NombreUsuarioCreador", HeaderText= "Usuario registro", DataType="string" },
                new GridColumn() {Key="FechaModificado", HeaderText= "Fecha modificado",Format="d MMM yyyy h:mm tt", DataType="date" },
                new GridColumn() {Key="NombreUsuarioModificador", HeaderText= "Usuario modificador", DataType="string" },
                new UnboundColumn() { DataType="string", Template = "<input type='button' onclick='nsUbc.LoadInfoEdit(${Id}, ${IdTipoUbicacion}, \"${UrlImagen}\");' value='EDITAR' />" }
            };
            return gm;            
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridUbicaciones()
        {            
            return View(new UbicacionesBl().Get());
        }
        #endregion

        #region [API]

        [HttpPost]
        public JsonResult Add(List<UbicacionesAddVm> lstCandidates)
        {           
            var rp = new ResponseBasicVm();
            if(lstCandidates != null && lstCandidates.Count > 0)
            {
                ubicacionesBl = new UbicacionesBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = ubicacionesBl.Add(lstCandidates, idUsuario);
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
                ubicacionesBl = new UbicacionesBl();               
                rp = ubicacionesBl.Delete(idsToDelete);
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("No se han enviado datos!");
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult Update(List<UbicacionesUpdateVm> lstCandidates)
        {
            var rp = new ResponseBasicVm();
            if (lstCandidates != null && lstCandidates.Count > 0)
            {
                ubicacionesBl = new UbicacionesBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = ubicacionesBl.Update(lstCandidates.FirstOrDefault(), idUsuario);
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