using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Variant
    {
        private Dictionary<string, string> _specifications;
        public Variant()
        {

        }

        public Variant(int id, string name, decimal? price, string ean, Dictionary<string, string> specifications, int amountInStock)
        {
            Id = id;
            Name = name;
            Price = price;
            Ean = ean;
            AmountInStock = amountInStock;
            _specifications = specifications;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Ean { get; set; }
        public Product Product { get; set; }
        public int AmountInStock { get; set; }

        public Dictionary<string, string> Specifications 
        {
            get { return _specifications ?? (_specifications = new Dictionary<string, string>()); }
            set { _specifications = value; }
        }
    }
}
