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
                return FindFirst(ClaimTypes.Name) != null ? FindFirst(ClaimTypes.Name).Value : string.Empty;
            }
        }

        public string Address
        {
            get
            {
                return FindFirst(ClaimTypes.StreetAddress) != null ? FindFirst(ClaimTypes.StreetAddress).Value : string.Empty;
            }
        }
    }
}