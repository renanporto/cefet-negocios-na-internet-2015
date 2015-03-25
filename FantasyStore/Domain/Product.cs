using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Product
    {
        private ICollection<Variant> _variants;
        public Product()
        {

        }

        public Product(int id, string name, string brand, Category category, ICollection<Variant> variants)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Category = category;
            Variants = variants;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public Category Category { get; set; }

        public ICollection<Variant> Variants 
        {
            get { return _variants ?? (_variants = new List<Variant>()); }
            set { _variants = value; }
        }
    }
}
