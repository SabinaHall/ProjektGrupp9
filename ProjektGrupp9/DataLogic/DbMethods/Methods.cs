using DataLogic.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataLogic.DbMethods
{
    public class Methods
    {
        public static int GetInvitesCount(string id)
        {
            int i = 0;
            using (var context = new ApplicationDbContext())
            {
                var list = context.MeetingInvites.Where(x => x.Receiver == id).ToList();
                i = list.Count;
            }

            return i;
        }

        public static void SendEmailInvitation(List<string> emails,string m, string subject)
        {
            var body = "<p>" + m + "</p>";
            var message = new MailMessage();
            foreach (var item in emails)
            {
                message.To.Add(new MailAddress(item));
            }

            //message.From = new MailAddress("filipparingqvist@gmail.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

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
