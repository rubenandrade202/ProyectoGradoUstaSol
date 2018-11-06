using ProyectoGradoUstaBus;
using ProyectoGradoUstaCommon;
using ProyectoGradoUstaUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace ProyectoGradoUstaWeb.Utility.Gral
{
  //public static class DatabaseBkp
  //{
  //  public static async void DoAsyncBkp(object source, ElapsedEventArgs e)
  //  {
  //    await Task.Run(() =>
  //    {
  //      var blSeguridad = new SecurityBl();
  //      var rp = blSeguridad.GenerarBkpDbFile();
  //      if(rp.Success)
  //      {
  //        var mailUtil = new Mail();
  //        mailUtil.Send(new List<string>() { "rubenandrade201@gmail.com" }, "prueba bkp db", "backup db", new List<string>() { rp.Data.ToString() });
  //      }

        
  //      //  var pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "BkpFiles\\";
  //      //string dbname = _dbContext.Database.Connection.Database;
  //      //string dbBackUp = "MajesticDB" + DateTime.Now.ToString(yyyyMMddHHmm);
  //      //const string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'MajesticDb-Ali-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
  //      //int path = _dbContext.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, string.Format(sqlCommand, dbname, dbBackUp));



  //      //var some = pathDirectory;
  //    });      
  //  }


  //  //public async Task<ResponseBasicVm> DoDbBkpProcess()
  //  //{
  //  //  var rp = new ResponseBasicVm();
  //  //  try
  //  //  {
  //  //    await new ResponseBasicVm();
  //  //    //if (destinataries != null && destinataries.Count > 0)
  //  //    //{
  //  //    //  var msg = new MailMessage();
  //  //    //  msg.Subject = subject;
  //  //    //  msg.Body = bodyHtml;
  //  //    //  msg.IsBodyHtml = true;
  //  //    //  destinataries.ForEach(x =>
  //  //    //  {
  //  //    //    msg.To.Add(new MailAddress(x));
  //  //    //  });
  //  //    //  using (var smtp = new SmtpClient())
  //  //    //  {
  //  //    //    await smtp.SendMailAsync(msg);
  //  //    //    rp.Success = false;
  //  //    //    return rp;
  //  //    //  }
  //  //    //}
  //  //    //else
  //  //    //{
  //  //    //  rp.Success = false;
  //  //    //  rp.MessageBad.Add("No se enviaron destinatarios a modulo MAIL USTA");
  //  //    //}
  //  //  }
  //  //  catch (Exception e)
  //  //  {
  //  //    rp.Success = false;
  //  //    rp.MessageBad.Add(e.ToString());
  //  //  }
  //  //  return rp;
  //  //}
  //}
}