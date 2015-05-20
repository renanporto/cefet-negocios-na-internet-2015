using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class ItemRepository
    {
        private readonly FantasyStoreDbContext _context;
        public ItemRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public void Update(Item item)
        {
            _context.Items.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
