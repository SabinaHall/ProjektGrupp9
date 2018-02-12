using DataLogic.Models;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.DbMethods
{
    public class ICalMessage
    {
        public static void SendMessage(Events e, string mail)
        {
            MailMessage message = new MailMessage();
            message.To.Add(mail);
            message.Subject = "Ett möte";
            message.Body = "emailbody";
            message.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            string DateFormat = "yyyyMMddTHHmmssZ";
            string now = DateTime.Now.ToUniversalTime().ToString(DateFormat);
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Compnay Inc//Product Application//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");

                DateTime dtStart = Convert.ToDateTime(DateTime.Now);
                DateTime dtEnd = Convert.ToDateTime(DateTime.Now);
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine("DTSTART:" + dtStart.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTEND:" + dtEnd.ToUniversalTime().ToString(DateFormat));
                sb.AppendLine("DTSTAMP:" + now);
                sb.AppendLine("UID:" + Guid.NewGuid());
                sb.AppendLine("CREATED:" + now);
                //sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + res.DetailsHTML);
                //sb.AppendLine("DESCRIPTION:" + res.Details);
                sb.AppendLine("LAST-MODIFIED:" + now);
                sb.AppendLine("LOCATION:" + e.Place);
                sb.AppendLine("SEQUENCE:0");
                sb.AppendLine("STATUS:CONFIRMED");
                sb.AppendLine("SUMMARY:" + " ett möte");
                sb.AppendLine("TRANSP:OPAQUE");
                sb.AppendLine("END:VEVENT");
            
            sb.AppendLine("END:VCALENDAR");
            var calendarBytes = Encoding.UTF8.GetBytes(sb.ToString());
            MemoryStream ms = new MemoryStream(calendarBytes);
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(ms, "event.ics", "text/calendar");
            message.Attachments.Add(attachment);

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    //Ange egna email och lösenordet. 
                    UserName = "dontreplygrupp9@gmail.com",
                    Password = "Grupp12345"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}
