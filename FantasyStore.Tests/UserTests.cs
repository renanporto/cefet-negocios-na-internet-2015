using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.WebApp;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NUnit.Framework;

namespace FantasyStore.Tests
{
    public class UserTests
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

        [Test]
        public async Task test_register_user()
        {
            var userManager = GetUserManager();

            var user = new User
            {
                FirstName = "Renan",
                LastName = "Porto",
                Document = "144.433.857-97",
                BirthDate = new DateTime(1992, 08, 30),
                Email = "renan.porto@vtex.com.br",
                UserName = "renan.porto@vtex.com.br",
            };

            var result = await userManager.CreateAsync(user, "renanporto");

            Assert.True(result.Succeeded);
        }
    }
}
