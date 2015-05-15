using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStore.Infrastructure.Repositories
{
    public class ItemRepository
    {
        private readonly FantasyStoreDbContext _context;
        public ItemRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

    }
}
