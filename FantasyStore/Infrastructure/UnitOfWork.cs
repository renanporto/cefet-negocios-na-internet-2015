using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Infrastructure.Repositories;

namespace FantasyStore.Infrastructure
{
    public class UnitOfWork
    {
        private readonly FantasyStoreDbContext _context = new FantasyStoreDbContext();
        private VariantRepository _variantRepository;
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        public ProductRepository Products
        {
            get
            {
                return _productRepository ?? (_productRepository = new ProductRepository(_context));
            }
        }

        public VariantRepository Variants
        {
            get
            {
                return _variantRepository ?? (_variantRepository = new VariantRepository(_context));
            }
        }

        public OrderRepository Orders
        {
            get
            {
                return _orderRepository ?? (_orderRepository = new OrderRepository(_context));
            }
        }

    }
}
