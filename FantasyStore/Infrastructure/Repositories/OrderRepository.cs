using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly FantasyStoreDbContext _context;
        public OrderRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public Order Get(int id)
        {
            return _context.Orders.Find(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders;
        }

        //public IEnumerable<Order> GetByOwner(int ownerId)
        //{
        //    return _context.Orders.Where(o => o.Owner.Id == ownerId);
        //}

        public void Insert(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified; ;
        }
    }
}
