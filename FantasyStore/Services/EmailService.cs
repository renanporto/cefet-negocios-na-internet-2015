using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Services
{
    public class EmailService
    {
        public static void Send(string subject, string body)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("sistemafantasystore@gmail.com", "fantasystore123"),
            };

            var message = new MailMessage("renan.porto@vtex.com.br", "renan.porto@vtex.com.br")
            {
                Sender = new MailAddress("renan.porto@vtex.com.br", "sistema"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };


            client.Send(message);
        }

        public static void Send(string subject, string body, string destination)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("sistemafantasystore@gmail.com", "fantasystore123"),
            };

            var message = new MailMessage(destination, destination)
            {
                Sender = new MailAddress(destination, "sistema"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                Priority = MailPriority.High
            };


            client.Send(message);
        }
    }
}
