using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace FantasyStore.WebApp.Models
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(IPrincipal principal)
            : base(principal)
        {
        }

        public string Name
        {
            get
            {
                return FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Address
        {
            get
            {
                return FindFirst(ClaimTypes.StreetAddress).Value;
            }
        }
    }
}