using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class ProductFieldValue
    {
        public ProductFieldValue()
        {
            
        }

        public ProductFieldValue(int id, FieldValue fieldValue, Product product)
        {
            Id = id;
            FieldValue = fieldValue;
            Product = product;
        }

        public int Id { get; set; }
        public FieldValue FieldValue { get; set; }
        public Product Product { get; set; }
    }
}
