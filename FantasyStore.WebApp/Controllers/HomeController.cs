﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.Services;
using FantasyStore.WebApp.Extensions;
using FantasyStore.WebApp.ViewModels;
using Microsoft.AspNet.Identity;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : AppController
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Home
        public ActionResult Index()
        {
            var cartCode = Session["CartId"];
            if (cartCode == null || User.Identity.GetUserId() == null) return View();

            var cart = _unitOfWork.Carts.GetCart(cartCode.ToString());
            if (cart.User != null) return View();

            cart.User = _unitOfWork.Users.Get(User.Identity.GetUserId());
            _unitOfWork.Carts.Update(cart);
            _unitOfWork.Commit();
            return View();
        }

        private string DisplaySuccessMessage(string message)
        {
            return string.Format("<div class='alert alert-success'>{0}</div>", message);
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            const string messageTemplate = @"<p>Nome: {0}</p>
                                    <p>Sobrenome: {1}</p>
                                    <p>Telefone para contato: {2}</p>
                                    <p>Email: {3}</p>
                                    <p>Tipo de solicitação: {4}</p>
                                    <p>Mensagem: {5}</p>";

            var body = string.Format(messageTemplate, model.FirstName, model.LastName, model.Phone, model.Email,
                model.RequestType, model.Message);

            EmailService.Send(model.RequestType, body);
            ViewBag.Confirmation = DisplaySuccessMessage("O email foi enviado com sucesso!");
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public PartialViewResult _Section1()
        {
            var products = _unitOfWork.Products.GetByCollection(1);
            var model = products.Select(p => p.ToProductViewModel()).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _Section2()
        {
            var products = _unitOfWork.Products.GetByCollection(2);
            var model = products.Select(p => p.ToProductViewModel()).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _Section3()
        {
            var products = _unitOfWork.Products.GetByCategory("Infantil");
            var model = products.Select(p => p.ToProductViewModel()).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        private Tuple<int, int> ExtractPrices(string term)
        {
            var trim = term.Replace(" ", "");
            var match = Regex.Match(trim, "([0-9]+)-([0-9]+)");

            if (!match.Success) throw new Exception("O termo não está no formato correto");

            var slice1 = Regex.Match(trim, "([0-9]+)").Groups[0].Value;
            var slice2 = Regex.Match(trim, "-([0-9]+)").Groups[0].Value.Replace("-", "");
            var price1 = Convert.ToInt32(slice1);
            var price2 = Convert.ToInt32(slice2);
            return new Tuple<int, int>(price1, price2);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Term))
            {
                return RedirectToAction("Index");
            }
            switch (model.Criteria)
            {
                case "1":
                    {
                        try
                        {
                            var id = Convert.ToInt32(model.Term);
                            var product = _unitOfWork.Products.Get(id);
                            if (product == null)
                            {
                                ViewBag.NotFoundMessage = string.Format("Nenhum produto encontrado com o código {0}", model.Term);
                                return View();
                            }
                            var result = new List<ProductViewModel>
                            {
                                product.ToProductViewModel()
                            };
                            ViewBag.SearchResult = result;
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = string.Format("Ocorreu um erro inesperado: {0}", ex.Message);
                        }
                    }
                    break;
                case "2":
                    {
                        try
                        {
                            var products = _unitOfWork.Products.GetByName(model.Term);
                            if (!products.Any())
                            {
                                ViewBag.NotFoundMessage = string.Format("Nenhum produto encontrado com o nome {0}", model.Term);
                                return View();
                            }
                            var result = products.Select(p => p.ToProductViewModel());
                            ViewBag.SearchResult = result;
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = string.Format("Ocorreu um erro inesperado: {0}", ex.Message);
                        }
                    }
                    break;
                case "3":
                    {
                        try
                        {
                            var tuple = ExtractPrices(model.Term);
                            var products = _unitOfWork.Products.GetByPriceRange(tuple.Item1, tuple.Item2);
                            if (!products.Any())
                            {
                                ViewBag.NotFoundMessage = string.Format("Nenhum produto encontrado com a faixa de preço {0}", model.Term);
                                return View();
                            }
                            var result = products.Select(p => p.ToProductViewModel());
                            ViewBag.SearchResult = result;
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = string.Format("Ocorreu um erro inesperado: {0}", ex.Message);
                        }
                    }
                    break;
            }

            return View();
        }

        [HttpGet]
        public ActionResult Category(int id)
        {
            var products = _unitOfWork.Products.GetByCategoryId(id);
            ViewBag.CategoryName = products.Select(p => p.Category.Name).First();
            var result = products.Select(p => p.ToProductViewModel());
            ViewBag.Products = result;
            return View();
        }


        [HttpGet]
        public ActionResult Products(int id)
        {
            var product = _unitOfWork.Products.Get(id);

            ViewBag.Lists = _unitOfWork.WishLists.GetAll();

            ViewBag.ImageUrl = product.Images.FirstOrDefault().Url;
            ViewBag.Name = product.Name;
            ViewBag.Id = product.Id;
            ViewBag.Category = product.Category.Name;
            ViewBag.Price = (product.Price).ToString().Replace(".", ",");
            ViewBag.OldPrice = (product.Price + 40).ToString().Replace(".", ",");
            ViewBag.ProductId = product.Id;

            var productFieldValues = _unitOfWork.FieldValues.GetByProduct(product.Id);

            ViewBag.Description = product.Description;
            ViewBag.Gender = productFieldValues.FirstOrDefault(f => f.Field.Equals("Genero")).Value;
            ViewBag.Sizes = productFieldValues.FirstOrDefault(f => f.Field.Equals("Tamanhos")).Value;
            ViewBag.Composition = productFieldValues.FirstOrDefault(f => f.Field.Equals("Composicao")).Value;
            ViewBag.IncludedItems = productFieldValues.FirstOrDefault(f => f.Field.Equals("Itens inclusos")).Value;

            var installmentOptions = GetInstallmentOptions(product);

            ViewBag.InstallmentOptions = installmentOptions;

            return View();
        }

        private List<string> GetInstallmentOptions(Product product)
        {
            var price = product.Price;
            var result = new List<string>();
            for (var i = 1; i <= 6; i++)
            {
                var value = decimal.Round((price / i).GetValueOrDefault(), 2)
                    .ToString(CultureInfo.InvariantCulture).Replace(".", ",");
                var str = i == 1
                    ? string.Format("R$ {0} à vista", value)
                    : string.Format("{0}x de R$ {1} sem juros", i, value);
                result.Add(str);
            }
            return result;
        }
    }
}
