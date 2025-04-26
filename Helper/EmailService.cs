using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EmploymentAgency.Helper
{
    public class EmailService
    {
        public static void SendEmail(string to, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}