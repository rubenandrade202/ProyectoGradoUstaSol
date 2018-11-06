using Infragistics.Web.Mvc;
using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoGradoUstaSecurity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace ProyectoGradoUstaWeb.Controllers
{
    public class SecurityController : BaseController
    {
        #region [FIELDS]
        SecurityBl seguridadBl = null;
        GeneralesBl generalBl = null;
        ////UsuarioBl usuarioBl;//pasar esta logica a securityBL
        #endregion

        #region [MVC]

        /// <summary>
        /// Se pretende chequear el estado de la sesión en el servidor. Se hace mediante un Head type ajax request
        /// y se setea un header que leera el cliente y así o cargara la pagina que se esta pidiendo o se enviara al login
        /// </summary>
        [HttpHead]
        [AllowAnonymous]        
        public void CheckSession()
        {
            if (Session["UserId"] == null)
            {
                HttpContext.Response.Headers.Add("SessionChecking", "false");
            }
            else
            {
                HttpContext.Response.Headers.Add("SessionChecking", "true");
            }
        }

        public JsonResult StayAlive()
        {
            var rp = new ResponseBasicVm();            
            rp.Data = "Success";
            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpcionesMenu()
        {
            //Pruebas
            //Session["UserIdIdioma"] = 1;
            //Session["UserId"] = 1;
            if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                //Mostrar pagina de error
                return null;
            }
            else
            {
                seguridadBl = new SecurityBl();
                var idIdioma = Convert.ToInt32(Session["UserIdIdioma"]);
                ViewBag.TgmOpciones = this.GetTreeGridModelMenuOpciones(idIdioma);
                ViewBag.TiposDePagina = seguridadBl.GetTiposPagina(idIdioma);
                return View();
            }
        }

       
        public ActionResult Roles()
        {           
            Session["IdRolSelected"] = 0; //propio de la pagina, este es el unico valor de sesión que quedara                        
            seguridadBl = new SecurityBl();   
            var model = new ModelosPaginaRolesVm();
            model.ModeloRoles = GetGridModelRoles();
            model.ModeloUsuariosByRol = GetGridModelUsuariosByRol();
            model.ModeloOpcionesByRol = GetTreeGridMenuByRol();
            ViewBag.UsuariosSistemaUsa = seguridadBl.GetUsuarioSistema();
            return View(model);           
        }

        public ActionResult Usuarios()
        {
            if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                //Mostrar pagina de error base controller
                return null;
            }
            else
            {
                var model = new UsuariosPageVm();                
                model.Usuarios = GetGmUsuarios();
                model.UsuariosAd = GetUsuariosAd();
                model.Roles = GetRoles();
                model.Idiomas = GetIdiomas();
                model.PassWord = GetTemPassword();
                model.PassWordVerify = GetTemPasswordVerify();
                model.Email = GetTemEmail();
                //Para chequear si ya se cargo la variable de aplicación que trae a los usuarios del AD asincronamente
                if (System.Web.HttpContext.Current.Application["UsersUsa"] == null)
                {
                    model.StatusUsersAd = false;
                }


                ViewBag.AdActive = ConfigurationManager.AppSettings["AdActive"] == null
                                       ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["AdActive"]);

                return View(model);
            }

        }

        public ActionResult Backup()
        {
            ViewBag.CorreoConfigurado = ConfigurationManager.AppSettings["MailNotifyMovement"] == null
                                        ? "Correo no configurado" : ConfigurationManager.AppSettings["MailNotifyMovement"].ToString();
            return View();
        }

        public ActionResult Error()
        {
            return View();

            /*
             <error statusCode="404" redirect="~/ErrorHandler/NotFound"/>
             
             */
        }

        /// <summary>
        /// Vista pricipal de inicio del sistema, para ingresar credenciales de ingreso
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            ViewBag.NombreNegocio = ConfigurationManager.AppSettings.Get("NombreNegocio");
            return View();
        }

        /// <summary>
        /// Realiza la validación de ingreso al sistema
        /// </summary>
        /// <param name="logInUsuarioVm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(BasicStrVm logInUsuarioVm)
        {
            if (ModelState.IsValid)
            {
                // se valida el ingreso al sistema
                ResponseBasicVm rp = MemberShip.SigIn(logInUsuarioVm);
                if (rp.Success)
                {
                    var user = rp.Data as UsuariosProyectoUsta;                   
                    Session["UserName"] = logInUsuarioVm.Id;                   
                    Session["UserId"] = user.Id;
                    Session["UserIdIdioma"] = user.IdIdioma;                                                         
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    rp.MessageBad.ForEach(x => ModelState.AddModelError("", x));
                    return View(logInUsuarioVm);
                }
            }
            else
            {
                return View(logInUsuarioVm);
            }
        }

        /// <summary>
        /// Se limpian las variables usadas de sesion, se finaliza la sesión. Se envia al login.
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            //Validar whatever mandar a la pagina de login
            WebSecurity.Logout();
            //Borrar toda la session involucrada con el usuario y demas
            Session["UserName"] = null;
            Session["Menu"] = null;
            Session["UserId"] = null;
            return RedirectToAction("Login", "Security");
        }

        [HttpPost]
       
        public JsonResult SetFiltersRoles(short idRolSelected)
        {
            Session["IdRolSelected"] = idRolSelected;
            return Json(new ResponseBasicVm() { Success = true });
        }
        #endregion

        #region [IG]
        private TreeGridModel GetTreeGridModelMenuOpciones(int idIdioma)
        {
            var tgm = new TreeGridModel().SetBasicAll();
            tgm.DataSourceUrl = Url.Action("HandlerIgTreeMenuOpciones");
            tgm.ID = "tgmOpciones";
            tgm.DataSource = new SecurityBl().GetConfigurarOpcionesMenu(idIdioma);
            tgm.PrimaryKey = "IdOpcion";
            tgm.ChildDataKey = "Child";
            tgm.Columns = new List<GridColumn>()
            {
                new GridColumn(){ Key = "NombreEspanol", HeaderText = "Nombre Esp", DataType = "string", Width="30%"},
                new GridColumn(){ Key = "NombreIngles", HeaderText = "Nombre Ing", DataType = "string", Width="30%"},
                new GridColumn(){ Key = "Controlador", HeaderText = "Controlador", DataType = "string", Width="15%"},
                new GridColumn(){ Key = "Accion", HeaderText = "Accion", DataType = "string", Width="15%"},
                new GridColumn(){ Key = "NombreTipo", HeaderText = "Tipo", DataType = "string", Width="10%"},
                new GridColumn(){ Key = "IdOpcion" , Hidden = true},
                new GridColumn(){ Key = "IdOpcionPadre" , Hidden = true},
                new GridColumn(){ Key = "Tipo" , Hidden = true},
                new GridColumn(){ Key = "Orden" , Hidden = true}
            };

            var ftSelection = tgm.Features.Where(x => x.Name == "Selection").FirstOrDefault();
            ftSelection.AddClientEvent("rowSelectionChanged", "IgTreeGridOpcionesSelected");
            return tgm;
        }

        private TreeGridModel GetTreeGridMenuByRol()
        {
            var tgm = new TreeGridModel().SetBasicAll();
            tgm.DataSourceUrl = Url.Action("HandlerIgTreeMenuOpcionesByRol");
            tgm.ID = "tgmOpcionesByRol";
            tgm.DataSource = new SecurityBl().GetOpcionesMenuByRol(0);
            var some = new SecurityBl().GetOpcionesMenuByRol(0).ToList();
            tgm.PrimaryKey = "IdOpcion";
            tgm.ChildDataKey = "Child";
            tgm.Columns = new List<GridColumn>()
            {
                new GridColumn(){ Key = "IdOpcion", DataType = "number", Hidden = true},
                new GridColumn(){ Key = "NombreEspanol", HeaderText = string.Format("{0} {1}", "Nombre", "Espanol"), DataType = "string", Width="40%"},
                new GridColumn(){ Key = "NombreIngles", HeaderText = string.Format("{0} {1}", "Nombre", "Ingles"), DataType = "string", Width="40%"},
                new GridColumn(){ Key = "IdOpcionPadre" , Hidden = true},
                new GridColumn(){ Key = "IsParent" , DataType="bool", Hidden = true},        
                new GridColumn(){ Key = "Seteo", DataType="bool", Hidden = true},    
                new UnboundColumn()
                {
                    Template = "{{if ${Seteo} === true && ${IsParent} === true}} <input type='checkbox' id='${IdOpcion}_${IdOpcionPadre}' checked onClick='nsRoles.UpdateOpcionesMenu(event, ${IdOpcion});' /> {{elseif ${Seteo} === true && ${IsParent} === false }} <input type='checkbox' id='${IdOpcion}_${IdOpcionPadre}' checked onClick='nsRoles.UpdateOpcionesMenu(event, ${IdOpcion}, ${IdOpcionPadre});' /> {{elseif ${Seteo} === false && ${IsParent} === true }} <input type='checkbox' id='${IdOpcion}_${IdOpcionPadre}' onClick='nsRoles.UpdateOpcionesMenu(event, ${IdOpcion});'/> {{else}} <input type='checkbox' id='${IdOpcion}_${IdOpcionPadre}' onClick='nsRoles.UpdateOpcionesMenu(event, ${IdOpcion}, ${IsParent}, ${IdOpcionPadre});'/> {{/if}}",                                                                       
                    Key = "Seteo", 
                    HeaderText = "Indicador",                     
                    Width="20%"
                }               
            };
            tgm.AddClientEvent("rowExpanded", "nsRolesRowExpanding");
            var ftRowSelectors = tgm.Features.Where(x => x.Name == "RowSelectors").FirstOrDefault() as GridRowSelectors;
            ftRowSelectors.EnableCheckBoxes = false;
            return tgm;
        }

        private GridModel GetGridModelRoles()
        {
            var gm = new GridModel().SetBasicAll(5);
            gm.DataSourceUrl = Url.Action("HandlerGridRoles");
            gm.UpdateUrl = Url.Action("HandlerUpdatingRoles");
            gm.ID = "gmRoles";
            
            gm.PrimaryKey = "IdRol";           
            gm.DataSource = new SecurityBl().GetRoles();
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn(){  Key = "IdRol", HeaderText = "Id", DataType = "number", Hidden=true},
                new GridColumn(){ Key = "NombreRol", HeaderText = "Nombre", DataType = "string", Width="40%"},
                new GridColumn(){ Key = "NombreUsuarioCreador", HeaderText = "UsuarioCreador", DataType = "string", Hidden = true},
                new GridColumn(){ Key = "FechaRegistro", HeaderText = "FechaRegistro",Format="d MMM yyyy h:mm tt", DataType = "date", Hidden = true},
                new GridColumn(){ Key = "NombreUsuarioModificador", HeaderText = "UsuarioModificador", DataType = "string", Width="30%"},
                new GridColumn(){ Key = "FechaModificado", HeaderText = "FechaModificado",Format="d MMM yyyy h:mm tt", DataType = "date", Width="30%"},
            };

            var ftUpdating = new GridUpdating();
            ftUpdating.EditMode = GridEditMode.Row;
            ftUpdating.EnableAddRow = true;
            ftUpdating.EnableDeleteRow = true;
            ftUpdating.AddClientEvent("rowDeleting", "nsRolesPromptEliminarRol");
            ftUpdating.AddClientEvent("editRowEnded", "nsRolesEditRowEnded");
            //ftUpdating.AddRowLabel = "Agregar nuevo rol";
            //ftUpdating.CancelLabel = "Cancelar";
            //ftUpdating.DeleteRowTooltip = "Borrar rol";
            //ftUpdating.CancelTooltip = "Cancelar";
            //ftUpdating.DoneLabel = "OK";
            //ftUpdating.DeleteRowLabel = "Borrar";

            var colUpSetIdRol = new ColumnUpdatingSetting();
            colUpSetIdRol.ColumnKey = "IdRol";
            colUpSetIdRol.ReadOnly = true;            

            var colUpSetNombreUsuarioCreador = new ColumnUpdatingSetting();
            colUpSetNombreUsuarioCreador.ColumnKey = "NombreUsuarioCreador";
            colUpSetNombreUsuarioCreador.ReadOnly = true;
            
            var colUpSetFechaRegistro = new ColumnUpdatingSetting();
            colUpSetFechaRegistro.ColumnKey = "FechaRegistro";
            colUpSetFechaRegistro.ReadOnly = true;

            var colUpSetNombreUsuarioModificador = new ColumnUpdatingSetting();
            colUpSetNombreUsuarioModificador.ColumnKey = "NombreUsuarioModificador";
            colUpSetNombreUsuarioModificador.ReadOnly = true;

            var colUpSetFechaModificado = new ColumnUpdatingSetting();
            colUpSetFechaModificado.ColumnKey = "FechaModificado";
            colUpSetFechaModificado.ReadOnly = true;

            var colUpSetNombreRol = new ColumnUpdatingSetting();
            colUpSetNombreRol.ColumnKey = "NombreRol";
            colUpSetNombreRol.Required = true;            

            ftUpdating.ColumnSettings.Add(colUpSetIdRol);
            ftUpdating.ColumnSettings.Add(colUpSetNombreUsuarioCreador);
            ftUpdating.ColumnSettings.Add(colUpSetFechaRegistro);
            ftUpdating.ColumnSettings.Add(colUpSetNombreUsuarioModificador);
            ftUpdating.ColumnSettings.Add(colUpSetFechaModificado);
            ftUpdating.ColumnSettings.Add(colUpSetNombreRol);

            var ftSelection = gm.Features.Where(x => x.Name == "Selection").FirstOrDefault();
            ftSelection.AddClientEvent("rowSelectionChanged", "nsRolesRowSelectionChanged");

            var ftPaging = gm.Features.Where(x => x.Name == "Paging").FirstOrDefault() as GridPaging;
            ftPaging.ShowPageSizeDropDown = false;
            gm.Features.Add(ftUpdating);
            return gm;
        }

        private GridModel GetGridModelUsuariosByRol()
        {
            seguridadBl = seguridadBl == null ? new SecurityBl() : seguridadBl;
            var gm = new GridModel().SetBasicAll(5);
            gm.DataSourceUrl = Url.Action("HandlerGridUsuariosByRol");
            gm.ID = "gmUsuariosPorRol";
            gm.PrimaryKey = "IdUsuario";
            gm.DataSource = seguridadBl.GetUsuariosPorRol(0);
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn(){ Key = "Id", HeaderText = "Id", DataType = "number", Hidden = true},
                new GridColumn(){ Key = "Value", HeaderText = "Nombre", DataType = "string", Width="70%"},
                new UnboundColumn(){Width="30%", HeaderText="Borrar", Template="<input type='button' class='btn btn-default2 btn-xs' value='"+"Borrar"+"' onClick='nsRoles.RemoverUsuarioFromRol(${Id})' />"}
            };

            var ftPaging = gm.Features.Where(x => x.Name == "Paging").FirstOrDefault() as GridPaging;
            ftPaging.ShowPageSizeDropDown = false;

            var ftRowSelectors = gm.Features.Where(x => x.Name == "RowSelectors").FirstOrDefault() as GridRowSelectors;
            ftRowSelectors.EnableCheckBoxes = false;

            return gm;
        }

        private GridModel GetGmUsuarios()
        {
            seguridadBl = seguridadBl == null ? new SecurityBl() : seguridadBl;
            var gm = new GridModel().SetBasicAll(5);
            gm.DataSourceUrl = Url.Action("HandlerGridUsuarios");
            gm.ID = "gmUsuariosUsa";
            gm.PrimaryKey = "Id";
            gm.DataSource = seguridadBl.GetUsuariosSistema();
            gm.Columns = new List<GridColumn>()
            {
                new GridColumn(){ Key = "UserName", HeaderText = "UserName", DataType = "string", Width="10%"},
                new GridColumn(){ Key = "Email", HeaderText = "Email", DataType = "string", Width="10%"},
                new GridColumn(){ Key = "NombreRol", HeaderText = "Rol", DataType = "string", Width="10%"},
                new GridColumn(){ Key = "UsuarioElite", HeaderText = "Usuario" + " AD", DataType = "string", Width="10%"},//template
                new GridColumn(){ Key = "NombreIdioma", HeaderText = "Idioma", DataType = "string", Width="10%"},
                new GridColumn(){ Key = "MenuEspecifico", HeaderText = "Specific Menu", DataType = "string", Width="10%"},//template              
                new UnboundColumn(){ Width="10%", HeaderText="Editar", Template="<input type='button' class='btn btn-default2 btn-xs'  value='"+"Editar"+ "'onClick='nsUsuariosUsa.LoadInfoUserToForm(${Id}, ${IdRol}, ${IdIdioma});' />"},
                new UnboundColumn(){ Width="10%", HeaderText="Borrar", Template="<input type='button' class='btn btn-default2 btn-xs' value='"+"Borrar"+ "' onClick='nsUsuariosUsa.DeactivateUser(${Id} );' />"},
                new GridColumn(){ Key = "Id", DataType = "number", Hidden = true},
                new GridColumn(){ Key = "IdIdioma", DataType = "number", Hidden = true},
                new GridColumn(){ Key = "IdRol", DataType = "number", Hidden = true}
            };
            return gm;
        }

        private ComboModel GetRoles()
        {
            seguridadBl = seguridadBl == null ? new SecurityBl() : seguridadBl;
            return new ComboModel()
            {
                ID = "cmbRoles",                
                DataSource = seguridadBl.GetRoles().Select(x => new BasicVm() { Id = x.IdRol, Value = x.NombreRol }),
                TextKey = "Value",
                ValueKey = "Id",
                ValidatorOptions = new EditorValidatorOptions()
                {
                    Required = new ValidatorRule() { ErrorMessage = "Required" }
                },
            }.SetBasicOptions();
        }

        private ComboModel GetIdiomas()
        {
            generalBl = generalBl == null ? new GeneralesBl() : generalBl;
            return new ComboModel()
            {
                ID = "cmbIdiomas",
                DataSource = generalBl.GetIdiomas().Select(x => new BasicVm() { Id = x.IdIdioma, Value = x.Nombre }),
                TextKey = "Value",
                ValueKey = "Id",
                SelectedIndexes = new int[] { 1 },//default english                 
                ValidatorOptions = new EditorValidatorOptions()
                {
                    Required = new ValidatorRule() { ErrorMessage = "Required" }
                },
            };
        }

        private ComboModel GetUsuariosAd()
        {
            var cmb = new ComboModel()
            {
                ID = "cmbUsuariosAd",
                DropDownWidth = 400,
                ValidatorOptions = new EditorValidatorOptions()
                {
                    Required = new ValidatorRule() { ErrorMessage = "Required" }
                },
                AutoComplete = true,
                DataSourceUrl = Url.Action("HandlerComboUsuariosAd"),
                FilteringType = ComboFilteringType.Remote,
                FilterExprUrlKey = "filter",
                CaseSensitive = false,
                HighlightMatchesMode = ComboHighlightMatchesMode.Contains,
                FilteringLogic = ComboFilteringLogic.Or,
                TextKey = "Nombre",
                ValueKey = "UserName",
                FilteringCondition = ComboFilteringCondition.Contains,
                AutoSelectFirstMatch = true,
                MultiSelectionSettings = new ComboMultiSelectionSettings() { Enabled = true, ShowCheckBoxes = true, ItemSeparator = ";" }

            };
            cmb.AddClientEvent("rendered", "renderedComboAdCheckData");
            return cmb;
        }

        private TextEditorModel GetTemPassword()
        {
            var tem = new TextEditorModel()
            {
                ID = "txtPassword",
                TextMode = TextEditorTextMode.Password,
                ValidatorOptions = new EditorValidatorOptions()
                {
                    NotificationOptions = new NotifierModel()
                    {
                        Mode = NotifierMode.Popover,
                        Direction = PopoverDirection.Bottom

                    },                    
                    Required = new ValidatorRule() { ErrorMessage = "Requerido" },
                    EqualTo = new EqualToValidatorRule() { ErrorMessage = "Ambas contraseñas deben coincidir", Selector = "#txtPasswordVerify" }
                }

            };
            return tem;
        }

        private TextEditorModel GetTemPasswordVerify()
        {
            var tem = new TextEditorModel()
            {
                ID = "txtPasswordVerify",
                TextMode = TextEditorTextMode.Password,
                ValidatorOptions = new EditorValidatorOptions()
                {

                    NotificationOptions = new NotifierModel()
                    {
                        Mode = NotifierMode.Popover,
                        Direction = PopoverDirection.Bottom

                    },
                    Required = new ValidatorRule() { ErrorMessage = "Requerido" },
                    EqualTo = new EqualToValidatorRule()
                    {
                        ErrorMessage = "Ambas contraseñas deben coincidir",
                        Selector = "#txtPassword"
                    }
                }

            };
            return tem;
        }

        private TextEditorModel GetTemEmail()
        {
            var tem = new TextEditorModel()
            {
                ID = "txtEmail",
                TextMode = TextEditorTextMode.Text,
                ValidatorOptions = new EditorValidatorOptions()
                {
                    Required = new ValidatorRule() { ErrorMessage = "Required" },
                    Email = new ValidatorRule() { ErrorMessage = "Email is not correct" }
                }
            };
            tem.AddClientEvent("valueChanged", "valueChangedEditorEmail");
            return tem;
        }

        [TreeGridDataSourceAction]
        public ActionResult HandlerIgTreeMenuOpciones()
        {
            return View(new SecurityBl().GetConfigurarOpcionesMenu(Convert.ToInt32(Session["UserIdIdioma"])));
        }

        [TreeGridDataSourceAction]
        public ActionResult HandlerIgTreeMenuOpcionesByRol()
        {
            var idRolSelected = (short)0;
            if (Session["IdRolSelected"] != null)
            {
                idRolSelected = Convert.ToInt16(Session["IdRolSelected"]);
            }

            //var some = new SecurityBl().GetOpcionesMenuByRol(idRolSelected).ToList();
            return View(new SecurityBl().GetOpcionesMenuByRol(idRolSelected));
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridRoles()
        {
            return View(new SecurityBl().GetRoles());
        }

        [GridDataSourceAction]
        public ActionResult HandlerGridUsuariosByRol()
        {
            var idRolSelected = (short)0;
            if (Session["IdRolSelected"] != null)
            {
                idRolSelected = Convert.ToInt16(Session["IdRolSelected"]);
            }
            return View(new SecurityBl().GetUsuariosPorRol(idRolSelected));
        }


        [GridDataSourceAction]
        public ActionResult HandlerGridUsuarios()
        {
            return View(new SecurityBl().GetUsuariosSistema());
        }

        public ActionResult HandlerUpdatingRoles()
        {
            var rp = new ResponseBasicVm();
            Transaction<RolesIgVm> transaccion = new GridModel().LoadTransactions<RolesIgVm>(HttpContext.Request.Form["ig_transactions"]).FirstOrDefault();
            if (transaccion != null)
            {
                seguridadBl = new SecurityBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);


                switch (transaccion.type)
                {
                    case "newrow":
                        var lst = new List<string>();
                        lst.Add(transaccion.row.NombreRol.ToUpper());
                        rp = seguridadBl.AddRoles(lst, idUsuario);
                        break;
                    case "row":
                        var lstUpdate = new List<BasicVm>();
                        lstUpdate.Add(new BasicVm() { Id = Convert.ToInt32(transaccion.rowId), Value = transaccion.row.NombreRol });
                        rp = seguridadBl.UpdateRoles(lstUpdate, idUsuario);
                        break;
                    default:
                        rp.Success = false;
                        rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
                        break;
                }
            }
            else
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            return Json(rp);
        }

        public ActionResult HandlerComboUsuariosAd()
        {
            var data = new List<UsuariosAdVm>();
            var requesting = HttpContext.Request.QueryString["filter(Nombre)"];
            if (requesting != null)
            {
                var some = new List<UsuariosAdVm>();
                some.Add(new UsuariosAdVm() { UserName = "prueba1", Mail = "prueba1@gmail.com", Nombre = "prueba1" });
                some.Add(new UsuariosAdVm() { UserName = "prueba2", Mail = "prueba2@gmail.com", Nombre = "prueba2" });
                some.Add(new UsuariosAdVm() { UserName = "prueba3", Mail = "prueba3@gmail.com", Nombre = "prueba3" });
                some.Add(new UsuariosAdVm() { UserName = "prueba4", Mail = "prueba4@gmail.com", Nombre = "prueba4" });
                some.Add(new UsuariosAdVm() { UserName = "prueba5", Mail = "prueba5@gmail.com", Nombre = "prueba5" });
                data = some;//((List<UsuariosAdVm>)System.Web.HttpContext.Current.Application["UsersUsa"]);
          
                var currentActiveUsersNames = new SecurityBl().GetUsuarios().Where(x => x.Activo == true).Select(x => x.UserName).ToList();
                var token = requesting.Replace("startsWith(", string.Empty).Replace(")", string.Empty);
                data = data.Where(x => x.Nombre.ToLower().Contains(token.ToLower())).Where(y => !currentActiveUsersNames.Contains(y.UserName)).Take(10).ToList();
            }
            else
            {
                data = data.Take(0).ToList();
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region [API]

        [HttpPost]
        public JsonResult MakeBackup()
        {
            var rp = new ResponseBasicVm();                      
            ProyectoGradoUstaUtility.Backup.MakeBackup();
            rp.Success = true;
            rp.MessageOk.Add("Se ha mandado la ejecución del proceso. En breve llegara al correo configurado el backup o el mensaje correspondiente del proceso.");                        
            return Json(rp);
        }

        [HttpPost]
        public JsonResult AddUsersAd(List<string> userNames, short idRol, int idIdioma, bool usuarioElite)
        {
            var rp = new ResponseBasicVm();
            if (userNames == null || userNames.Count == 0 || idRol == 0 || idIdioma == 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            //else if (Session["UserId"] == null || System.Web.HttpContext.Current.Application["UsersUsa"] == null)
            //{
            //    rp.Success = false;
            //    rp.MessageBad.Add("MsjSesionError");
            //}
            else
            {
                var idUsuarioRegistro = Convert.ToInt32(Session["UserIdIdioma"]);
                var some = new List<UsuariosAdVm>();
                some.Add(new UsuariosAdVm() { UserName = "prueba1", Mail = "prueba1@gmail.com", Nombre = "prueba1" });
                some.Add(new UsuariosAdVm() { UserName = "prueba2", Mail = "prueba2@gmail.com", Nombre = "prueba2" });
                some.Add(new UsuariosAdVm() { UserName = "prueba3", Mail = "prueba3@gmail.com", Nombre = "prueba3" });
                some.Add(new UsuariosAdVm() { UserName = "prueba4", Mail = "prueba4@gmail.com", Nombre = "prueba4" });
                some.Add(new UsuariosAdVm() { UserName = "prueba5", Mail = "prueba5@gmail.com", Nombre = "prueba5" });

                var lstCurrentAdUsers = some;// ((List<UsuariosAdVm>)System.Web.HttpContext.Current.Application["UsersUsa"]);
                rp = MemberShip.SignUpUsersAd(userNames, idRol, idIdioma, idUsuarioRegistro, lstCurrentAdUsers);
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult AddUserThird(string userName, string email, string passWord, short idRol, int idIdioma)//TODO
        {
            var rp = new ResponseBasicVm();
            if (userName == null || idRol == 0 || idIdioma == 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idUsuarioRegistro = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = MemberShip.SignInUserThird(userName, email, passWord, idRol, idIdioma, idUsuarioRegistro);
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult AddModulo(string nombre)
        {
            var rp = new ResponseBasicVm();
            if (nombre == string.Empty || nombre == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idUsuario = Convert.ToInt32(Session["UserIdIdioma"]);
                var idIdioma = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = new SecurityBl().AddModulo(nombre, idUsuario, idIdioma);
            }

            return Json(rp);
        }

        public JsonResult AddRoles(List<string> rolesToCreate)
        {
            var rp = new ResponseBasicVm();
            if (rolesToCreate == null || rolesToCreate.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = new SecurityBl().AddRoles(rolesToCreate, idUsuario);
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult AddPaginas(Dictionary<string, int> namesWithParentModule)
        {
            var rp = new ResponseBasicVm();
            if (namesWithParentModule.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idUsuario = Convert.ToInt32(Session["UserIdIdioma"]);
                var idIdioma = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = new SecurityBl().AddPaginas(namesWithParentModule, idUsuario, idIdioma);
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult AddUsersToRol(List<int> idUsuarios, short idRol)
        {
            var rp = new ResponseBasicVm();
            if (idUsuarios == null || idUsuarios.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)//Esto seria util validarlo a nivel de base controller para dejar de replicar bastante codigo en controllers
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                if (Session["IdRolSelected"] == null)
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se ha seleccionado un rol");//TODO
                }
                else
                {
                    int idCurrentUsuario = Convert.ToInt32(Session["UserId"]);
                    short idRolSelected = Convert.ToInt16(Session["IdRolSelected"]);
                    if (idRolSelected == idRol)
                    {
                        rp = new SecurityBl().AddUsersToRol(idUsuarios, idRolSelected, idCurrentUsuario);
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("No concuerda el rol enviado con el rol seteado en el servidor");
                    }
                }
            }
            return Json(rp);
        }

        public JsonResult CheckUserName(string userName)
        {
            return Json(new SecurityBl().CheckUserName(userName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckDataAdAvailable()
        {
            var rp = new ResponseBasicVm();
            if (System.Web.HttpContext.Current.Application["UsersUsa"] == null)
            {
                rp.Success = false;
            }
            else
            {
                rp.Success = true;
            }

            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckEmailAvailable(string email)
        {
            var rp = new ResponseBasicVm();
            rp = new SecurityBl().CheckEmail(email);
            return Json(rp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateModulos(List<ConfiguracionMenuTreeParentVm> modulosToUpdate)
        {
            var rp = new ResponseBasicVm();
            if (modulosToUpdate == null || modulosToUpdate.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                seguridadBl = new SecurityBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                var idIdioma = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = seguridadBl.UpdateModulos(modulosToUpdate, idUsuario, idIdioma);
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdatePaginas(List<ConfiguracionMenuTreeParentVm> modulosToUpdate)
        {
            var rp = new ResponseBasicVm();
            if (modulosToUpdate == null || modulosToUpdate.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                seguridadBl = new SecurityBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                var idIdioma = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = seguridadBl.UpdatePaginas(modulosToUpdate, idUsuario, idIdioma);
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateOpcionesRol(BasicBooleanVm opcionToUpdate, short idRol)
        {
            var rp = new ResponseBasicVm();
            if (opcionToUpdate == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)//Esto seria util validarlo a nivel de base controller para dejar de replicar bastante codigo en controllers
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                if (Session["IdRolSelected"] == null)
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se ha seleccionado un rol");//TODO
                }
                else
                {
                    int idCurrentUsuario = Convert.ToInt32(Session["UserId"]);
                    short idRolSelected = Convert.ToInt16(Session["IdRolSelected"]);
                    if (idRolSelected == idRol)
                    {
                        rp = new SecurityBl().UpdateOpcionRol(opcionToUpdate, idRolSelected, idCurrentUsuario);
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("No concuerda el rol enviado con el rol seteado en el servidor");
                    }
                }
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateUserAd(int idUser, short idRol, int idIdioma)
        {
            var rp = new ResponseBasicVm();
            if (idUser == 0 || idRol == 0 || idRol == 0 || idIdioma == 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idUsuarioRegistro = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = MemberShip.UpdateUserAd(idUser, idRol, idIdioma, idUsuarioRegistro);
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult UpdateUserThird(int idUser, short idRol, int idIdioma, string passWord, string email)
        {
            var rp = new ResponseBasicVm();
            if (idUser == 0 || idRol == 0 || idRol == 0 || email == string.Empty)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idUsuarioRegistro = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = MemberShip.UpdateUserThird(idUser, idRol, idIdioma, passWord, email, idUsuarioRegistro);
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult DeleteOpciones(List<int> opciones)
        {
            var rp = new ResponseBasicVm();
            if (opciones == null || opciones.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserIdIdioma"] == null || Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                seguridadBl = new SecurityBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                var idIdioma = Convert.ToInt32(Session["UserIdIdioma"]);
                rp = seguridadBl.DeleteOpciones(opciones, idUsuario, idIdioma);
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult DeleteRoles(List<int> roles)
        {
            var rp = new ResponseBasicVm();
            if (roles == null || roles.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                seguridadBl = new SecurityBl();
                var idUsuario = Convert.ToInt32(Session["UserId"]);
                rp = seguridadBl.DeleteRoles(roles, idUsuario);
            }
            return Json(rp);
        }

        [HttpPost]
        public JsonResult RemoveUsersFromRol(List<int> idUsuarios)
        {
            var rp = new ResponseBasicVm();
            if (idUsuarios == null || idUsuarios.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)//Esto seria util validarlo a nivel de base controller para dejar de replicar bastante codigo en controllers
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                if (Session["IdRolSelected"] == null)
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se ha seleccionado un rol");//TODO
                }
                else
                {
                    rp = new SecurityBl().RemoveUsersFromRole(idUsuarios);
                }
            }

            return Json(rp);
        }

        [HttpPost]
        public JsonResult DeleteUsers(List<int> idUsuarios)
        {
            var rp = new ResponseBasicVm();
            if (idUsuarios == null || idUsuarios.Count <= 0)
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjNoSeHanEnviadoParametros");
            }
            else if (Session["UserId"] == null)//Esto seria util validarlo a nivel de base controller para dejar de replicar bastante codigo en controllers
            {
                rp.Success = false;
                rp.MessageBad.Add("MsjSesionError");
            }
            else
            {
                var idCurrentUser = Convert.ToInt32(Session["UserId"]);
                rp = new SecurityBl().DeleteUsers(idUsuarios, idCurrentUser);
            }

            return Json(rp);
        }

        #endregion

        #region [Methods]
        #endregion
    }
}