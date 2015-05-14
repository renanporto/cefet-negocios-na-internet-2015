using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class WishList
    {
        public WishList()
        {
            
        }

        public WishList(int id, Guid code, ICollection<Product> products)
        {
            Id = id;
            Code = code;
            Products = products;
        }

        private ICollection<Product> _products; 
        public int Id { get; set; }
        public Guid Code { get; set; }

        public ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            set { _products = value; }
        }
    }
}
