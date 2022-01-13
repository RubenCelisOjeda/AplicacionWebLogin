using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ServicioAPISeguridad.Transversal.Common.Utils
{
    public static class MetGlo
    {

        public static bool IsEmail(string pEmail)
        {
            bool correcto = false;

            try
            {
               new MailAddress(pEmail);
               correcto = false;
            }
            catch (Exception ex)
            {
                correcto = false;
            }
            return correcto;
        }

        public static bool EnviarCorreo()
        {
            bool correcto = false;

            try
            {
                using (MailMessage mailMsg = new MailMessage())
                {
                    //correo destinatario
                    mailMsg.To.Add(Constantes.EMAIL_SERVIDOR);

                    //asunto
                    mailMsg.Subject = "PRUEBA DE CORREO";
                    mailMsg.SubjectEncoding = Encoding.UTF8;

                    //correo destinatario 2
                    //mailMsg.Bcc.Add("");

                    //mensaje a enviar
                    mailMsg.Body = "PRUEBA CTM";
                    mailMsg.BodyEncoding = Encoding.UTF8;
                    mailMsg.IsBodyHtml = true;
                    mailMsg.From = new MailAddress(Constantes.EMAIL_SERVIDOR);

                    //cliente
                    using (SmtpClient cliente = new SmtpClient())
                    {
                        cliente.UseDefaultCredentials = false;
                        cliente.Credentials = new NetworkCredential(Constantes.EMAIL_SERVIDOR, Constantes.EMAIL_SERVIDOR_PASSWORD);
                        cliente.Port = 587;
                        cliente.EnableSsl = true;

                        //host
                        cliente.Host = "smtp.gmail.com";
                        cliente.Send(mailMsg);
                        correcto = true;
                    }
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                throw ex;
            }
            return correcto;
        }
    }
}
