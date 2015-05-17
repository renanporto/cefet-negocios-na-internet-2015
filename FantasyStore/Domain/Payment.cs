using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public Cart Cart { get; set; }
        public string CartCode { get; set; }
        public decimal? Value { get; set; }
        public int Installment { get; set; }
        public decimal? InstallmentValue { get; set; }

    }
}
