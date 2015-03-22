using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Client
    {
        public Client()
        {

        }

        private IEnumerable<Address> _addresses;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Address> Addresses 
        {
            get { return _addresses ?? (_addresses = new List<Address>()); }
            set { _addresses = value; } 
        }

        public User User { get; set; }
    }
}
