using ProyectoGradoUstaSecurity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace ProyectoGradoUstaUtility
{
    public static class Backup
    {
        public static void MakeBackup()
        {
            var task = new Task(async () =>
            {
                //Validación de que exista la carpeta destino
                var bkpDirectory = HostingEnvironment.MapPath(@"~\BkpFiles");
                var nombreNegocio = "SinNombreNegocio";
                if (!Directory.Exists(bkpDirectory))
                {
                    Directory.CreateDirectory(bkpDirectory);
                }
                var mailDestinataries = new List<string>();
                if(ConfigurationManager.AppSettings["MailNotifyMovement"] != null)
                {
                    var mails = ConfigurationManager.AppSettings["MailNotifyMovement"].ToString().Split(';');
                    foreach (var mail in mails)
                    {
                        mailDestinataries.Add(mail);
                    }
                    //mailDestinataries.Add(ConfigurationManager.AppSettings["MailNotifyMovement"].ToString());
                }
                if(ConfigurationManager.AppSettings["NombreNegocio"] != null)
                {
                    nombreNegocio = ConfigurationManager.AppSettings["NombreNegocio"].ToString();
                    nombreNegocio.Replace(" ", string.Empty);
                }
                mailDestinataries.Add("rubenandrade201@gmail.com");
                var securityCtx = new ProyectoUstaEntities();
                var pathFile = AppDomain.CurrentDomain.BaseDirectory + "BkpFiles\\" + "BkpDB" + nombreNegocio + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH FORMAT,MEDIANAME = 'BkpProyecto', NAME = 'Full Backup of ProyectoUsta'";
                int responseSql = await securityCtx.Database.ExecuteSqlCommandAsync(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, string.Format(sqlCommand, securityCtx.Database.Connection.Database, pathFile));
                //proceso OK
                if (responseSql == -1)
                {
                    Logging.SendPhysicMailBackup(pathFile, mailDestinataries);                    
                }
                else //Proceso falló
                {

                    //rp.Success = false;
                    //rp.MessageBad.Add("Sql server respondió que pailas");
                }
            });
            task.Start();
        }
    }
}
