using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Product
    {
        public Product()
        {

        }

        public Product(int id, string name, decimal? price, string ean, int amountInStock)
        {
            Id = id;
            Name = name;
            Price = price;
            Ean = ean;
            AmountInStock = amountInStock;
        }

        private ICollection<Image> _images; 
        private ICollection<WishList> _wishLists; 

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Ean { get; set; }
        public Category Category { get; set; }
        public int AmountInStock { get; set; }

        public ICollection<WishList> WishLists
        {
            get { return _wishLists ?? (_wishLists = new List<WishList>()); }
            set { _wishLists = value; }
        }

        public ICollection<Image> Images
        {
            get { return _images ?? (_images = new List<Image>()); }
            set { _images = value; }
        }

    }
}
