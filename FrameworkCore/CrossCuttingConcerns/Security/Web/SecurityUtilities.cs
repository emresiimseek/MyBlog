using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace FrameworkCore.CrossCuttingConcerns.Security.Web
{
    public class SecurityUtilities
    {
        public Identity FormAuthTicketToIdentity(FormsAuthenticationTicket ticket)
        {
            Identity identity = new Identity
            {
                Id = setId(ticket),
                Name = setName(ticket),
                Email = setemail(ticket),
                Roles = setroles(ticket),
                Firstname = setfirstname(ticket),
                Lastname = setlastname(ticket),
                AuthenticationType = setAuthenticationType(),
                IsAuthenticated = setIsAuthenticated(),
            };
            return identity;

        }
        private bool setIsAuthenticated()
        {
            return true;
        }
        private string setAuthenticationType()
        {
            return "Forms";
        }
        private string setlastname(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[3];
        }
        private string setfirstname(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[2];
        }

        private string[] setroles(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            string[] roles = data[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return roles;
        }
        private string setemail(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return data[0];
        }
        private string setName(FormsAuthenticationTicket ticket)
        {
            return ticket.Name;
        }
        private int setId(FormsAuthenticationTicket ticket)
        {
            string[] data = ticket.UserData.Split('|');
            return (Convert.ToInt32(data[4]));
        }
    }
}
