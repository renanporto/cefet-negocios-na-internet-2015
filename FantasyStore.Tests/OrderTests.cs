using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using NUnit.Framework;

namespace FantasyStore.Tests
{ 
    public class OrderTests
    {
        public Product Product
        {
            get
            {
                return new Product
                {
                    Id = 1,
                    Name = "Cafeteira Nespresso Modelo 24453"
                };
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {

                return new List<Product>
                {
                     new Product(1, "110v", 300, "EAN1234", 10),
                     new Product(2, "220v", 300, "EAN4567", 15)
                 };
            }
        }

        [Test]
        public void create_order()
        {
            var products = Products.ToList();

            var order = new Order
            {
                Id = 1,
                CreatedAt = DateTime.UtcNow,
                Status = "Pendente"
            };

            var items = new List<Item>
            {
                new Item { Id = 1, Product = products[0], Amount = 1, Order = order },
                new Item { Id = 2, Product = products[1], Amount = 1, Order = order }
            };


            using (var context = new FantasyStoreDbContext())
            {
                context.Orders.Add(order);
                items.ForEach(i => context.Items.Add(i));
                products.ForEach(v => context.Products.Add(v));
                context.SaveChanges();
            }
        }




    }
}
