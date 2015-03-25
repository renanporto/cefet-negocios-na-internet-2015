using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Variant
    {
        public Variant()
        {

        }

        public Variant(int id, string name, decimal? price, string ean, int amountInStock)
        {
            Id = id;
            Name = name;
            Price = price;
            Ean = ean;
            AmountInStock = amountInStock;
        }

        private ICollection<WishList> _wishLists; 

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Ean { get; set; }
        public Product Product { get; set; }
        public int AmountInStock { get; set; }

        public ICollection<WishList> WishLists
        {
            get { return _wishLists ?? (_wishLists = new List<WishList>()); }
            set { _wishLists = value; }
        }

    }
}
