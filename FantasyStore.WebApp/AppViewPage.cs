using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FantasyStore.WebApp
{

    // view page customizada para fornecer acesso ao AppUser
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        private UserManager<User> GetUserManager()
        {
            var usermanager = new UserManager<User>(
                    new UserStore<User>(new FantasyStoreDbContext()));
            // allow alphanumeric characters in username
            usermanager.UserValidator = new UserValidator<User>(usermanager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            return usermanager;
        }
        protected User CurrentUser
        {
            get
            {
                var userManager = GetUserManager();
                return userManager.FindById(User.Identity.GetUserId());
            }
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    } 
}