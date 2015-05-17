using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyStore.WebApp.ViewModels
{
    public class AddressViewModel
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Complement { get; set; }
    }
}