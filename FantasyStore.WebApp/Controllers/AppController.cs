using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using FantasyStore.WebApp.Models;

namespace FantasyStore.WebApp.Controllers
{
    public class AppController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(User as ClaimsPrincipal);
            }
        }
	}
}