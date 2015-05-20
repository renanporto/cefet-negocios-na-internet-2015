using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class ProductFieldValueRepository
    {
        private readonly FantasyStoreDbContext _context;
        public ProductFieldValueRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductFieldValue> GetByProduct(int productId)
        {
            return _context.ProductFieldValues.Include(p => p.Product).Where(a => a.Product.Id == productId);
        }
        
    }
}
