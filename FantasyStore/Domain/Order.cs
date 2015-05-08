using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Order
    {
        private IEnumerable<Item> _items; 
        public int Id { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Item> Items
        {
            get { return _items ?? (_items = new List<Item>()); }
            set { _items = value; }
        }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public Variant Variant { get; set; }
        public Order Order { get; set; }
    }
}
