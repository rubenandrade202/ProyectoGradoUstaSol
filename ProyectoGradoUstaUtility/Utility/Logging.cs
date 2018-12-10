using ProyectoGradoUstaCommon;
using ProyectoUstaDomain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace ProyectoGradoUstaUtility
{
    public static class Logging
    {
        public static void NotifyMovementCreditClient(int valorCreditoActual, int valorCreditoTotal, string mailClient, string nombreCliente)
        {
            var destinatariesNotifySale = ConfigurationManager.AppSettings["MailNotifyMovement"];
            var subject = ConfigurationManager.AppSettings["SubjectNotifyMovementCredito"] == null ? "Subject default no seteado en web config" : ConfigurationManager.AppSettings["SubjectNotifyMovementCredito"].ToString();
            var businessName = ConfigurationManager.AppSettings["NombreNegocio"] == null ? "Subject default no seteado en web config" : ConfigurationManager.AppSettings["NombreNegocio"].ToString(); //ConfigurationManager.AppSettings.Get("");

            if (destinatariesNotifySale == null && (mailClient == null || mailClient == string.Empty))
            {
                NotifyTxt("No hay valores destinatarios validos para envio de correo por credito de cliente", "GRAVE");
                return;
            }
            else
            {
                var destinataries = destinatariesNotifySale.Split(';').ToList();
                destinataries.Add(mailClient);
                subject += " " + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
                var task = new Task(async () =>
                {
                    var msgHtml = string.Empty;
                    //msgHtml = "Se ha registrado un credito por valor de $" + valorCreditoActual + " para el cliente " + nombreCliente + ". El nuevo saldo del credito es por: $" + valorCreditoTotal + ". Gracias por comprar en el supermercado El Dorado. ";                   
                    msgHtml = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>
<html lang='en'>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>

  <title>Grids Master Template</title>

  <style type='text/css'>
    
    /* Outlines the grids, remove when sending */
    table td { border: 1px solid black; }

    /* CLIENT-SPECIFIC STYLES */
    body, table, td, a { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }
    table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; }
    img { -ms-interpolation-mode: bicubic; }

    /* RESET STYLES */
    img { border: 0; outline: none; text-decoration: none; }
    table { border-collapse: collapse !important; }
    body { margin: 0 !important; padding: 0 !important; width: 100% !important; }

    /* iOS BLUE LINKS */
    a[x-apple-data-detectors] {
      color: inherit !important;
      text-decoration: none !important;
      font-size: inherit !important;
      font-family: inherit !important;
      font-weight: inherit !important;
      line-height: inherit !important;
    }

    /* ANDROID CENTER FIX */
    div[style*='margin: 16px 0;'] { margin: 0 !important; }

    /* MEDIA QUERIES */
    @media all and (max-width:400px){ 
      .wrapper{ width:320px!important; padding: 0 !important; }
      .container{ width:300px!important;  padding: 0 !important; }
      .mobile{ width:300px!important; display:block!important; padding: 0 !important; }
      .img{ width:100% !important; height:auto !important; }
      *[class='mobileOff'] { width: 0px !important; display: none !important; }
      *[class*='mobileOn'] { display: block !important; max-height:none !important; }
    }

  </style>    
</head>
<body style='margin:0; padding:0; background-color:#F2F2F2;'>
  
  <span style='display: block; width: 640px !important; max-width: 640px; height: 1px' class='mobileOff'></span>
  
  <center>
    <table width='100%' border='0' cellpadding='0' cellspacing='0' bgcolor='#F2F2F2'>
      <tr>
        <td align='center' valign='top'>
            
          <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='center' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td align='left' valign='top' style='font-weight:bold;'>
                      Notificación Movimiento Credito
                    </td>
                  </tr>
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>

          <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='left' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Cliente
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      " + nombreCliente + @"
                    </td>
					</tr>
					<tr>
					<td width='50%' class='mobile' align='left' valign='top'>
                      Valor movimento
                    </td>
					
					
                    <td width='50%' class='mobile' align='left' valign='top'>
                       $ " + string.Format("{0:n0}", valorCreditoActual)  + @"
                    </td>
					</tr>
					<tr>
					<td width='50%' class='mobile' align='left' valign='top'>
                      Total Credito Otorgado
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + string.Format("{0:n0}", valorCreditoTotal) + @"
                    </td>
					</tr>
					
					
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>
		  
		  <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='center' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td align='left' valign='top' style='font-weight:bold;'>
                      Gracias por comprar en " + businessName + @"  
                    </td>
                  </tr>
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>
		  		  		         
        </td>
      </tr>
    </table>
  </center>
</body>
</html>";




                    var mailUtility = new Mail();
                    var rp = await mailUtility.Send(destinataries, subject, msgHtml, null);
                    if (!rp.Success)
                    {
                        var message = string.Empty;
                        rp.MessageBad.ForEach(x =>
                        {
                            message += x;
                        });
                        NotifyTxt(message, "GRAVE");
                    }
                });
                task.Start();
            }
        }

        public static void NotifyMovementPayClient(int valorPago, int valorCreditoTotal, string mailClient, string nombreCliente)
        {
            var destinatariesNotifySale = ConfigurationManager.AppSettings["MailNotifyMovement"];
            var subject = ConfigurationManager.AppSettings["SubjectNotifyMovementCredito"] == null ? "Subject default no seteado en web config" : ConfigurationManager.AppSettings["SubjectNotifyMovementCredito"].ToString();
            if (destinatariesNotifySale == null && (mailClient == null || mailClient == string.Empty))
            {
                NotifyTxt("No hay valores destinatarios validos para envio de correo por credito de cliente", "GRAVE");
                return;
            }
            else
            {
                var destinataries = destinatariesNotifySale.Split(';').ToList();
                destinataries.Add(mailClient);
                subject += " " + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
                var task = new Task(async () =>
                {
                    var msgHtml = string.Empty;
                    msgHtml = "Se ha registrado un abono de credito por valor de $" + valorPago + " del cliente " + nombreCliente +". El nuevo saldo del credito es por: $" + valorCreditoTotal + ". Gracias por comprar en el supermercado El Dorado ";
                    var mailUtility = new Mail();
                    var rp = await mailUtility.Send(destinataries, subject, msgHtml, null);
                    if (!rp.Success)
                    {
                        var message = string.Empty;
                        rp.MessageBad.ForEach(x =>
                        {
                            message += x;
                        });
                        NotifyTxt(message, "GRAVE");
                    }
                });
                task.Start();
            }
        }

        public static void NotifyMovementConfirmLeft(string nombreProveedor, List<BasicVm> noConfirmedProducts)
        {
            var destinatariesNotify = ConfigurationManager.AppSettings["MailNotifyMovement"];
            var subject = ConfigurationManager.AppSettings["SubjectNotifyMovementConfirmation"] == null ? "Subject no seteado en web config para movimiento de confirmación" : ConfigurationManager.AppSettings["SubjectNotifyMovementConfirmation"].ToString();
            if (destinatariesNotify == null)
            {
                NotifyTxt("No hay valores destinatarios validos para envio de correo por credito de cliente", "GRAVE");
                return;
            }
            else
            {
                var destinataries = destinatariesNotify.Split(';').ToList();
                subject += " " + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
                var task = new Task(async () =>
                {
                    var msgHtml = string.Empty;
                    msgHtml += "Notificación de aprobación de movimiento El Dorado para el proveedor " + nombreProveedor + " con los siguientes productos no confirmados";
                    msgHtml += "<br>";
                    msgHtml += "<ul>";
                    foreach (BasicVm item in noConfirmedProducts)
                    {
                        msgHtml += "<li>" + item.Value + " X " + item.Id + "</li>";
                    }
                    msgHtml += "</ul>";
                    var mailUtility = new Mail();
                    var rp = await mailUtility.Send(destinataries, subject, msgHtml, null);
                    if (!rp.Success)
                    {
                        var message = string.Empty;
                        rp.MessageBad.ForEach(x =>
                        {
                            message += x;
                        });
                        NotifyTxt(message, "GRAVE");
                    }
                });
                task.Start();
            }
        }

        public static void NotifyMovementGeneral()
        {
            var destinatariesNotifySale = ConfigurationManager.AppSettings["MailNotifyMovement"];
            var subject = ConfigurationManager.AppSettings["SubjectNotifyMovement"] == null ? "Subject default no seteado en web config" : ConfigurationManager.AppSettings["SubjectNotifyMovement"].ToString();
            if (destinatariesNotifySale == null)
            {
                NotifyTxt("Llave MailNotifyMovement no seteada para informe de venta por correo", "GRAVE");
                return;
            }
            else
            {
                var destinataries = destinatariesNotifySale.Split(';').ToList();
                subject += " " + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
                var task = new Task(async () =>
                {
                    var fechaHoraInicialDia = DateTime.Now.Date;
                    var fechaHoraFinalDia = DateTime.Now.Date.AddDays(1).AddTicks(-1);
                    var initialDateMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var lastDateMonth = initialDateMonth.AddMonths(1).AddTicks(-1);//initialDateMonth.AddMonths(1).AddDays(-1).AddTicks(-1);

                    var totalInventory = "0";
                    var totalCreditGral = "0";
                    var totalSalesMonth = "0";
                    var totalPayedRequestMonth = "0";
                    var totalOtherPayedMonth = "0";

                    var totalCreditDay = "0";
                    var totalSalesDay = "0";
                    var totalIncomeDay = "0";
                    var totalPayedRequestDay = "0";
                    var totalOtherPayedDay = "0";
                    
                    var ctxDomain = new ProyectoUstaDomainCtx();

                    var recordsVentaDia = ctxDomain.VentasProyectoUsta.Where(x => x.FechaRegistro >= fechaHoraInicialDia && x.FechaRegistro <= fechaHoraFinalDia).Select(x => x.Total).ToList();
                    var recordsSalesMonth = ctxDomain.VentasProyectoUsta.Where(x => x.FechaRegistro >= initialDateMonth && x.FechaRegistro <= lastDateMonth).Select(x => x.Total).ToList();
                    var recordsInventory = ctxDomain.ProductosProyectoUsta.Where(x => x.CantidadActual > 0).Select(x => new { Valor = x.CantidadActual * x.Precio }).ToList();
                    var recordsCreditAll = ctxDomain.ClientesProyectoUsta.Select(x => x.CupoEmpleado).ToList();
                    var recordsLogCredits = ctxDomain.LogCreditosProyectoUsta.Where(x => x.FechaRegistro >= fechaHoraInicialDia && x.FechaRegistro <= fechaHoraFinalDia).Select(x => x.Valor).ToList();
            
                    if (recordsLogCredits != null && recordsLogCredits.Count > 0)
                    {
                        totalCreditDay = string.Format("{0:n0}", recordsLogCredits.Sum());
                    }

                    if (recordsCreditAll != null && recordsCreditAll.Count > 0)
                    {
                        totalCreditGral = string.Format("{0:n0}", recordsCreditAll.Sum());
                    }

                    if (recordsInventory != null && recordsInventory.Count > 0)
                    {
                        totalInventory = string.Format("{0:n0}", recordsInventory.Sum(x => x.Valor)); 
                    }

                    if (recordsVentaDia.Count > 0)
                    {
                        totalSalesDay = string.Format("{0:n0}", recordsVentaDia.Sum());
                    }

                    if (recordsSalesMonth.Count > 0)
                    {
                        totalSalesMonth = string.Format("{0:n0}", recordsSalesMonth.Sum());
                    }
                    
                    var newHtml = @"<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.01 Transitional//EN' 'http://www.w3.org/TR/html4/loose.dtd'>
<html lang='en'>
<head>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>

  <title>Grids Master Template</title>

  <style type='text/css'>
    
    /* Outlines the grids, remove when sending */
    table td { border: 1px solid black; }

    /* CLIENT-SPECIFIC STYLES */
    body, table, td, a { -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; }
    table, td { mso-table-lspace: 0pt; mso-table-rspace: 0pt; }
    img { -ms-interpolation-mode: bicubic; }

    /* RESET STYLES */
    img { border: 0; outline: none; text-decoration: none; }
    table { border-collapse: collapse !important; }
    body { margin: 0 !important; padding: 0 !important; width: 100% !important; }

    /* iOS BLUE LINKS */
    a[x-apple-data-detectors] {
      color: inherit !important;
      text-decoration: none !important;
      font-size: inherit !important;
      font-family: inherit !important;
      font-weight: inherit !important;
      line-height: inherit !important;
    }

    /* ANDROID CENTER FIX */
    div[style*='margin: 16px 0;'] { margin: 0 !important; }

    /* MEDIA QUERIES */
    @media all and (max-width:400px){ 
      .wrapper{ width:320px!important; padding: 0 !important; }
      .container{ width:300px!important;  padding: 0 !important; }
      .mobile{ width:300px!important; display:block!important; padding: 0 !important; }
      .img{ width:100% !important; height:auto !important; }
      *[class='mobileOff'] { width: 0px !important; display: none !important; }
      *[class*='mobileOn'] { display: block !important; max-height:none !important; }
    }

  </style>    
</head>
<body style='margin:0; padding:0; background-color:#F2F2F2;'>
  
  <span style='display: block; width: 640px !important; max-width: 640px; height: 1px' class='mobileOff'></span>
  
  <center>
    <table width='100%' border='0' cellpadding='0' cellspacing='0' bgcolor='#F2F2F2'>
      <tr>
        <td align='center' valign='top'>
            
          <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='center' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td align='left' valign='top' style='font-weight:bold;'>
                      Información General/Mes
                    </td>
                  </tr>
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>

          <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='left' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Total Inventario Gral
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalInventory + @"
                    </td>
					</tr>
					<tr>
					<td width='50%' class='mobile' align='left' valign='top'>
                      Total Creditos Gral
                    </td>
					
					
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalCreditGral + @"
                    </td>
					</tr>
					<tr>
					<td width='50%' class='mobile' align='left' valign='top'>
                      Total Ventas Mes
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalSalesMonth + @"
                    </td>
					</tr>
					<tr>
					<td width='50%' class='mobile' align='left' valign='top'>
                      Total Pago Pedidos Mes
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalPayedRequestMonth + @"
                    </td>
					</tr>
					<tr>
					<td width='50%' class='mobile' align='left' valign='top'>
                      Total Otros Egresos Mes
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalOtherPayedMonth + @"
                    </td>
                  </tr>
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>
		  
		  <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='center' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td align='left' valign='top' style='font-weight:bold;'>
                      Información Día
                    </td>
                  </tr>
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>
		  
		  <table width='100%' cellpadding='0' cellspacing='0' border='0' class='wrapper' bgcolor='#FFFFFF'>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
            <tr>
              <td align='left' valign='top'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0' class='container'>
                  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Total Creditos Día
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalCreditDay + @"
                    </td>
                  </tr>
				  
				  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Total Ventas Día
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalSalesDay + @"
                    </td>
                  </tr>
				  
				  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Total Ingreso Efectivo Día
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                     $ " + totalIncomeDay + @"
                    </td>
                  </tr>
				  
				  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Total Pago Pedidos Día
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalPayedRequestDay + @"
                    </td>
                  </tr>
				  
				  <tr>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      Total Otros Pagos Día
                    </td>
                    <td width='50%' class='mobile' align='left' valign='top'>
                      $ " + totalOtherPayedDay + @"
                    </td>
                  </tr>
				  
                </table>

              </td>
            </tr>
            <tr>
              <td height='10' style='font-size:10px; line-height:10px;'>&nbsp;</td>
            </tr>
          </table>
		           
        </td>
      </tr>
    </table>
  </center>
</body>
</html>";


                    var mailUtility = new Mail();
                    var rp = await mailUtility.Send(destinataries, subject, newHtml, null);
                    if (!rp.Success)
                    {
                        var message = string.Empty;
                        rp.MessageBad.ForEach(x =>
                        {
                            message += x;
                        });
                        NotifyTxt(message, "GRAVE");
                    }
                });
                task.Start();
            }
        }

        public static void NotifyMovementGeneric(string htmlBody)
        {
            var destinatariesNotify = ConfigurationManager.AppSettings["MailNotifyMovement"];
            var subject = ConfigurationManager.AppSettings["SubjectNotifyMovement"] == null ? "Subject default no seteado en web config" : ConfigurationManager.AppSettings["SubjectNotifyMovement"].ToString();
            if (destinatariesNotify == null)
            {
                NotifyTxt("Llave MailNotifyMovement no seteada para informe de venta por correo", "GRAVE");
                return;
            }
            else
            {
                var destinataries = destinatariesNotify.Split(';').ToList();
                subject += " " + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
                var task = new Task(async () =>
                {
                    var mailUtility = new Mail();
                    var rp = await mailUtility.Send(destinataries, subject, htmlBody, null);
                    if (!rp.Success)
                    {
                        var message = string.Empty;
                        rp.MessageBad.ForEach(x =>
                        {
                            message += x;
                        });
                        NotifyTxt(message, "GRAVE");
                    }
                });
                task.Start();
            }
        }

        public static void SendPhysicMailBackup(string pathFile, List<string> mailDestinataries)
        {
            var task = new Task(async () =>
            {
                var mailUtility = new Mail();
                var rp = await mailUtility.Send(mailDestinataries, "Backup Sistema POS El Dorado", "Backup Sistema POS El Dorado", new List<string>() { pathFile });
                if (!rp.Success)
                {
                    var message = string.Empty;
                    rp.MessageBad.ForEach(x =>
                    {
                        message += x;
                    });
                    NotifyTxt(message, "GRAVE");
                }
            });
            task.Start();            
        }

        public static void NotifyTxt(string txt, string severidad)
        {
            var bufferMsg = string.Empty;
            var routeDirectory = string.Empty;
            var routeFile = string.Empty;
            var momentoEvento = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
            var currentFileName = DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
            var configRouteConfig = ConfigurationManager.AppSettings["LoggerTxtFolder"];
            //validacion existencia directorio
            if (configRouteConfig == null)
            {
                //Quemado obligado a escribir error de tipo fatal por faltante configuración web config                
                routeDirectory = HostingEnvironment.MapPath(@"~\LogFiles");
                if (!Directory.Exists(routeDirectory))
                {
                    Directory.CreateDirectory(routeDirectory);
                }
                currentFileName = DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt";
                routeFile += routeDirectory + @"\" + currentFileName;

                bufferMsg = "Llave de configuración LoggerTxtFolder no seteada, se utiliza una por default, generalmente ~\\LogFiles.\r\nADVERTENCIA\r\n" + momentoEvento;
                Write(routeFile, bufferMsg);
            }
            else
            {
                routeDirectory = HostingEnvironment.MapPath(configRouteConfig.ToString());               
                if (!Directory.Exists(routeDirectory))
                {
                    Directory.CreateDirectory(routeDirectory);
                }
                routeFile = routeDirectory + @"\" + currentFileName;

                if (!File.Exists(routeFile))
                {
                    using (FileStream fs = File.Create(routeFile))
                    { }
                }
                bufferMsg = txt + "\r\n" + severidad + "\r\n" + momentoEvento;
                Write(routeFile, bufferMsg);
            }
        }

        private static void Write(string pathFile, string txt)
        {
            using (StreamWriter sw = File.AppendText(pathFile))
            {
                sw.WriteLine(txt);
                sw.Close();
            }
        }
    }
}
