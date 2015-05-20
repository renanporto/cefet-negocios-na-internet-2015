using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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


        private decimal? GetTotal(IEnumerable<Item> items)
        {
            var result = 0m;

            foreach (var item in items)
            {
                var subTotal = (item.Product.Price * item.Amount).Value;
                result += subTotal;
            }

            return result;
        }

        public void Add(int productId, int quantity)
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
                            Amount = quantity,
                            Product = _context.Products.SingleOrDefault(p => p.Id == productId),
                            ProductId = productId
                        };

                        cart.Items.Add(item);
                    }
                    else
                    {
                        item.Amount++;
                    }
                    cart.Total = GetTotal(cart.Items);
                }// caso não esteja na sessao, cria o carrinho
                else
                {
                    HttpContext.Current.Session["CartId"] = cartCode;
                    HttpContext.Current.Session.Timeout = 30;
                    var item = new Item
                    {
                        Amount = quantity,
                        Product = _context.Products.SingleOrDefault(p => p.Id == productId),
                        ProductId = productId
                    };
                    var cart = new Cart
                    {
                        Items = new List<Item> { item },
                        Code = cartCode
                    };

                    cart.Total = GetTotal(cart.Items);

                    Save(cart);
                }
            }
            else
            {
                var cart = GetUserCart(userId);

                if (cart == null)
                {
                    var user = _context.Users.Find(userId);
                    cart = new Cart { User = user, Code = Guid.NewGuid().ToString() };
                    var item = new Item
                    {
                        Amount = quantity,
                        Cart = cart,
                        ProductId = productId,
                        Product = _context.Products.FirstOrDefault(p => p.Id == productId)
                    };

                    cart.Items.Add(item);
                    cart.Total = GetTotal(cart.Items);
                    Save(cart);
                    HttpContext.Current.Session["CartId"] = cart.Code;
                    HttpContext.Current.Session.Timeout = 30;
                }
                else 
                {
                    var item = _context.Items.Include(i => i.Product).Include(i => i.Cart)
                                         .SingleOrDefault(i => i.ProductId == productId && i.Cart.Id == cart.Id);

                    if (item == null)
                    {
                        item = new Item
                        {
                            Amount = quantity,
                            Cart = cart,
                            ProductId = productId,
                            Product = _context.Products.FirstOrDefault(p => p.Id == productId)
                        };

                        cart.Items.Add(item);
                    }
                    else
                    {
                        item.Amount++;
                    }

                    cart.Total = GetTotal(cart.Items);
                    Update(cart);
                }

            }
        }

        public void Update(Cart cart)
        {
            _context.Carts.Attach(cart);
            _context.Entry(cart).State = EntityState.Modified;
        }

        public void Save(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public Cart GetCart(string cartCode)
        {
            return _context.Carts.Include(c => c.Items)
                                 .Include(c => c.Items.Select(i => i.Product.Images))
                                 .FirstOrDefault(c => c.Code.Equals(cartCode));
        }

        public Cart GetUserCart(string userId)
        {
            var cart = _context.Carts.Include(c => c.Items)
                                    .Include(c => c.Items.Select(i => i.Product.Images))
                                    .OrderByDescending(c => c.Id)
                                    .FirstOrDefault(c => c.User.Id.Equals(userId)
                                    && !_context.Payments.Any(p => p.Cart.Id == c.Id));

            return cart;
        }


        public void Remove(int productId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            
            var cartCode = userId == null ? GetCartCode() : GetUserCart(userId).Code;
            
            var item = _context.Items.Include(i => i.Product).Include(i => i.Cart)
                .FirstOrDefault(i => i.ProductId == productId && i.Cart.Code.Equals(cartCode));

            _context.Items.Attach(item);
            _context.Entry(item).State = EntityState.Deleted;

            var cart = GetCart(cartCode);
            cart.Total = GetTotal(cart.Items);
        }

        public Cart GetByCode(string cartCode)
        {
            return _context.Carts.Include(c => c.Items)
                .Include(c => c.Items.Select(i => i.Product))
                .Include(c => c.User)
                .FirstOrDefault(c => c.Code.Equals(cartCode));
        }

        public void UpdateCartTotal(Cart cart, Item item)
        {
            cart.Total = GetTotal(new List<Item>{item});
        }
    }
}
