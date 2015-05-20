using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class ProductFieldValue
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}
