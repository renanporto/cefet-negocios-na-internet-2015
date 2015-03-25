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

        public WishList(int id, Guid code, ICollection<Variant> variants)
        {
            Id = id;
            Code = code;
            Variants = variants;
        }

        private ICollection<Variant> _variants; 
        public int Id { get; set; }
        public Guid Code { get; set; }

        public ICollection<Variant> Variants
        {
            get { return _variants ?? (_variants = new List<Variant>()); }
            set { _variants = value; }
        }
    }
}
