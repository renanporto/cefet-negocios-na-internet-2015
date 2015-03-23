using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Product
    {
        private IEnumerable<Variant> _variants;
        public Product()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

        public IEnumerable<Variant> Variants 
        {
            get { return _variants ?? (_variants = new List<Variant>()); }
            set { _variants = value; }
        }
    }
}
