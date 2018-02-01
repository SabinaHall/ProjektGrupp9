using DataLogic.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
