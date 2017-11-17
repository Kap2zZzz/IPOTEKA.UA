using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace IPOTEKA.UA.Code
{
    public static class SendMail
    {
        public static void Send()
        {
            var fromAddress = new MailAddress("yurij.muzyka@gmail.com", "MUZYKA"); //mail, Опис відправника
            var toAddress = new MailAddress("yurij.muzyka@gmail.com", "MUZYKA2");
            const string fromPassword = "Fatal1ty";
            const string subject = "Нова заявка з IPOTEKA.UA"; //Тема листа
            const string body = "Body"; //Тіло листа

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                //Port = 465,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var message = new MailMessage(fromAddress, toAddress);
            message.To.Add(toAddress);
            message.To.Add(toAddress);
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;

            smtp.Send(message);

        }
    }
}