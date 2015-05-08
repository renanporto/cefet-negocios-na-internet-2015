using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.WebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace FantasyStore.WebApp
{
    public class Startup
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });

            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<User>(
                    new UserStore<User>(new FantasyStoreDbContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<User>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                return usermanager;
            };
        }

        
    }
}