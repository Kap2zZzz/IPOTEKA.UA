using IPOTEKA.UA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace IPOTEKA.UA.Code
{
    public static class SendMail
    {
        public static void Send(Application a)
        {
            var fromAddress = new MailAddress("yurij.muzyka@gmail.com", "IPOTEKA-UA"); //mail, Опис відправника
            var toAddress = new MailAddress("yurij.muzyka@gmail.com", "Юрій Музика");
            const string fromPassword = "Fatal1ty";
            const string subject = "Нова заявка з IPOTEKA-UA"; //Тема листа
            string body = Body(a); //Тіло листа
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var message = new MailMessage(fromAddress, toAddress);
            //message.To.Add(new MailAddress("pilip.volodimir@gmail.com", "Володимир Пилип"));
            //message.To.Add(new MailAddress("krysa.myroslav@gmail.com", "Мирослав Криса"));
            //message.To.Add(new MailAddress("max.fedonyuk @gmail.com", "Max Fedonyuk"));

            //message.To.Add(toAddress);
            //message.To.Add(toAddress);

            using (var ms = new MemoryStream(a.XmlData))
            {
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Xml);
                System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
                attach.ContentDisposition.FileName = "Application " + a.ApplicationId + ".xml";
                message.Attachments.Add(attach);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    //ex.InnerException.Message.ToString();
                    //Тут будуть логи ... 
                }
            }
            //message.Attachments.Add(attach);
        }

        private static string Body(Application a)
        {
            string b = string.Format
(
@"<body>
<table border='1' cellpadding='5' cellspacing='5'>
    <tr>
        <td>Клієнт:</td>
        <td>" + a.Sname + " " + a.Name + @"</td>
    </tr>
    <tr>
        <td>Телефон:</td>
        <td>" + a.PhoneNumber + @"</td>
    </tr>
    <tr>
        <td>Продукт:</td>
        <td>" + a.ProductType + @"</td>
    </tr>
    <tr>
        <td>Сума:</td>
        <td>" + a.CreditSum + @"</td>
    </tr>
    <tr>
        <td>Термін:</td>
        <td>" + a.Termin + @"</td>
    </tr>
    <tr>
        <td>Місто:</td>
        <td>" + a.City + @"</td>
    </tr>
</table>
</body>");
            return b;
        }
    }
}