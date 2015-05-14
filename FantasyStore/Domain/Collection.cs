using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private ICollection<Product> _products;
        public ICollection<Product> Products
        {
            get
            {
                return _products ?? (_products = new List<Product>());
            }
            set
            {
                _products = value;
            }
        }
    }
}
