using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Cart
    {
        public int Id { get; set; }

        public string Code { get; set; }
        public decimal? Total { get; set; }
        public User User { get; set; }
        private ICollection<Item> _items; 
        public ICollection<Item> Items
        {
            get { return _items ?? (_items = new List<Item>()); }
            set { _items = value; }
        }
    }
}
