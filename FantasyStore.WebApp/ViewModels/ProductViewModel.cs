﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyStore.WebApp.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PriceFrom { get; set; }
        public string PriceTo { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string ProductLink { get; set; }
        public string AddToCartLink { get; set; }
    }
}