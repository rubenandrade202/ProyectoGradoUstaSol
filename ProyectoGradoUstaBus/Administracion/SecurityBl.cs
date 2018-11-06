using ProyectoGradoUstaCommon;
using ProyectoGradoUstaSecurity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaBus
{
  public class SecurityBl
  {
    #region [FIELDS]
    ProyectoUstaEntities securityCtx;
    #endregion

    #region [CONSTRUCTOR]
    public SecurityBl()
    {
      securityCtx = new ProyectoUstaEntities();
    }
    #endregion

    #region [GET]
    public IQueryable<ConfiguracionMenuTreeParentVm> GetConfigurarOpcionesMenu(int idIdioma)
    {
      return (from opcionesParent in securityCtx.OpcionesProyectoUsta
              where opcionesParent.IdOpcionPadre == null
              select new ConfiguracionMenuTreeParentVm()
              {
                IdOpcion = opcionesParent.IdOpcion,
                IdOpcionPadre = null,
                NombreEspanol = opcionesParent.NombreEspanol,
                NombreIngles = opcionesParent.NombreIngles,
                Orden = opcionesParent.Orden,
                Controlador = opcionesParent.Controlador,
                Accion = opcionesParent.Accion,
                Tipo = opcionesParent.IdTipoOpcionHija,
                NombreTipo = null,
                Child = (from opcionesChild in securityCtx.OpcionesProyectoUsta
                         join tipoOpcion in securityCtx.TipoOpcionMenuProyectoUsta on opcionesChild.IdTipoOpcionHija equals tipoOpcion.IdTipo
                         where opcionesChild.IdOpcionPadre == opcionesParent.IdOpcion
                         select new ConfiguracionMenuTreeChildVm()
                         {
                           IdOpcion = opcionesChild.IdOpcion,
                           IdOpcionPadre = opcionesChild.IdOpcionPadre,
                           NombreEspanol = opcionesChild.NombreEspanol,
                           NombreIngles = opcionesChild.NombreIngles,
                           Orden = opcionesChild.Orden,
                           Controlador = opcionesChild.Controlador,
                           Accion = opcionesChild.Accion,
                           Tipo = (short)opcionesChild.IdTipoOpcionHija,
                           NombreTipo = idIdioma == 1 ? tipoOpcion.NombreEspañol : tipoOpcion.NombreIngles
                         }).OrderBy(x => x.Orden).ToList()
              }).OrderBy(x => x.NombreEspanol).AsQueryable();
    }

    public IQueryable<ConfiguracionMenuTreeParentVm> GetOpcionesMenuByRol(short idRol)
    {
      List<MenuTreeByRolFromSp> fuente = ((securityCtx as IObjectContextAdapter).ObjectContext).ExecuteStoreQuery<MenuTreeByRolFromSp>("GetMenuProyectoUstaByRol @IdRol",
                              new SqlParameter("@IdRol", idRol)).ToList();

      var some = (from opcionesParent in fuente
                  where opcionesParent.IdOpcionPadre == null
                  select new ConfiguracionMenuTreeParentVm()
                  {
                    IdOpcion = opcionesParent.IdOpcion,
                    IdOpcionPadre = opcionesParent.IdOpcionPadre,
                    NombreEspanol = opcionesParent.NombreEspanol,
                    NombreIngles = opcionesParent.NombreIngles,
                    //Orden = 0,//default value
                    //Controlador = "",//default value
                    //Accion = "",//default value
                    //Tipo = null,//default value
                    //NombreTipo = null,//default value
                    Seteo = opcionesParent.IdRol == null ? false : true,
                    IsParent = true,
                    //Seteo = opcionesParent.IdRol == null ? "Checked" : String.Empty,
                    Child = (from opcionesChild in fuente
                             where opcionesChild.IdOpcionPadre == opcionesParent.IdOpcion
                             select new ConfiguracionMenuTreeChildVm()
                             {
                               IdOpcion = opcionesChild.IdOpcion,
                               IdOpcionPadre = opcionesChild.IdOpcionPadre,
                               NombreEspanol = opcionesChild.NombreEspanol,
                               NombreIngles = opcionesChild.NombreIngles,
                               //Orden = 0,
                               //Controlador = "",
                               //Accion = "",
                               //Tipo = null,
                               //NombreTipo = "",
                               Seteo = opcionesChild.IdRol == null ? false : true,
                               IsParent = false
                             }).ToList()
                  }).OrderBy(x => x.NombreEspanol).ToList();

      return (from opcionesParent in fuente
              where opcionesParent.IdOpcionPadre == null
              select new ConfiguracionMenuTreeParentVm()
              {
                IdOpcion = opcionesParent.IdOpcion,
                IdOpcionPadre = opcionesParent.IdOpcionPadre,
                NombreEspanol = opcionesParent.NombreEspanol,
                NombreIngles = opcionesParent.NombreIngles,
                //Orden = 0,//default value
                //Controlador = "",//default value
                //Accion = "",//default value
                //Tipo = null,//default value
                //NombreTipo = null,//default value
                Seteo = opcionesParent.IdRol == null ? false : true,
                IsParent = true,
                //Seteo = opcionesParent.IdRol == null ? "Checked" : String.Empty,
                Child = (from opcionesChild in fuente
                         where opcionesChild.IdOpcionPadre == opcionesParent.IdOpcion
                         select new ConfiguracionMenuTreeChildVm()
                         {
                           IdOpcion = opcionesChild.IdOpcion,
                           IdOpcionPadre = opcionesChild.IdOpcionPadre,
                           NombreEspanol = opcionesChild.NombreEspanol,
                           NombreIngles = opcionesChild.NombreIngles,
                           //Orden = 0,
                           //Controlador = "",
                           //Accion = "",
                           //Tipo = null,
                           //NombreTipo = "",
                           Seteo = opcionesChild.IdRol == null ? false : true,
                           IsParent = false
                         }).ToList()
              }).OrderBy(x => x.NombreEspanol).AsQueryable();
    }

    public List<BasicVm> GetTiposPagina(int idIdioma)
    {
      var result = new List<BasicVm>();
      try
      {
        result = (from tipoOpciones in securityCtx.TipoOpcionMenuProyectoUsta
                  select new BasicVm()
                  {
                    Id = tipoOpciones.IdTipo,
                    Value = idIdioma == 1 ? tipoOpciones.NombreEspañol : tipoOpciones.NombreIngles
                  }).ToList();
      }
      catch (Exception e)
      {
        //TODO Log error administrador sistema  
      }
      return result;
    }

    public IQueryable<RolesIgVm> GetRoles()
    {
      var some = (from roles in securityCtx.RolesProyectoUsta
                  join usuariosSistemaCreador in securityCtx.UsuariosProyectoUsta on roles.IdUsuarioCreador equals usuariosSistemaCreador.Id
                  join usuariosSistemaModificador in securityCtx.UsuariosProyectoUsta on roles.IdUsuarioModificador equals usuariosSistemaModificador.Id into lfjA
                  from lfjB in lfjA.DefaultIfEmpty()
                  select new RolesIgVm()
                  {
                    IdRol = roles.IdRol,
                    NombreRol = roles.NombreRol,
                    NombreUsuarioCreador = usuariosSistemaCreador.UserName,
                    NombreUsuarioModificador = lfjB.UserName,
                    FechaModificado = roles.FechaModificado,
                    FechaRegistro = roles.FechaRegistro
                  }).OrderBy(x => x.NombreRol).ToList();


      return (from roles in securityCtx.RolesProyectoUsta
              join usuariosSistemaCreador in securityCtx.UsuariosProyectoUsta on roles.IdUsuarioCreador equals usuariosSistemaCreador.Id
              join usuariosSistemaModificador in securityCtx.UsuariosProyectoUsta on roles.IdUsuarioModificador equals usuariosSistemaModificador.Id into lfjA
              from lfjB in lfjA.DefaultIfEmpty()
              select new RolesIgVm()
              {
                IdRol = roles.IdRol,
                NombreRol = roles.NombreRol,
                NombreUsuarioCreador = usuariosSistemaCreador.UserName,
                NombreUsuarioModificador = lfjB.UserName,
                FechaModificado = roles.FechaModificado,
                FechaRegistro = roles.FechaRegistro
              }).OrderBy(x => x.NombreRol).AsQueryable();
    }

    public UsuariosProyectoUsta GetUserByUserName(string userName)
    {
      var objUsuario = new UsuariosProyectoUsta();
      try
      {
        objUsuario = securityCtx.UsuariosProyectoUsta.Where(x => x.UserName == userName).FirstOrDefault();
      }
      catch (Exception e)
      {
      }
      return objUsuario;
    }

    public InfoUsuarioLogOutVm GetInfoLogOutUsuario(int idusuario)
    {
      var result = new InfoUsuarioLogOutVm();
      try
      {
        result = (from usuarios in securityCtx.UsuariosProyectoUsta
                  join idiomas in securityCtx.IdiomasProyectoUsta on usuarios.IdIdioma equals idiomas.IdIdioma
                  join usuariosRol in securityCtx.UsuariosRolesProyectoUsta on usuarios.Id equals usuariosRol.IdUsuario
                  join roles in securityCtx.RolesProyectoUsta on usuariosRol.IdRol equals roles.IdRol
                  where usuarios.Id == idusuario
                  select new InfoUsuarioLogOutVm()
                  {
                    CurrentIdiom = idiomas.Nombre,
                    IdCurrentIdiom = idiomas.IdIdioma,
                    IdOptionIdiom = securityCtx.IdiomasProyectoUsta.Where(x => x.IdIdioma != idiomas.IdIdioma).Select(y => y.IdIdioma).FirstOrDefault(),
                    NombreOpcionIdiom = securityCtx.IdiomasProyectoUsta.Where(x => x.IdIdioma != idiomas.IdIdioma).Select(y => y.Nombre).FirstOrDefault(),
                    NombreRol = roles.NombreRol,
                    UserName = usuarios.UserName
                  }).FirstOrDefault();
      }
      catch (Exception e)
      {
        //log error en aplicacion y mail a administrador                
      }
      return result;
    }

    public IQueryable<BasicVm> GetUsuariosPorRol(short idRol)
    {
      return (from usuariosRol in securityCtx.UsuariosRolesProyectoUsta
              join usuarios in securityCtx.UsuariosProyectoUsta on usuariosRol.IdUsuario equals usuarios.Id
              where usuariosRol.IdRol == idRol //&& usuarios.Activo == true
              select new BasicVm()
              {
                Id = usuarios.Id,
                Value = usuarios.UserName
              }).OrderBy(x => x.Value).AsQueryable();
    }

    public IQueryable<BasicVm> GetUsuarioSistema()//TODO REEMPLAZAR CON EL PRINCIPAL
    {
      return (from usuarios in securityCtx.UsuariosProyectoUsta
              where usuarios.Activo == true
              select new BasicVm()
              {
                Id = usuarios.Id,
                Value = usuarios.UserName
              }).OrderBy(x => x.Value).AsQueryable();
    }

    public IQueryable<UsuariosProyectoUsta> GetUsuarios()
    {
      return securityCtx.UsuariosProyectoUsta.AsQueryable();
    }

    public IQueryable<UsuariosIgVm> GetUsuariosSistema()
    {
      return (from usuariosUsa in securityCtx.UsuariosProyectoUsta
              join usuariosRolUsa in securityCtx.UsuariosRolesProyectoUsta on usuariosUsa.Id equals usuariosRolUsa.IdUsuario
              join idiomas in securityCtx.IdiomasProyectoUsta on usuariosUsa.IdIdioma equals idiomas.IdIdioma
              join rolesUsa in securityCtx.RolesProyectoUsta on usuariosRolUsa.IdRol equals rolesUsa.IdRol
              where usuariosUsa.Activo == true
              select new UsuariosIgVm()
              {
                Id = usuariosUsa.Id,
                Email = usuariosUsa.Email,
                FechaModificado = usuariosUsa.FechaModificado,
                FechaRegistro = usuariosUsa.FechaRegistro,
                IdIdioma = usuariosUsa.IdIdioma,
                IdRol = usuariosRolUsa.IdRol,
                NombreIdioma = idiomas.Nombre,
                NombreRol = rolesUsa.NombreRol,
                UserName = usuariosUsa.UserName,
                UsuarioElite = usuariosUsa.UsuarioEmpresa,
                MenuEspecifico = usuariosRolUsa.MenuEspecifico
              }).OrderBy(x => x.UserName).AsQueryable();
    }

    public List<BigMenuParentVm> GetMenuStructureByUser(int idUser, int idIdioma)
    {
      var rawRecords = (from usuarios in securityCtx.UsuariosProyectoUsta
                        join usuarioRol in securityCtx.UsuariosRolesProyectoUsta on usuarios.Id equals usuarioRol.IdUsuario
                        join opcionesRol in securityCtx.OpcionesRolProyectoUsta on usuarioRol.IdRol equals opcionesRol.IdRol
                        join opciones in securityCtx.OpcionesProyectoUsta on opcionesRol.IdOpcion equals opciones.IdOpcion
                        join tipoOpciones in securityCtx.TipoOpcionMenuProyectoUsta on opciones.IdTipoOpcionHija equals tipoOpciones.IdTipo into leftJoinA
                        where usuarios.Id == idUser
                        from x in leftJoinA.DefaultIfEmpty()
                        select new
                        {
                          opciones.IdOpcion,
                          opciones.IdOpcionPadre,
                          Nombre = idIdioma == 1 ? opciones.NombreIngles : opciones.NombreEspanol,
                          Controlador = opciones.Controlador,
                          Accion = opciones.Accion,
                          opciones.IdTipoOpcionHija,
                          NombreTipoOpcion = idIdioma == 1 ? x.NombreIngles : x.NombreEspañol,
                          Orden = opciones.Orden
                        }).ToList();

      var lst = new List<BigMenuParentVm>();
      lst = rawRecords.Where(x => x.IdOpcionPadre == null).Select(x => new BigMenuParentVm()
      {
        IdOpcion = x.IdOpcion,
        Nombre = x.Nombre,
        Orden = x.Orden,
        ChildConfiguration = rawRecords.Where(y => y.IdOpcionPadre == x.IdOpcion && y.IdTipoOpcionHija == 1).Select(z => new BigMenuChildVm()
        {
          Accion = z.Accion,
          Controlador = z.Controlador,
          IdOpcion = z.IdOpcion,
          IdTipoOpcionHija = z.IdTipoOpcionHija,
          Nombre = z.Nombre,
          NombreTipoOpcion = z.NombreTipoOpcion,
          Orden = z.Orden
        }).OrderBy(l => l.IdTipoOpcionHija).ThenBy(m => m.Orden).ToList(),
        ChildProcess = rawRecords.Where(y => y.IdOpcionPadre == x.IdOpcion && y.IdTipoOpcionHija == 2).Select(z => new BigMenuChildVm()
        {
          Accion = z.Accion,
          Controlador = z.Controlador,
          IdOpcion = z.IdOpcion,
          IdTipoOpcionHija = z.IdTipoOpcionHija,
          Nombre = z.Nombre,
          NombreTipoOpcion = z.NombreTipoOpcion,
          Orden = z.Orden
        }).OrderBy(l => l.IdTipoOpcionHija).ThenBy(m => m.Orden).ToList(),
        ChildReports = rawRecords.Where(y => y.IdOpcionPadre == x.IdOpcion && y.IdTipoOpcionHija == 3).Select(z => new BigMenuChildVm()
        {
          Accion = z.Accion,
          Controlador = z.Controlador,
          IdOpcion = z.IdOpcion,
          IdTipoOpcionHija = z.IdTipoOpcionHija,
          Nombre = z.Nombre,
          NombreTipoOpcion = z.NombreTipoOpcion,
          Orden = z.Orden
        }).OrderBy(l => l.IdTipoOpcionHija).ThenBy(m => m.Orden).ToList()

      }).OrderBy(x => x.Orden).ToList();
      return lst;
    }

    #endregion

    #region [ADD]
    public ResponseBasicVm AddModulo(string name, int idUsuario, int idIdioma)
    {
      var rp = new ResponseBasicVm();
      try
      {
        name = name.ToUpper();
        var record = securityCtx.OpcionesProyectoUsta.Where(x => x.NombreEspanol == name ||
                                                    x.NombreIngles == name).FirstOrDefault();
        if (record == null)
        {
          var newRec = new OpcionesProyectoUsta();
          newRec.IdUsuarioCreador = idUsuario;
          newRec.NombreEspanol = idIdioma == 1 ? name : "No Asignado";
          newRec.NombreIngles = idIdioma == 2 ? name : "No Set";
          newRec.Orden = 0;
          securityCtx.OpcionesProyectoUsta.Add(newRec);
          securityCtx.SaveChanges();
          rp.Success = true;
          rp.MessageOk.Add("RegistroIngresado");
          rp.Data = newRec;
        }
        else
        {
          rp.Success = false;
          rp.MessageOk.Add("MsjCamposNoDisponibles");
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm AddPaginas(Dictionary<string, int> pagesWithModuleParent, int idUsuario, int idIdioma)
    {
      var rp = new ResponseBasicVm();
      var tranToDo = false;
      var dataToRespond = new Dictionary<string, object>();
      try
      {
        foreach (var pageAndModule in pagesWithModuleParent)
        {
          var existente = securityCtx.OpcionesProyectoUsta.Where(x => x.NombreEspanol == pageAndModule.Key.ToUpper()
                                                      || x.NombreIngles == pageAndModule.Key.ToUpper()).FirstOrDefault();
          if (existente == null)
          {
            tranToDo = true;
            var newPagina = new OpcionesProyectoUsta();
            newPagina.IdUsuarioCreador = idUsuario;
            newPagina.NombreEspanol = idIdioma == 1 ? pageAndModule.Key.ToUpper() : "NoAsignado";
            newPagina.NombreIngles = idIdioma == 2 ? pageAndModule.Key.ToUpper() : "NoAsignado";
            newPagina.IdOpcionPadre = (short)pageAndModule.Value;
            newPagina.Orden = 1;
            newPagina.IdTipoOpcionHija = 1;
            newPagina.FechaRegistro = DateTime.Now;
            securityCtx.OpcionesProyectoUsta.Add(newPagina);
            securityCtx.SaveChanges();
            rp.MessageOk.Add("RegistroIngresado");
            dataToRespond.Add(pageAndModule.Key, newPagina);
          }
          else
          {
            rp.Alert = true;
            rp.MessageAlert.Add(string.Format("{0} {1}", pageAndModule.Key, "DatoYaExiste"));
          }

          if (tranToDo)
          {
            rp.Success = true;
            rp.Data = dataToRespond;
          }
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm AddRoles(List<string> namesCandidate, int idUsuario)
    {
      var rp = new ResponseBasicVm();
      var actualNames = new List<string>();
      var actualRecords = new List<RolesProyectoUsta>();
      try
      {
        namesCandidate.ForEach(x => x.ToUpper());
        var existentesDb = securityCtx.RolesProyectoUsta.Where(x => namesCandidate.Contains(x.NombreRol)).Select(y => y.NombreRol).ToList();
        if (existentesDb.Count > 0)
        {
          actualNames = namesCandidate.Except(existentesDb).ToList();
          rp.Alert = true;
          foreach (var existenteName in existentesDb)
          {
            rp.MessageAlert.Add(existenteName + " " + "DatoYaExiste");
          }
        }
        else
        {
          actualNames = namesCandidate;
        }

        foreach (var nameToCreate in actualNames)
        {
          var curRol = new RolesProyectoUsta();
          curRol.IdEstado = true;
          curRol.IdUsuarioCreador = idUsuario;
          curRol.FechaModificado = DateTime.Now;
          curRol.FechaRegistro = DateTime.Now;
          curRol.IdUsuarioModificador = idUsuario;
          curRol.NombreRol = nameToCreate.ToUpper();
          actualRecords.Add(curRol);
        }

        if (actualRecords.Count > 0)
        {
          rp.Success = true;
          securityCtx.RolesProyectoUsta.AddRange(actualRecords);
          securityCtx.SaveChanges();
          foreach (var actualRecord in actualRecords)
          {
            rp.MessageOk.Add(actualRecord.NombreRol + " " + "RegistroIngresado");
          }
        }
        else
        {
          rp.MessageBad.Add("RegistroNoIngresado");
          rp.Success = false;
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm AddUsersToRol(List<int> idUsuarios, short idRol, int idUsuarioRegistro)
    {
      var rp = new ResponseBasicVm();
      try
      {
        var existentesDb = securityCtx.UsuariosRolesProyectoUsta.Where(x => idUsuarios.Contains(x.IdUsuario)).ToList();
        securityCtx.UsuariosRolesProyectoUsta.RemoveRange(existentesDb);
        securityCtx.SaveChanges();

        var newCollection = new List<UsuariosRolesProyectoUsta>();
        //TODO validar que la entrada de datos corresponda a ID's de usuarios reales sin utilizar DB simplemente con logica BL
        foreach (var idUsuario in idUsuarios)
        {
          var newUsuarioRol = new UsuariosRolesProyectoUsta()
          {
            FechaRegistro = DateTime.Now,
            IdRol = idRol,
            IdUsuario = idUsuario,
            IdUsuarioRegistro = idUsuarioRegistro,
            MenuEspecifico = false
          };
          newCollection.Add(newUsuarioRol);
        }

        if (newCollection.Count > 0)
        {
          rp.Success = true;
          securityCtx.UsuariosRolesProyectoUsta.AddRange(newCollection);
          securityCtx.SaveChanges();
          rp.MessageOk.Add("RegistroIngresado");
        }
        else
        {
          rp.Success = false;
          rp.MessageBad.Add("No fueron agregados");//todo
        }

      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

        //public ResponseBasicVm AddUsersToSystem(List<string> userNames, short idRol, int idIdioma, bool usuarioElite,
        //                                        int idUsuarioRegistro, WebSecurity webSecurity, List<UsuariosAdVm> usuariosAd)
        //{
        //    var rp = new ResponseBasicVm();
        //    try
        //    {
        //        var currentUsers = securityCtx.UsuariosUsa.ToList();
        //        var newOnes = userNames.Where(x => !currentUsers.Select(y => y.UserName).Contains(x)).ToList();
        //        var oldOnes = userNames.Where(x => currentUsers.Where(z => z.Activo == false).Select(y => y.UserName).Contains(x)).ToList();
        //        var notAllowed = userNames.Where(x => currentUsers.Where(z => z.Activo == true).Select(y => y.UserName).Contains(x)).ToList();

        //        if (newOnes.Count > 0)
        //        {
        //            var lstNewOnes = new List<UsuariosUsa>();
        //            foreach (var userNameNewOne in newOnes)
        //            {
        //                //var newOne = new UsuariosUsa()
        //                //{
        //                //    Activo = true,
        //                //    Email = usuariosAd.Where(x => x.UserName == userNameNewOne).FirstOrDefault().Mail,
        //                //    FechaRegistro = DateTime.Now,
        //                //    IdIdioma = (byte)idIdioma,
        //                //    IdUsuarioRegistro = idUsuarioRegistro,
        //                //    UserName

        //                //};
        //            }
        //        }

        //        if (usuarioElite)
        //        {

        //        }
        //        else
        //        {

        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        rp.Success = false;
        //        rp.MessageBad.Add(e.ToString());
        //    }
        //    return rp;
        //}


    //public ResponseBasicVm GenerarBkpDbFile()
    public async Task<ResponseBasicVm> GenerarBkpDbFile()
    {
      var rp = new ResponseBasicVm();
      try
      {
        //string dbname = securityCtx.Database.Connection.Database;
        var pathFile = AppDomain.CurrentDomain.BaseDirectory + "BkpFiles\\" + "BkpProyecto" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bak";
        string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH FORMAT,MEDIANAME = 'BkpProyecto', NAME = 'Full Backup of ProyectoUsta'";
        int responseSql = await securityCtx.Database.ExecuteSqlCommandAsync(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, string.Format(sqlCommand, securityCtx.Database.Connection.Database, pathFile));
        if(responseSql == -1)
        {
          rp.Success = true;
          rp.Data = pathFile;
        }  
        else
        {
          rp.Success = false;
          rp.MessageBad.Add("Sql server respondió que pailas");
        }      
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }
    #endregion

    #region [UPDATE]

    public ResponseBasicVm UpdateModulos(List<ConfiguracionMenuTreeParentVm> modulosToUpdate, int idUsuario, int idIdioma)
    {
      var rp = new ResponseBasicVm();
      var tranToDo = false;
      try
      {
        var idModulosToUpdate = modulosToUpdate.Select(y => y.IdOpcion).ToList();
        var records = securityCtx.OpcionesProyectoUsta.Where(x => idModulosToUpdate.Contains(x.IdOpcion)).ToList();
        if (records.Count <= 0)
        {
          rp.Success = false;
          rp.MessageBad.Add("NoSeEncontraronDatos");
        }
        else
        {
          foreach (OpcionesProyectoUsta opcionUsaDb in records)
          {
            var updateData = modulosToUpdate.Where(x => x.IdOpcion == opcionUsaDb.IdOpcion).FirstOrDefault();
            var recordNameExistente = securityCtx.OpcionesProyectoUsta.Where(x => (x.NombreEspanol == updateData.NombreEspanol.ToUpper()
                                                                    || x.NombreIngles == updateData.NombreIngles.ToUpper())
                                                                    && x.IdOpcion != opcionUsaDb.IdOpcion).FirstOrDefault();
            if (recordNameExistente == null)
            {
              tranToDo = true;
              opcionUsaDb.NombreEspanol = updateData.NombreEspanol.ToUpper();
              opcionUsaDb.NombreIngles = updateData.NombreIngles.ToUpper();
              opcionUsaDb.Orden = updateData.Orden;
              opcionUsaDb.IdUsuarioModificador = idUsuario;
              opcionUsaDb.FechaModificado = DateTime.Now;
            }
            else
            {
              rp.Alert = true;
              rp.MessageAlert.Add(string.Format("{0} {1}", (idIdioma == 1) ? updateData.NombreEspanol : updateData.NombreIngles, ".DatoYaExiste"));
            }
          }

          if (tranToDo)
          {
            securityCtx.SaveChanges();
            rp.Success = true;
            rp.MessageOk.Add("RegistroIngresado");
          }
          else
          {
            rp.Success = false;
            rp.MessageBad.Add("RegistroNoIngresado");
          }
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm UpdatePaginas(List<ConfiguracionMenuTreeParentVm> modulosToUpdate, int idUsuario, int idIdioma)
    {
      var rp = new ResponseBasicVm();
      var tranToDo = false;
      try
      {
        var idModulosToUpdate = modulosToUpdate.Select(y => y.IdOpcion).ToList();
        var records = securityCtx.OpcionesProyectoUsta.Where(x => idModulosToUpdate.Contains(x.IdOpcion)).ToList();
        if (records.Count <= 0)
        {
          rp.Success = false;
          rp.MessageBad.Add("NoSeEncontraronDatos");
        }
        else
        {
          foreach (OpcionesProyectoUsta opcionUsaDb in records)
          {
            var updateData = modulosToUpdate.Where(x => x.IdOpcion == opcionUsaDb.IdOpcion).FirstOrDefault();
            var recordNameExistente = securityCtx.OpcionesProyectoUsta.Where(x => (x.NombreEspanol == updateData.NombreEspanol.ToUpper()
                                                                    || x.NombreIngles == updateData.NombreIngles.ToUpper())
                                                                    && x.IdOpcion != opcionUsaDb.IdOpcion).FirstOrDefault();
            if (recordNameExistente == null)
            {
              tranToDo = true;
              opcionUsaDb.NombreEspanol = updateData.NombreEspanol;
              opcionUsaDb.NombreIngles = updateData.NombreIngles;
              opcionUsaDb.Controlador = updateData.Controlador;
              opcionUsaDb.Accion = updateData.Accion;
              opcionUsaDb.IdTipoOpcionHija = updateData.Tipo;
              opcionUsaDb.Orden = updateData.Orden;
              opcionUsaDb.IdUsuarioModificador = idUsuario;
              opcionUsaDb.FechaModificado = DateTime.Now;
            }
            else
            {
              rp.Alert = true;
              rp.MessageAlert.Add(string.Format("{0} {1}", (idIdioma == 1) ? updateData.NombreEspanol + "DatoYaExiste"
                                                                          : updateData.NombreIngles + "DatoYaExiste"));
            }
          }

          if (tranToDo)
          {
            securityCtx.SaveChanges();
            rp.Success = true;
            rp.MessageOk.Add("RegistroIngresado");
          }
          else
          {
            rp.Success = false;
            rp.MessageBad.Add("RegistroNoIngresado");
          }
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm UpdateRoles(List<BasicVm> rolesToUpdatePrm, int idUsuario)
    {
      var rp = new ResponseBasicVm();
      var notExistent = new List<BasicVm>();
      var tranToDo = false;
      try
      {
        //UpperCase a todos los nombres enviados por el cliente
        rolesToUpdatePrm.ForEach(x => x.Value = x.Value.ToUpper());
        var idRolesToUpdate = rolesToUpdatePrm.Select(x => x.Id).ToList();
        //Se traen a memoria todos los registros encontrados en BD a actualizar
        var oldRecords = securityCtx.RolesProyectoUsta.Where(x => idRolesToUpdate.Contains(x.IdRol)).ToList();
        //Se valida que exista por lo menos uno a procesar
        if (oldRecords.Count > 0)
        {
          //ahora registramos los no existentes en una lista temporal para informar al consumidor de la BL
          notExistent = rolesToUpdatePrm.Where(x => !oldRecords.Select(y => y.IdRol).Contains((short)x.Id)).Select(z => new BasicVm() { Id = z.Id, Value = z.Value }).ToList();
          //Iteramos cada uno de los objetos traidos por EF a actualizar
          foreach (var oldRole in oldRecords)
          {
            // Ajustamos los nuevos datos para el registro
            var newData = rolesToUpdatePrm.Where(x => x.Id == oldRole.IdRol).FirstOrDefault();

            //Verificamos que no se intente actualizar con un nombre que ya tenga asignado otro rol
            if (securityCtx.RolesProyectoUsta.Where(x => x.IdRol != oldRole.IdRol && x.NombreRol == newData.Value).Count() > 0)
            {
              //
              rp.Alert = true;
              rp.MessageAlert.Add(string.Format("{0} {1}", newData.Value, "DatoYaExiste"));
            }
            else
            {
              tranToDo = true;
              oldRole.NombreRol = newData.Value;
              oldRole.IdUsuarioModificador = idUsuario;
              oldRole.FechaModificado = DateTime.Now;
              rp.MessageOk.Add(string.Format("{0} {1}", newData.Value, "RegistroIngresado"));
            }
          }

          if (tranToDo)
          {
            rp.Success = true;
            securityCtx.SaveChanges();
          }
          else
          {
            rp.Success = false;
            rp.MessageBad.Add("RegistroNoIngresado");
          }

          //informamos de registros enviados que no se encontraban en BD
          foreach (var itemNotExist in notExistent)
          {
            rp.Alert = true;
            rp.MessageAlert.Add(string.Format("{0} {1}", itemNotExist.Id, ".DatoNoExiste"));
          }
        }
        else
        {
          rp.Success = false;
          rp.MessageBad.Add("DatoNoExiste");
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm UpdateOpcionRol(BasicBooleanVm configToUpdate, short idRole, int idUsuario)
    {
      var rp = new ResponseBasicVm();
      var idDependientes = new List<short>();
      var lstNewRecords = new List<OpcionesRolProyectoUsta>();
      var lstOldRecords = new List<OpcionesRolProyectoUsta>();
      try
      {
        if (configToUpdate.ItIs)
        {
          idDependientes = securityCtx.OpcionesProyectoUsta.Where(x => x.IdOpcionPadre == (short)configToUpdate.Id).Select(y => y.IdOpcion).ToList();
          //crear
          if (configToUpdate.State)
          {
            //borrar antiguos
            lstOldRecords = securityCtx.OpcionesRolProyectoUsta.Where(x => idDependientes.Contains(x.IdOpcion) && x.IdRol == idRole).ToList();
            if (lstOldRecords.Count > 0)
            {
              securityCtx.OpcionesRolProyectoUsta.RemoveRange(lstOldRecords);
              securityCtx.SaveChanges();
            }
            //crear todos en bloque
            idDependientes.ForEach(x => lstNewRecords.Add(new OpcionesRolProyectoUsta()
            {
              IdOpcion = x,
              Actualizar = false,
              Consultar = false,
              Exportar = false,
              Imprimir = false,
              FechaRegistro = DateTime.Now,
              IdRol = idRole,
              IdUsuarioCreador = idUsuario,
            }));

            lstNewRecords.Add(new OpcionesRolProyectoUsta()
            {
              IdOpcion = (short)configToUpdate.Id,
              Actualizar = false,
              Consultar = false,
              Exportar = false,
              Imprimir = false,
              FechaRegistro = DateTime.Now,
              IdRol = idRole,
              IdUsuarioCreador = idUsuario,
            });

            securityCtx.OpcionesRolProyectoUsta.AddRange(lstNewRecords);
            securityCtx.SaveChanges();
            rp.Success = true;
            rp.MessageOk.Add("RegistroIngresado");

          }
          else//eliminar
          {
            lstOldRecords = securityCtx.OpcionesRolProyectoUsta.Where(x => idDependientes.Contains(x.IdOpcion) && x.IdRol == idRole).ToList();
            var oldRecord = securityCtx.OpcionesRolProyectoUsta.Where(x => x.IdOpcion == configToUpdate.Id && x.IdRol == idRole).FirstOrDefault();
            securityCtx.OpcionesRolProyectoUsta.RemoveRange(lstOldRecords);
            securityCtx.OpcionesRolProyectoUsta.Remove(oldRecord);
            securityCtx.SaveChanges();
            rp.Success = true;
            rp.MessageOk.Add("EliminacionCorrecta");
          }
        }
        else
        {
          //crear
          if (configToUpdate.State)
          {
            //Se verifica si es una pagina lo que se esta ingresando
            if (!configToUpdate.ItIs)
            {
              var recordParent = securityCtx.OpcionesProyectoUsta.Where(x => x.IdOpcion == configToUpdate.Id).FirstOrDefault();
              var opcionRecordParentRol = new OpcionesRolProyectoUsta()
              {
                IdOpcion = (short)recordParent.IdOpcionPadre,
                Actualizar = false,
                Consultar = false,
                Exportar = false,
                Imprimir = false,
                FechaRegistro = DateTime.Now,
                IdRol = idRole,
                IdUsuarioCreador = idUsuario
              };
              //chequeo de existencia por posible velocidad en UI
              if (securityCtx.OpcionesRolProyectoUsta.Where(x => x.IdOpcion == opcionRecordParentRol.IdOpcion &&
                                             x.IdRol == opcionRecordParentRol.IdRol).Count() == 0)
              {
                securityCtx.OpcionesRolProyectoUsta.Add(opcionRecordParentRol);
                securityCtx.SaveChanges();
              }
            }


            var newOpcionRol = new OpcionesRolProyectoUsta()
            {
              IdOpcion = (short)configToUpdate.Id,
              Actualizar = false,
              Consultar = false,
              Exportar = false,
              Imprimir = false,
              FechaRegistro = DateTime.Now,
              IdRol = idRole,
              IdUsuarioCreador = idUsuario,
            };

            //chequeo de existencia por posible velocidad en UI
            if (securityCtx.OpcionesRolProyectoUsta.Where(x => x.IdOpcion == newOpcionRol.IdOpcion &&
                                                x.IdRol == newOpcionRol.IdRol).Count() == 0)
            {
              securityCtx.OpcionesRolProyectoUsta.Add(newOpcionRol);
              securityCtx.SaveChanges();
            }
            rp.Success = true;
            rp.MessageOk.Add("RegistroIngresado");
          }
          else//eliminar
          {
            var opcionToDelete = securityCtx.OpcionesRolProyectoUsta.Where(x => x.IdOpcion == configToUpdate.Id && x.IdRol == idRole).FirstOrDefault();
            if (opcionToDelete != null)
            {
              securityCtx.OpcionesRolProyectoUsta.Remove(opcionToDelete);
              securityCtx.SaveChanges();
            }
            rp.Success = true;
            rp.MessageOk.Add("EliminacionCorrecta");
          }
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm UpdateUsuarios()
    {
      var rp = new ResponseBasicVm();
      try
      {
        securityCtx.SaveChanges();
        rp.Success = true;
        rp.MessageOk.Add("ElRegistroFueActualizadoCorrectamente");
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    #endregion

    #region [DELETE]
    public ResponseBasicVm DeleteOpciones(List<int> opciones, int idUsuario, int idIdioma)
    {
      var rp = new ResponseBasicVm();
      var tranToDo = false;
      try
      {
        var records = securityCtx.OpcionesProyectoUsta.Where(x => opciones.Contains(x.IdOpcion)).ToList();
        if (records.Count <= 0)
        {
          rp.Success = false;
          rp.MessageBad.Add("NoSeEncontraronDatos");
        }
        else
        {
          foreach (var opcionUsa in records)
          {
            //Chequeo de dependencias
            var depRol = securityCtx.OpcionesRolProyectoUsta.Where(x => x.IdOpcion == opcionUsa.IdOpcion).FirstOrDefault();
            var depUsuario = securityCtx.OpcionesUsuarioProyectoUsta.Where(x => x.IdOpcion == opcionUsa.IdOpcion).FirstOrDefault();
            if (depRol == null && depUsuario == null)
            {
              tranToDo = true;
              var recordsDependientesToDelete = securityCtx.OpcionesProyectoUsta.Where(x => x.IdOpcionPadre == opcionUsa.IdOpcion).ToList();
              securityCtx.OpcionesProyectoUsta.RemoveRange(recordsDependientesToDelete);
              securityCtx.OpcionesProyectoUsta.Remove(opcionUsa);
              rp.MessageOk.Add(idIdioma == 1 ? opcionUsa.NombreEspanol + "EliminacionCorrecta"
                                              : opcionUsa.NombreIngles + "EliminacionCorrecta");
            }
            else
            {
              rp.Alert = true;
              rp.MessageBad.Add(idIdioma == 1 ? opcionUsa.NombreEspanol + "MsjNoEliminadoPorDependiente"
                                              : opcionUsa.NombreIngles + "MsjNoEliminadoPorDependiente");
            }
          }

          if (tranToDo)
          {
            securityCtx.SaveChanges();
            rp.Success = true;
          }
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm DeleteRoles(List<int> roles, int idUsuario)
    {
      var rp = new ResponseBasicVm();
      var tranToDo = false;
      try
      {
        var records = securityCtx.RolesProyectoUsta.Where(x => roles.Contains(x.IdRol)).ToList();
        if (records.Count <= 0)
        {
          rp.Success = false;
          rp.MessageBad.Add("NoSeEncontraronDatos");
        }
        else
        {
          foreach (var rolUsa in records)
          {
            //Chequeo de dependencias
            var depRolUsuario = securityCtx.UsuariosRolesProyectoUsta.Where(x => x.IdRol == rolUsa.IdRol).FirstOrDefault();
            var depOpcionesRol = securityCtx.UsuariosRolesProyectoUsta.Where(x => x.IdRol == rolUsa.IdRol).FirstOrDefault();
            if (depRolUsuario == null && depOpcionesRol == null)
            {
              tranToDo = true;
              securityCtx.RolesProyectoUsta.Remove(rolUsa);
              rp.MessageOk.Add(rolUsa.NombreRol + "EliminacionCorrecta");
            }
            else
            {
              rp.Alert = true;
              rp.MessageBad.Add(rolUsa.NombreRol + "MsjNoEliminadoPorDependiente");
            }
          }
          if (tranToDo)
          {
            securityCtx.SaveChanges();
            rp.Success = true;
          }
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm RemoveUsersFromRole(List<int> usuarios)
    {
      var rp = new ResponseBasicVm();
      try
      {
        var records = securityCtx.UsuariosRolesProyectoUsta.Where(x => usuarios.Contains(x.IdUsuario)).ToList();
        if (records.Count <= 0)
        {
          rp.Success = false;
          rp.MessageBad.Add("NoSeEncontraronDatos");
        }
        else
        {
          rp.Success = true;
          records.ForEach(x => rp.MessageOk.Add(x.UsuariosProyectoUsta.UserName + " " + "EliminacionCorrecta"));   //rp.MessageOk.Add()
          securityCtx.UsuariosRolesProyectoUsta.RemoveRange(records);
          securityCtx.SaveChanges();
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm DeleteUsers(List<int> idUsuarios, int idUsuarioRegistro)
    {
      var rp = new ResponseBasicVm();
      try
      {
        var currentJobUsers = securityCtx.UsuariosProyectoUsta.Where(x => idUsuarios.Contains(x.Id)).ToList();
        //la idea aca es poder eliminar los usuarios que no tengan niguna dependencia con el sistema
        //en general bien sea como haber creado algun registro o haber modificado algun registro y tenga
        //su id comprometido con el historial actual de la BD. La idea es hacerlo en un SP mandando
        //los id en cuestión y que el sp responda cuales se pueden eliminar definitivamente de la tabla 
        //usuarios y cuales se dejan inactivados para conservar el historial de esos resgistros
        //por ahora se desactivaran todos

        currentJobUsers.ForEach(x =>
        {
          x.Activo = false;
          x.FechaModificado = DateTime.Now;
          x.IdUsuarioModificador = idUsuarioRegistro;
          rp.MessageOk.Add(x.UserName + " " + "EliminacionCorrecta");
        });
        securityCtx.SaveChanges();
        rp.Success = true;
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }
    #endregion

    #region [CHECK]
    public ResponseBasicVm CheckModuloName(string name)
    {
      var rp = new ResponseBasicVm();
      try
      {
        name = name.ToUpper();
        var record = securityCtx.OpcionesProyectoUsta.Where(x => x.NombreEspanol == name ||
                                                    x.NombreIngles == name).FirstOrDefault();
        if (record == null)
        {
          rp.Success = true;
          rp.MessageOk.Add("MsjCamposDisponibles");
        }
        else
        {
          rp.Success = false;
          rp.MessageOk.Add("MsjCamposNoDisponibles");
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    /// <summary>
    /// Check if UserName exists
    /// True: User exists
    /// False: User does not exist
    /// En caso de que el usuario exista se envia en rp.Data type UsuariosProyectoUsta
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public ResponseBasicVm CheckUserName(string userName)
    {
      var rp = new ResponseBasicVm();
      try
      {
        var rsl = securityCtx.UsuariosProyectoUsta.Where(x => x.UserName == userName).FirstOrDefault();
        if (rsl == null)
        {
          rp.Success = false;
          rp.MessageOk.Add("Nombre de usuario no existe y esta libre para su uso.");
        }
        else
        {
          rp.Data = rsl;
          rp.Success = true;
          rp.MessageBad.Add("Nombre de usuario existe en la BD, no esta libre para su uso.");
        }
      }
      catch (Exception e)
      {
        rp.Success = false;
        rp.MessageBad.Add(e.ToString());
      }
      return rp;
    }

    public ResponseBasicVm CheckEmail(string email)
    {
      email = email.ToLower();
      var rp = new ResponseBasicVm();
      if (securityCtx.UsuariosProyectoUsta.Where(x => x.Email == email).FirstOrDefault() == null)
      {
        rp.Success = false;
        rp.MessageBad.Add("Email already exists in database");
      }
      else
      {
        rp.Success = true;
        rp.MessageOk.Add("Email is free to use");
      }
      return rp;
    }
    #endregion
  }
}
