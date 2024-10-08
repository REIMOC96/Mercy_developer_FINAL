using MercDevs_ej2.Models;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MercDevs_ej2.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpSettings.Host)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.Username),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            await smtpClient.SendMailAsync(mailMessage);
        }
        public async Task<bool> SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachment, string attachmentName)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpSettings.Host)
                {
                    Port = _smtpSettings.Port,
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = true,
                };

                //definimos formato del email, usando la configuracion de smtp para usarlas como direccion de origen del servicio
                //luego decimos el tema principal, seguido por el cuerpo del correo ademas de usar un formato similar a html para darle un formato mas de pagina
                //por ultimo usamos mail.message.to.add(to) para pasarle el parametro de destinatario del correo, esto nos permite pasarle cualquier correo de cliente 
                //y adjuntar la informacion mas el pdf en cuestion.
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(to);

                // Adjuntar el archivo
                using (var stream = new MemoryStream(attachment))
                {
                    var attachmentToSend = new Attachment(stream, attachmentName, "application/pdf");
                    mailMessage.Attachments.Add(attachmentToSend);

                    await smtpClient.SendMailAsync(mailMessage);
                }

                return true; // Correo enviado exitosamente
            }
            catch (Exception)
            {
                return false; // Error al enviar el correo
            }
        }

    }
}
