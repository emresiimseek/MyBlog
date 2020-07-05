using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.CrossCuttingConcerns.Security
{
    public class Identity : IIdentity
    {
        public string AuthenticationType
        {
            get; set;
        }

        public bool IsAuthenticated
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }


    }
}
