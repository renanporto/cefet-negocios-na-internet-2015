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

        public Variant Get(int id)
        {
            return _context.Variants.Find(id);
        }

        public IEnumerable<Variant> GetAll()
        {
            return _context.Variants;
        }

        public IEnumerable<Variant> GetByName(string name)
        {
            return _context.Variants.Where(p => p.Name.Contains(name));
        }

        public void Save(Variant variant)
        {
            _context.Variants.Add(variant);
        }

        public void Delete(Variant variant)
        {
            _context.Variants.Remove(variant);
        }

        public void Update(Variant variant)
        {
            _context.Variants.Attach(variant);
            _context.Entry(variant).State = EntityState.Modified; ;
        }
    }
}
