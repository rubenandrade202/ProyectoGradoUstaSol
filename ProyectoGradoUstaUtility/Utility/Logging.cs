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
            if(destinatariesNotifySale == null && (mailClient == null || mailClient == string.Empty))
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
                    msgHtml = "Se ha registrado un credito por valor de $" + valorCreditoActual + " para el cliente " + nombreCliente + ". El nuevo saldo del credito es por: $" + valorCreditoTotal + ". Gracias por comprar en el supermercado El Dorado. ";                   
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
                    var totalCajaDia = 0;
                    var totalVentaDia = 0;
                    var totalCreditosDia = 0;
                    var totalCreditos = 0;
                    var msgHtml = string.Empty;
                    var msgHtmlCompliment = string.Empty;
                    

                    var ctxDomain = new ProyectoUstaDomainCtx();
                    var recordsVentaDia = ctxDomain.VentasProyectoUsta.Where(x => x.FechaRegistro >= fechaHoraInicialDia && x.FechaRegistro <= fechaHoraFinalDia).Select(x => x.Total).ToList();
                    var recordsCajaDia = ctxDomain.MovimientosCajaProyectoUsta.Where(x => x.FechaRegsitro >= fechaHoraInicialDia && x.FechaRegsitro <= fechaHoraFinalDia).Select(x => x.Valor).ToList();
                    var recordsClientesCreditos = ctxDomain.ClientesProyectoUsta.ToList();                    
                    var recordsLogCreditosDia = (from logCreditos in ctxDomain.LogCreditosProyectoUsta
                                                 join clientes in ctxDomain.ClientesProyectoUsta on logCreditos.IdCliente equals clientes.Id
                                                 join usuarios in ctxDomain.UsuariosProyectoUsta on logCreditos.IdUsuario equals usuarios.Id
                                                 where logCreditos.FechaRegistro >= fechaHoraInicialDia && logCreditos.FechaRegistro <= fechaHoraFinalDia
                                                 select new
                                                 {
                                                     logCreditos.FechaRegistro,
                                                     logCreditos.Valor,
                                                     usuarios.UserName,
                                                     clientes.Nombre
                                                 }).ToList();

                    if (recordsVentaDia.Count > 0)
                    {
                        totalVentaDia = recordsVentaDia.Sum();
                    }
                    if (recordsCajaDia.Count > 0)
                    {
                        totalCajaDia = recordsCajaDia.Sum();
                    }
                    if(recordsClientesCreditos.Count > 0)
                    {
                        totalCreditos = recordsClientesCreditos.Sum(x => x.CupoEmpleado);
                    }
                    if (recordsLogCreditosDia.Count > 0)
                    {
                        totalCreditosDia = recordsLogCreditosDia.Sum(x => x.Valor);
                        msgHtmlCompliment += "</br>CREDITOS DEL DÍA</br><ul>";
                        recordsLogCreditosDia.OrderBy(x => x.Nombre).ToList().ForEach(x =>
                        {
                            msgHtmlCompliment += "<li><b>CLIENTE: " + x.Nombre + "</b></li>";
                            msgHtmlCompliment += "<li>VENDEDOR: " + x.UserName + "</li>";
                            msgHtmlCompliment += "<li>VALOR: " + x.Valor + "</li>";
                            msgHtmlCompliment += "<li>FECHA REF: " + x.FechaRegistro + "</li>";
                        });
                        msgHtmlCompliment += "</ul>";


                    }

                    msgHtml = "<ul><li>VENTAS REGISTRADAS DÍA:&nbsp;" + totalVentaDia + "</li>";
                    msgHtml += "<li>INGRESO EFECTIVO DÍA:&nbsp;" + totalCajaDia + "</li>";
                    msgHtml += "<li>TOTAL CREDITOS GENERAL:&nbsp;" + totalCreditos + "</li>";
                    msgHtml += "<li>TOTAL CREDITOS DÍA:&nbsp;" + totalCreditosDia + "</li>";
                    msgHtml += "<li>TOTAL PAGO PEDIDOS DÍA:&nbsp;" + 0 + "</li>";
                    msgHtml += "<li>TOTAL OTROS PAGOS DÍA:&nbsp;" + 0 + "</li>";
                    msgHtml += "</ul>";
                    msgHtml += msgHtmlCompliment;

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
