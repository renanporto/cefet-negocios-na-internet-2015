using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.WebApp.Util;
using Microsoft.AspNet.Identity;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class OrdersController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Order
        [HttpGet]
        public ActionResult Tracking()
        {
            return View();
        }

        private string GenerateHtml(Order order)
        {
            var items = order.Cart.Items;

            var rows = items.Select(i =>
            {
                var productTag = string.Format(@"<div>
                        <img src='{0}' alt='{1}' style='width: 70px; height: 70px;' />
                        <strong>{2} </strong><br />
                    </div>", i.Product.Images.First().Url, i.Product.Images.First().Name, i.Product.Name);
                const string row = @"<tr>
                                <td>{0}</td>
                                <td>{1}</td>
                                <td>{2}</td>
                                <td>{3}</td>
                                <td>R$ {4}</td>
                            </tr>";

                return string.Format(row, productTag, i.Product.Category.Name, 
                                    i.Product.Description, i.Amount, i.Product.Price.ToString().Replace(".", ","));
            });
            var rowsTemplate = string.Join(string.Empty, rows);
            const string template = @"<div class='alert alert-info'>
                                            Número do pedido: <strong>{0}</strong> <br/>
                                            Data de solicitação: <strong>{1}</strong> <br/>
                                            Solicitante: <strong>{2}</strong> <br/>
                                            Status: <strong>{3}</strong> <br/>
                                            Forma de pagamento: <strong>{4}</strong><br/>
                                            Total: <strong>R$ {5}</strong> <br/>
                                     </div>
                                <table class='table table-bordered table-hover table-striped'>
                                    <tr>
                                        <th>Produto</th>
                                        <th>Categoria</th>
                                        <th>Descrição</th>
                                        <th>Quantidade</th>
                                        <th>Preço</th>  
                                    </tr>
                                    {6}
                                </table>";
            var payment = _unitOfWork.Payments.GetByCartId(order.Cart.Id);
            var paymentDetails = payment.Installment == 1 ? string.Format("R$ {0} à vista", payment.InstallmentValue) 
                                    : string.Format("{0}x de  R$ {1}", payment.Installment, payment.InstallmentValue.ToString().Replace(".", ","));
            var owner = string.Format("{0} {1}", order.Owner.FirstName, order.Owner.LastName);
            var date = order.CreatedAt.ConvertFromUtc();
            return string.Format(template, order.OrderNumber,
                date.ToString("dd/MM/yyyy hh:mm:ss"), owner, order.Status, 
                paymentDetails, order.Cart.Total.ToString().Replace(".", ","), rowsTemplate);
        }

        [HttpGet]
        public ActionResult Search(string orderNumber)
        {
            long number;
            try
            {
                number = Convert.ToInt64(orderNumber);
            }
            catch (FormatException)
            {
                return Json("<div class='alert alert-danger'>Forneça um número válido. </div>");
            }

            var order = _unitOfWork.Orders.GetByNumber(number);

            var html = GenerateHtml(order);

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var orders = _unitOfWork.Orders.GetByOwner(User.Identity.GetUserId());
            var result = new StringBuilder();

            foreach (var order in orders)
            {
                var html = GenerateHtml(order);
                result.Append(html);
                result.Append("<hr/>");
            }

            ViewBag.Orders = result;
            return View();
        }
    }
}