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
            var priceFrom = product.Price + 40;
            var productLink = string.Format("/Products/{0}", product.Id);
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.Images.First().Url,
                ImageName = product.Images.First().Name,
                PriceFrom = priceFrom.Value.ToString().Replace(".", ","),
                PriceTo = product.Price.ToString().Replace(".", ","),
                ProductLink = productLink
            };
        }

        public static CartItemViewModel ToCartItemViewModel(this Item item)
        {
            var price = item.Product.Price.ToString().Replace(".", ",");
            return new CartItemViewModel
            {
                Id = item.Id,
                Amount = item.Amount,
                ProductName = item.Product.Name,
                Price = price,
                ImageUrl = item.Product.Images.First().Url,
                ProductId = item.Product.Id
            };
        }
    }
}