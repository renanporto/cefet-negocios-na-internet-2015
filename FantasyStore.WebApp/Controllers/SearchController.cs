using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Infrastructure;
using FantasyStore.WebApp.Extensions;
using FantasyStore.WebApp.ViewModels;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        public ActionResult Index()
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

        [HttpPost]
        public ActionResult Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            switch (model.Criteria)
            {
                case "1":
                    {
                        try
                        {
                            var id = Convert.ToInt32(model.Term);
                            var product = unitOfWork.Products.Get(id);
                            var viewModel = new List<ProductViewModel>
                        {
                            product.ToProductViewModel()
                        };
                            return View(viewModel);
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
                            var products = unitOfWork.Products.GetByName(model.Term);
                            var viewModel = products.Select(p => p.ToProductViewModel());
                            return View(viewModel);
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
                            var products = unitOfWork.Products.GetByPriceRange(tuple.Item1, tuple.Item2);
                            var viewModel = products.Select(p => p.ToProductViewModel());
                            return View(viewModel);
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
    }
}