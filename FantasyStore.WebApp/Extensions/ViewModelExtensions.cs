using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using FantasyStore.Domain;
using FantasyStore.WebApp.ViewModels;

namespace FantasyStore.WebApp.Extensions
{
    public static class ViewModelExtensions
    {
        public static RegisterModel ToViewModel(this User user)
        {
            
            return new RegisterModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Document = user.Document,
                BirthDate = user.BirthDate.HasValue ? user.BirthDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                Email = user.UserName
            };
        }
    }
}