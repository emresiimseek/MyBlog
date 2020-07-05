using MyBlog.EntityFramework.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace FrameworkCore.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {
        public static void createCookie(int id, string username, string email, DateTime expiration, List<UsersRoleses> roles, bool rememberMe, string firstName, string lastName)
        {
            var authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, expiration, rememberMe, createAutoTag(email, roles, firstName, lastName, id));
            string enTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, enTicket));
        }

        private static string createAutoTag(string email, List<UsersRoleses> roles, string firstName, string lastName, int id)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(email);
            stringBuilder.Append('|');
            foreach (UsersRoleses usersRoles in roles)
            {
                stringBuilder.Append(usersRoles.Roles);//TODO:Burada join ile roller çekilecek
                stringBuilder.Append(',');
            }
            stringBuilder.Append('|');
            stringBuilder.Append(firstName);
            stringBuilder.Append('|');
            stringBuilder.Append(lastName);
            stringBuilder.Append('|');
            stringBuilder.Append(id);
            return stringBuilder.ToString();
        }
    }
}
