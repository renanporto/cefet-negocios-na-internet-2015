using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.Migrations;
using FantasyStore.Services;
using FantasyStore.WebApp.Extensions;
using FantasyStore.WebApp.ViewModels;
using Microsoft.AspNet.Identity;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class CheckoutController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(int productId)
        {
            if (productId == 0) return View();

            _unitOfWork.Carts.Add(productId);
            _unitOfWork.Commit();

            return Json("Item adicionado com sucesso", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Remove(int productId)
        {
            if (productId == 0) return View();

            _unitOfWork.Carts.Remove(productId);
            _unitOfWork.Commit();

            return Json("Item removido com sucesso", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Cart()
        {
            var session = Session["CartId"];
            var user = _unitOfWork.Users.Get(User.Identity.GetUserId());
            if (session == null)
            {  
                if (user == null)
                {
                    ViewBag.Flag = 1;
                    return View();
                }

                var cart = _unitOfWork.Carts.GetUserCart(user.Id);

                if (cart == null || !cart.Items.Any())
                {
                    ViewBag.Flag = 1;
                    return View();                    
                }

                var hasDeliveryAddress = user.Addresses.Any(a => a.IsDeliveryAddress);
                if (!hasDeliveryAddress)
                {
                    const string message = @"<div class='alert alert-info'>Atenção. Você ainda não possui um endereço de entrega cadastrado. 
                                <a href='/Auth/CreateAddress'>Clique aqui para cadastrar </a></div>";
                    @ViewBag.Message = message;
                    @ViewBag.Disabled = "disabled";
                }

                var items = cart.Items.Select(i => i.ToCartItemViewModel());
                ViewBag.CartItems = items;
                return View();   
            }
            else
            {
                var hasDeliveryAddress = user.Addresses.Any(a => a.IsDeliveryAddress);
                if (!hasDeliveryAddress)
                {
                    const string message = @"<div class='alert alert-info'>Atenção. Você ainda não possui um endereço de entrega cadastrado. 
                                <a href='/Auth/CreateAddress'>Clique aqui para cadastrar </a></div>";
                    @ViewBag.Message = message;
                    @ViewBag.Disabled = "disabled";
                }

                var code = session.ToString();
                var userId = User.Identity.GetUserId();
                var cart = userId == null ? _unitOfWork.Carts.GetCart(code) : _unitOfWork.Carts.GetUserCart(userId);
                if (cart == null)
                {
                    ViewBag.Flag = 1;
                    return View();
                }

                if (!cart.Items.Any())
                {
                    ViewBag.Flag = 1;
                    return View();
                }
                var items = cart.Items.Select(i => i.ToCartItemViewModel());
                ViewBag.CartItems = items;
                return View();   
            }
        }

        private SelectList GetInstallmentListFormatted(decimal? total)
        {
            const int installmentMaxMultiplier = 10;
            var result = new List<SelectListItem>();

            for (var i = 1; i <= installmentMaxMultiplier; i++)
            {
                var value = decimal.Round((total/i).GetValueOrDefault(), 2)
                    .ToString(CultureInfo.InvariantCulture).Replace(".", ",");
                var item = new SelectListItem
                {
                    Text = i == 1 ? string.Format("R$ {0} à vista", value) 
                                  : string.Format("{0}x de R$ {1} sem juros", i, value),
                    Value = i.ToString(CultureInfo.InvariantCulture)
                };
                result.Add(item);
            }

            return new SelectList(result, "Value", "Text");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            
            var cart = _unitOfWork.Carts.GetUserCart(userId);
            var installmentList = GetInstallmentListFormatted(cart.Total);
            ViewBag.installment = installmentList;
            return View();
        }

        [HttpGet]
        public ActionResult OrderFinished()
        {
            var user = _unitOfWork.Users.Get(User.Identity.GetUserId());

            var order = user.Orders.OrderByDescending(o => o.Id).FirstOrDefault();

            ViewBag.OrderInfo = order;

            return View();
        }

        private void SendEmailMessage(Order order)
        {
            var itens = order.Cart.Items.Select(i => i.Product.Name);

            var joinedNames = string.Join(", ", itens);

            const string messageTemplate = @"<p>Número do pedido: {0}</p>
                                    <p>Status do pedido: {1}</p>
                                    <p>Data de solicitação: {2}</p>
                                    <p>Solicitante: {3}</p>
                                    <p>Itens: {4}</p>
                                    <p>Total: {5}</p>";

            var body = string.Format(messageTemplate, order.OrderNumber, order.Status, order.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"),
                string.Format("{0} {1}", order.Owner.FirstName, order.Owner.LastName), joinedNames, order.Cart.Total);

            EmailService.Send("Confirmação do pedido - Fantasy Store", body, order.Owner.UserName);
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {
            var cart = _unitOfWork.Carts.GetUserCart(User.Identity.GetUserId());
            var date = Convert.ToDateTime(model.ExpirationDate);
            var installmentValue = cart.Total/model.Installment;
            var user = _unitOfWork.Users.Get(User.Identity.GetUserId());
            var payment = new Payment
            {
                Cart = cart,
                CartCode = cart.Code,
                CreditCardNumber = model.CreditCardNumber,
                ExpirationDate = date,
                Installment = model.Installment,
                Value = cart.Total,
                InstallmentValue = installmentValue,
                Name = model.Name
            };
            var number = DateTime.UtcNow.Ticks / 10000;
            var order = new Order
            {
                Cart = cart,
                CreatedAt = DateTime.UtcNow,
                Owner = user,
                Status = "Pendente",
                OrderNumber = number
            };

            _unitOfWork.Payments.Save(payment);
            _unitOfWork.Orders.Save(order);
            _unitOfWork.Commit();

            SendEmailMessage(order);
            return RedirectToAction("OrderFinished", "Checkout");
        }
            



    }
}