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
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;
        private AddressRepository _addressRepository;
        private CartRepository _cartRepository;
        private ItemRepository _items;
        private UserRepository _users;
        private PaymentRepository _payments;
        private WishListRepository _wishLists;

        public WishListRepository WishLists
        {
            get
            {
                return _wishLists ?? (_wishLists = new WishListRepository(_context));
            }
        }

        public ItemRepository Items
        {
            get
            {
                return _items ?? (_items = new ItemRepository(_context));
            }
        }

        public PaymentRepository Payments
        {
            get
            {
                return _payments ?? (_payments = new PaymentRepository(_context));
            }
        }

        public UserRepository Users
        {
            get
            {
                return _users ?? (_users = new UserRepository(_context));
            }
        }

        public CartRepository Carts
        {
            get
            {
                return _cartRepository ?? (_cartRepository = new CartRepository(_context));
            }
        }

        public ProductRepository Products
        {
            get
            {
                return _productRepository ?? (_productRepository = new ProductRepository(_context));
            }
        }

        public OrderRepository Orders
        {
            get
            {
                return _orderRepository ?? (_orderRepository = new OrderRepository(_context));
            }
        }

        public AddressRepository Addresses
        {
            get
            {
                return _addressRepository ?? (_addressRepository = new AddressRepository(_context));
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
