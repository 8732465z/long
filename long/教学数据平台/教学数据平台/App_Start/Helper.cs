using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Security;
namespace 教学数据平台.App_Start
{
    public class Helper
    {
      
        public static Models.TeachDBEntities2 tdb = new Models.TeachDBEntities2();
        public static FormsAuthenticationTicket GetTicket(HttpContextBase context)
        {
            var t = context.Request.Cookies[FormsAuthentication.FormsCookieName].Value;
            var ticket = FormsAuthentication.Decrypt(t);
            return ticket;
        }
        public static int GetDepartmentID(HttpContextBase context)
        {

            var ticket = GetTicket(context);
            string[] userdata = ticket.UserData.Split(';');
            return int.Parse(userdata[0]);
        }
        public static int GetTeacherID(HttpContextBase context)
        {
            var ticket = GetTicket(context);
            string[] userdata = ticket.UserData.Split(';');
            return int.Parse(userdata[1]);
        }
    }
}