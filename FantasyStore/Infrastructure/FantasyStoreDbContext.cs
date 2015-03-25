using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyStore.Domain;

namespace FantasyStore.Infrastructure
{
    public class FantasyStoreDbContext : DbContext
    {
        public FantasyStoreDbContext()
            : base("name=FantasyStoreStr")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldValue> FieldValues { get; set; }
        public DbSet<ProductFieldValue> ProductFieldValues { get; set; }
    }
}
