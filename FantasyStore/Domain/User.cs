using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FantasyStore.Domain
{
    public class User : IdentityUser
    {
        public User()
        {

        }

        private IEnumerable<Address> _addresses;
        private IEnumerable<Order> _orders; 
        public string Password { get; set; }

        public IEnumerable<Order> Orders
        {
            get { return _orders ?? (_orders = new List<Order>()); }
            set { _orders = value; }
        }

        public IEnumerable<Address> Addresses
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            set { _addresses = value; }
        }
    }
}
