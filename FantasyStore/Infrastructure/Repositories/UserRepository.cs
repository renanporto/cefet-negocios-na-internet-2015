using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly FantasyStoreDbContext _context;
        public UserRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public User Get(string userId)
        {
            return _context.Users.Include(u => u.Addresses).Include(u => u.Orders).FirstOrDefault(u => u.Id.Equals(userId));
        }
        


    }
}
