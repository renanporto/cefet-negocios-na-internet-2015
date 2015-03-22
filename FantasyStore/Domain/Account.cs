using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Account
    {
        public Account()
        {

        }

        public Account(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        private IEnumerable<Order> _orders; 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public IEnumerable<Order> Orders
        {
            get { return _orders ?? (_orders = new List<Order>()); }
            set { _orders = value; }
        }
    }
}
