using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class OrderController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Order
        [HttpGet]
        public ActionResult Tracking()
        {
            return View();
        }

        private static string GenerateHtml(Order order)
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
                                <td>{4}</td>
                            </tr>";

                return string.Format(row, productTag, i.Product.Category.Name, 
                                    i.Product.Description, i.Amount, i.Product.Price.ToString().Replace(".", ","));
            });
            var rowsTemplate = string.Join(string.Empty, rows);
            const string template = @"<div class='alert alert-info'>
                                            Número do pedido: {0} <br/>
                                            Data de solicitação: {1} <br/>
                                            Solicitante: {2} <br/>
                                            Status: {3} <br/>
                                            Total: {4} <br/>
                                     </div>
                                <table class='table table-bordered table-hover table-striped'>
                                    <tr>
                                        <th>Produto</th>
                                        <th>Categoria</th>
                                        <th>Descrição</th>
                                        <th>Quantidade</th>
                                        <th>Preço</th>  
                                    </tr>
                                    {5}
                                </table>
                            </div>";
            var owner = string.Format("{0} {1}", order.Owner.FirstName, order.Owner.LastName);
            return string.Format(template, order.OrderNumber,
                order.CreatedAt.ToString("dd/MM/yyyy hh:mm:ss"), owner, order.Status, order.Cart.Total.ToString().Replace(".", ","), rowsTemplate);
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


    }
}