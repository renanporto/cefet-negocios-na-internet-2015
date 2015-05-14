using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class VariantRepository
    {
         private readonly FantasyStoreDbContext _context;
         public VariantRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return _context.Products.Where(p => p.Name.Contains(name));
        }

        public void Insert(Product variant)
        {
            _context.Products.Add(variant);
        }

        public void Delete(Product variant)
        {
            _context.Products.Remove(variant);
        }

        public void Update(Product variant)
        {
            _context.Products.Attach(variant);
            _context.Entry(variant).State = EntityState.Modified; ;
        }
    }
}
