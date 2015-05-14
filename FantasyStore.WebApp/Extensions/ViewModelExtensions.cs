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

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            var priceFrom = product.Price + 20;
            return new ProductViewModel
            {
                Name = product.Name,
                ImageUrl = product.Images.First().Url,
                ImageName = product.Images.First().Name,
                PriceFrom = priceFrom.ToString(),
                PriceTo = product.Price.ToString()
            };
        }
    }
}