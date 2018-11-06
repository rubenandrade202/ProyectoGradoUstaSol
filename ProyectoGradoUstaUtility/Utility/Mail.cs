using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaUtility
{
    public class Mail
    {
        public async Task<ResponseBasicVm> Send(List<string> destinataries, string subject, string bodyHtml, List<string> pathAttachments)
        {
            var rp = new ResponseBasicVm();
            try
            {
                if (destinataries != null && destinataries.Count > 0)
                {
                    var msg = new MailMessage();
                    msg.Subject = subject;
                    msg.Body = bodyHtml;
                    if (pathAttachments != null && pathAttachments.Count > 0)
                    {
                        foreach (var item in pathAttachments)
                        {
                            //chequear que exista el recurso
                            var bufferAttach = new Attachment(item);
                            msg.Attachments.Add(bufferAttach);
                        }
                    }
                    msg.IsBodyHtml = true;
                    destinataries.ForEach(x =>
                    {
                        msg.To.Add(new MailAddress(x));
                    });
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(msg);
                        rp.Success = true;
                        return rp;
                    }
                }
                else
                {
                    rp.Success = false;
                    rp.MessageBad.Add("No se enviaron destinatarios a modulo MAIL USTA");
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
