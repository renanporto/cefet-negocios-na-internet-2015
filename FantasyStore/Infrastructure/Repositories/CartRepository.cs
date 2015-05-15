using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FantasyStore.Domain;
using Microsoft.AspNet.Identity;

namespace FantasyStore.Infrastructure.Repositories
{
    public class CartRepository
    {
        private readonly FantasyStoreDbContext _context;
        public CartRepository(FantasyStoreDbContext context)
        {
            _context = context;
        }


        // se não estiver na sessão, gera o maior id
        private string GetCartCode()
        {
            var sessionCartId = HttpContext.Current.Session["CartId"];
            if (sessionCartId == null)
            {
                return Guid.NewGuid().ToString();
            }

            return sessionCartId.ToString();
        }

        public void Add(int productId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            // usuário visitante
            if (userId == null)
            {
                var cartCode = GetCartCode();

                // pega o id do carrinho. se tiver na sessao, é pq existe no banco e é só consultar
                if (_context.Carts.Any(c => c.Code.Equals(cartCode)))
                {
                    var cart = _context.Carts.FirstOrDefault(c => c.Code.Equals(cartCode));
                    var item = _context.Items.Include(i => i.Product).Include(i => i.Cart)
                        .SingleOrDefault(i => i.Cart.Code == cartCode && i.ProductId == productId);
                    // trata existencia do item
                    if (item == null)
                    {
                        item = new Item
                        {
                            Amount = 1,
                            Product = _context.Products.SingleOrDefault(p => p.Id == productId),
                            ProductId = productId
                        };
                        cart.Total += item.Product.Price;
                        cart.Items.Add(item);
                    }
                    else
                    {
                        item.Amount++;
                        cart.Total += item.Product.Price;
                    }
                }// caso não esteja na sessao, cria o carrinho
                else
                {
                    HttpContext.Current.Session["CartId"] = cartCode;
                    var item = new Item
                    {
                        Amount = 1,
                        Product = _context.Products.SingleOrDefault(p => p.Id == productId),
                        ProductId = productId
                    };
                    var cart = new Cart
                    {
                        Items = new List<Item> { item },
                        Total = item.Product.Price,
                        Code = cartCode
                    };

                    Save(cart);
                }
            }
            else
            {
                var cart = _context.Carts.SingleOrDefault(c => c.User.Id == userId);

                var item = _context.Items.Include(i => i.Product).Include(i => i.Cart)
                                         .SingleOrDefault(i => i.ProductId == productId && i.Cart.Id == cart.Id);

                if (item == null)
                {
                    item = new Item
                    {
                        Amount = 1,
                        Cart = cart,
                        ProductId = productId,
                        Product = _context.Products.FirstOrDefault(p => p.Id == productId)
                    };
                }
                else
                {
                    item.Amount++;
                }
                
                if (cart == null)
                {
                    cart = new Cart
                    {
                        Items = new List<Item> {item},
                        Total = item.Product.Price,
                        User = _context.Users.Find(userId)
                    };
                }
                cart.Items.Add(item);
            }
        }

        public void Save(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public Cart GetUserCart(string userId)
        {
            return _context.Carts.FirstOrDefault(c => c.User.Id.Equals(userId));
        }
    }
}
