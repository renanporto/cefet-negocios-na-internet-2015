using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure.Repositories
{
    public class PaymentRepository
    {
        private readonly FantasyStoreDbContext _context;
        public PaymentRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }

        public void Save(Payment payment)
        {
            _context.Payments.Add(payment);
        }


        public bool HasPayment(Cart cart)
        {
            return _context.Payments.Any(p => p.Cart.Id == cart.Id);
        }
    }
}
