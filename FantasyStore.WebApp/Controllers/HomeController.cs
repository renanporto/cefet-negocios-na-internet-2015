using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Infrastructure;

namespace FantasyStore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Home
        public ActionResult Index()
        {
            var products = unitOfWork.Products.GetAll();
            return View(products);
        }

        public ActionResult Index(string term)
        {
            var productsByCategory = unitOfWork.Products.GetByCategory(term);
            if (!productsByCategory.Any())
            {
                var productsByBrand = unitOfWork.Products.GetByBrand(term);
                if (!productsByBrand.Any())
                {
                    //Retornar uma página amigável com esse conteúdo
                    return Content(string.Format("Não há produtos na categoria ou marca {0}", term));
                }
                return View(productsByBrand);
            }
            return View(productsByCategory);
        }
    }
}