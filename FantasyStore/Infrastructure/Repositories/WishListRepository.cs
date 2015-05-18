using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class WishListRepository
    {
        private readonly FantasyStoreDbContext _context;
        public WishListRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WishList> GetAll()
        {
            return _context.WishLists.Include(w => w.Products).Include(w => w.User);
        }

        public WishList GetByCode(string code)
        {
            return
                _context.WishLists.Include(w => w.Products)
                    .Include(w => w.User)
                    .FirstOrDefault(w => w.Code.Equals(code));
        }

        public IEnumerable<WishList> GetByName(string name)
        {
            return
                _context.WishLists.Include(w => w.Products)
                    .Include(w => w.User)
                    .Where(w => w.Name.Contains(name));
        }


        public IEnumerable<WishList> GetByOwner(string ownerId)
        {
           return _context.WishLists.Include(w => w.Products)
                     .Include(w => w.User)
                         .Where(w => w.User.Id.Equals(ownerId));
        }

        public void Save(WishList wishList)
        {
            _context.WishLists.Add(wishList);
        }
    }
}
