using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Order
    {
        public int Id { get; set; }

        public User Owner { get; set; }

        public Cart Cart { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }

        public long OrderNumber { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int? ProductId { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
