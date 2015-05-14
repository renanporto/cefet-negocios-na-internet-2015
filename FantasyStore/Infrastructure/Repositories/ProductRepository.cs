using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private readonly FantasyStoreDbContext _context;
        public ProductRepository(FantasyStoreDbContext context)
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

        public IEnumerable<Product> GetByCollection(int collectionId)
        {
            return _context.Collections.Include(c => c.Products)
                                       .Include(c=> c.Products.Select(p => p.Images))
                                       .First(c => c.Id == collectionId).Products;
        }

        public IEnumerable<Product> GetByCategory(string category)
        {
            return _context.Products.Include(p => p.Images)
                                    .Include(p => p.Category)
                                    .Where(p => p.Category.Name.Equals(category));
        }

        public void Insert(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Update(Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;;
        }
    }
}
