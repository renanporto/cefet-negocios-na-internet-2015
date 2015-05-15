using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string  Document { get; set; }

        private IEnumerable<Address> _addresses;
        private IEnumerable<Order> _orders; 

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
