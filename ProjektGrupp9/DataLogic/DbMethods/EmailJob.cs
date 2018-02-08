using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLogic.Models;

namespace DataLogic.DbMethods
{
    public class EmailJob : IJob
    {
        public static int count = 0;
        public void Execute(IJobExecutionContext context)
        {
            var message = $"Idag har det gjort {count} ändringar på formella inlägg,in och kolla för se mer information.";
            var subject = "Summering av formella inlägg";
            var emailList = new List<string>();
            using(var db = new ApplicationDbContext())
            {
                var users = db.Users.Where(x => x.GetMail);
                foreach (var item in users)
                {
                    emailList.Add(item.Email);
                }
                //emailList.Add("hozzyy_96@hotmail.com");
                
            }

            Methods.SendEmailInvitation(emailList, message, subject);
        }
    }
}
