using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FantasyStore.WebApp.ViewModels
{
    public class PaymentViewModel
    {
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public int Installment { get; set; }
        public string Name { get; set; }

    }
}