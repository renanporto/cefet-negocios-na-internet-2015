using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyStore.WebApp.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ShippingValue { get; set; }
        public string Price { get; set; }
        public string CategoryName { get; set; }
        public int Amount { get; set; }
        public string ImageUrl { get; set; }
    }
}