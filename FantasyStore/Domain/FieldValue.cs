using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class FieldValue
    {
        public FieldValue()
        {
            
        }

        public FieldValue(int id, Field field, string value)
        {
            Id = id;
            Field = field;
            Value = value;
        }

        public int Id { get; set; }
        public Field Field { get; set; }
        public string Value { get; set; }
    }
}
