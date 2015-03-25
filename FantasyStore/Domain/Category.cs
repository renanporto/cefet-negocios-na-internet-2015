using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Domain
{
    public class Category
    {
        public Category()
        {
            
        }

        public Category(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }
}
