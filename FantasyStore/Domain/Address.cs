using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Address
    {
        public Address()
        {

        }

        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Complement { get; set; }
        public bool IsDeliveryAddress { get; set; }
        public User User { get; set; }
    }
}
