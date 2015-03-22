using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class AddressRepository
    {
        private readonly FantasyStoreDbContext _context;
        public AddressRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public Address Get(int id)
        {
            return _context.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAll()
        {
            return _context.Addresses;
        }

        public void Insert(Address address)
        {
            _context.Addresses.Add(address);
        }

        public void Delete(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public void Update(Address order)
        {
            _context.Addresses.Attach(order);
            _context.Entry(order).State = EntityState.Modified; ;
        }
    }
}
