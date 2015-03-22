using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class ClientRepository
    {
        private readonly FantasyStoreDbContext _context;
        public ClientRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public Client Get(int id)
        {
            return _context.Clients.Find(id);
        }

        public IEnumerable<Address> GetAddresses(int clientId)
        {
            return _context.Clients.Find(clientId).Addresses;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients;
        }

        public void Insert(Client client)
        {
            _context.Clients.Add(client);
        }

        public void Delete(Client client)
        {
            _context.Clients.Remove(client);
        }

        public void Update(Client client)
        {
            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Modified; ;
        }
    }
}
