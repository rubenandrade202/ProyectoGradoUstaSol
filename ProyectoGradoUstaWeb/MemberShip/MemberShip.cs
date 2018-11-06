using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoGradoUstaSecurity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace ProyectoGradoUstaWeb
{
    public class MemberShip
    {
        public static ResponseBasicVm SignUpUsersAd(List<string> userNames, short idRol, int idIdioma,
                                                int idUsuarioRegistro, List<UsuariosAdVm> usuariosAd)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var seguridadBl = new SecurityBl();
                var currentUsers = seguridadBl.GetUsuarios().ToList();
                var newOnes = userNames.Where(x => !currentUsers.Select(y => y.UserName).Contains(x)).ToList();
                var oldOnes = userNames.Where(x => currentUsers.Where(z => z.Activo == false).Select(y => y.UserName).Contains(x)).ToList();
                var notAllowed = userNames.Where(x => currentUsers.Where(z => z.Activo == true).Select(y => y.UserName).Contains(x)).ToList();

                //Nuevos
                if (newOnes.Count > 0)
                {
                    var lstNewUsuarioRoles = new List<UsuariosRolesProyectoUsta>();
                    foreach (var userNameNew in newOnes)
                    {
                        WebSecurity.CreateUserAndAccount(userNameNew, Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8)
                                                                    , new
                                                                    {
                                                                        IdIdioma = idIdioma,
                                                                        Email = usuariosAd.Where(x => x.UserName == userNameNew).FirstOrDefault().Mail,
                                                                        IdUsuarioRegistro = idUsuarioRegistro,
                                                                        UsuarioEmpresa = true
                                                                    });
                    }

                    var justEnteredIds = seguridadBl.GetUsuarios().Where(x => newOnes.Contains(x.UserName)).Select(y => y.Id).ToList();
                    var rpRolesUsers = seguridadBl.AddUsersToRol(justEnteredIds, idRol, idUsuarioRegistro);
                    if (!rpRolesUsers.Success)
                    {
                        rp.Alert = true;
                        rpRolesUsers.MessageAlert.ForEach(x => rp.MessageAlert.Add(x));
                        rpRolesUsers.MessageBad.ForEach(x => rp.MessageBad.Add(x));
                    }
                    rp.Success = true;
                    newOnes.ForEach(x => rp.MessageOk.Add(x + " " + "RegistroIngresado"));
                }

                if (oldOnes.Count > 0)
                {
                    var usersToUpdate = currentUsers.Where(x => oldOnes.Contains(x.UserName)).ToList();
                    usersToUpdate.ForEach(x =>
                    {
                        x.Activo = true;
                        x.Email = usuariosAd.Where(y => y.UserName == x.UserName).FirstOrDefault().Mail;
                        x.FechaModificado = DateTime.Now;
                        x.IdIdioma = (byte)idIdioma;
                        x.IdUsuarioModificador = idUsuarioRegistro;
                    });
                    var rpUpdateUsers = seguridadBl.UpdateUsuarios();
                    if (!rpUpdateUsers.Success)
                    {
                        rp.Alert = true;
                        rpUpdateUsers.MessageAlert.ForEach(x => rp.MessageAlert.Add(x));
                        rpUpdateUsers.MessageBad.ForEach(x => rp.MessageBad.Add(x));
                    }
                    var rpRolesUsers = seguridadBl.AddUsersToRol(usersToUpdate.Select(x => x.Id).ToList(), idRol, idUsuarioRegistro);
                    if (!rpRolesUsers.Success)
                    {
                        rp.Alert = true;
                        rpRolesUsers.MessageAlert.ForEach(x => rp.MessageAlert.Add(x));
                        rpRolesUsers.MessageBad.ForEach(x => rp.MessageBad.Add(x));
                    }
                    rp.Success = true;
                    oldOnes.ForEach(x => rp.MessageOk.Add(x + " " + "RegistroIngresado"));
                }

                if (notAllowed.Count > 0)
                {
                    rp.Alert = true;
                    notAllowed.ForEach(x => rp.MessageAlert.Add(x + " " + "RegistroNoIngresado"));
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public static ResponseBasicVm SignInUserThird(string userName, string email, string password, short idRol,
                                                    int idIdioma, int idUsuarioRegistro)
        {
            var rp = new ResponseBasicVm();
            try
            {
                userName = "tp-" + userName.ToLower();
                var seguridadBl = new SecurityBl();
                var currentUser = seguridadBl.GetUsuarios().Where(x => x.UserName == userName.ToLower()).FirstOrDefault();
                if (currentUser == null)
                {
                    WebSecurity.CreateUserAndAccount(userName.ToLower(), password, new
                    {
                        IdIdioma = idIdioma,
                        Email = email,
                        IdUsuarioRegistro = idUsuarioRegistro,
                        UsuarioEmpresa = false
                    });
                    var justEnteredId = seguridadBl.GetUsuarios().Where(x => x.UserName == userName.ToLower()).Select(y => y.Id).ToList();
                    var rpRolesUsers = seguridadBl.AddUsersToRol(justEnteredId, idRol, idUsuarioRegistro);
                    if (!rpRolesUsers.Success)
                    {
                        rp.Alert = true;
                        rpRolesUsers.MessageAlert.ForEach(x => rp.MessageAlert.Add(x));
                        rpRolesUsers.MessageBad.ForEach(x => rp.MessageBad.Add(x));
                    }
                    rp.Success = true;

                }
                else
                {
                    currentUser.Activo = true;
                    currentUser.Email = email;
                    currentUser.FechaModificado = DateTime.Now;
                    currentUser.IdIdioma = (byte)idIdioma;
                    currentUser.IdUsuarioModificador = idUsuarioRegistro;
                    seguridadBl.UpdateUsuarios();
                }
                rp.MessageOk.Add(userName + " " + "RegistroIngresado");
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public static ResponseBasicVm UpdateUserAd(int idUser, short idRol, int idIdioma, int idUsuarioRegistro)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var seguridadBl = new SecurityBl();
                var currentUser = seguridadBl.GetUsuarios().Where(x => x.Id == idUser).FirstOrDefault();
                if (currentUser == null)
                {
                    rp.Success = false;
                    rp.MessageBad.Add("User does not exist at database");
                }
                else
                {
                    currentUser.FechaModificado = DateTime.Now;
                    currentUser.IdIdioma = (byte)idIdioma;
                    currentUser.IdUsuarioModificador = idUsuarioRegistro;
                    var lstUsuario = new List<int>();
                    lstUsuario.Add(idUser);
                    seguridadBl.AddUsersToRol(lstUsuario, idRol, idUsuarioRegistro);
                    seguridadBl.UpdateUsuarios();
                    rp.Success = true;
                    rp.MessageOk.Add("User updated");
                }
            }
            catch (Exception e)
            {
                rp.Success = false;
                rp.MessageBad.Add(e.ToString());
            }
            return rp;
        }

        public static ResponseBasicVm UpdateUserThird(int idUser, short idRol, int idIdioma, string passWord, string email, int idUsuarioRegistro)
        {
            var rp = new ResponseBasicVm();
            try
            {
                var securityBl = new SecurityBl();
                var currentUser = securityBl.GetUsuarios().Where(x => x.Id == idUser).FirstOrDefault();
                if (currentUser == null)
                {
                    rp.Success = false;
                    rp.MessageBad.Add("User does not exist in database.");
                }
                else
                {
                    currentUser.Email = email;
                    currentUser.FechaModificado = DateTime.Now;
                    currentUser.IdIdioma = (byte)idIdioma;
                    currentUser.IdUsuarioModificador = idUsuarioRegistro;
                    if (passWord != string.Empty)
                    {
                        var token = WebSecurity.GeneratePasswordResetToken(currentUser.UserName);
                        WebSecurity.ResetPassword(token, passWord);
                    }
                    rp = securityBl.UpdateUsuarios();
                    rp.Success = true;
                    rp.MessageOk.Add("User updated sucesfully");
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
        /// Se realiza el sign in al sistema por medio de las credenciales
        /// rp.Succes = false No se efectuo correctamente el ingreso
        /// rp.Succes = true Se valida el ingreso.
        /// En caso de que el usuario exista en DB se envia en rp.Data tipo UsuarioProyectoUsta
        /// </summary>
        /// <param name="logInUsuarioVm"></param>
        /// <returns></returns>
        public static ResponseBasicVm SigIn(BasicStrVm logInUsuarioVm)
        {

            var securityBl = new SecurityBl();
            var rp = new ResponseBasicVm();
            var valideAd = false;
            var validMemberShip = false;
            try
            {
                //sanitiza espacios en blanco
                logInUsuarioVm.Id = logInUsuarioVm.Id.Trim();
                logInUsuarioVm.Value = logInUsuarioVm.Value.Trim();

                //Validación de existencia en DB del UserName obliga a que se lleve a cabo un chequeo en DB en el sistema antes
                //de cualquier validación adicional, se valida que no exista.
                var rslt = securityBl.CheckUserName(logInUsuarioVm.Id);
                if (!rslt.Success)
                {
                    //informar a administrador intento de ingreso mediante fuerza bruta
                    rp.Success = false;
                    rp.MessageBad.Add("Nombre de usuario o contraseña invalido.");// ResourceFdimUsa.NombreDeUsuarioYOContrasenaInvalido);
                    return rp;
                }
                rp.Data = rslt.Data;

                validMemberShip = WebSecurity.Login(logInUsuarioVm.Id, logInUsuarioVm.Value);

                var appSettings = ConfigurationManager.AppSettings;
                //Se pregunta si esta activa la validación por diretorio activo
                var adConfigurationActive = Convert.ToBoolean(appSettings["AdActive"]);
                if (adConfigurationActive)
                {                   
                    //Validaciones epecificas
                    valideAd = ActiveDirectoryConfig.ValidationsAD(logInUsuarioVm.Id, logInUsuarioVm.Value);                   

                    //Acciones a tomar
                    //Validacion AD errada y Validacion Membership errada no se puede ingrear al sistema, mostrar error generico
                    if ((valideAd == false) && (validMemberShip == false))
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("Nombre de usuario o contraseña invalido");

                    }
                    // Validacion AD errada y validacion membership correcta.
                    //Se asume el ingreso bien sea por que el AD este inactivo o se trata de una cuenta de terceros mas
                    //sin embargo para membership es valida
                    else if ((valideAd == false) && (validMemberShip == true))
                    {
                        rp.Success = true;
                    }
                    //Sí es un usuario de AD valido y la validación membership fue falsa, se deben actualizar las
                    //credenciales almacenadas en memberhip con las credenciales actuales ingresadas por el usuario
                    //y que fueron validadas en AD
                    else if ((valideAd == true) && (validMemberShip == false))
                    {
                        // se le pide un token de reset a membership y se actualiza la clave en este repositorio.
                        var token = WebSecurity.GeneratePasswordResetToken(logInUsuarioVm.Id);
                        WebSecurity.ResetPassword(token, logInUsuarioVm.Value);
                        rp.Success = true;

                    }
                    //Validación optima
                    else if ((valideAd == true) && (validMemberShip == true))
                    {
                        rp.Success = true;
                    }
                }
                else
                {
                    if(validMemberShip)
                    {
                        rp.Success = true;
                    }
                    else
                    {
                        rp.Success = false;
                        rp.MessageBad.Add("Nombre de usuario o contraseña invalido");//ResourceFdimUsa.MsjNoLogin);
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
    }
}