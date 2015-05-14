using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Infrastructure;
using FantasyStore.Services;
using FantasyStore.WebApp.Extensions;
using FantasyStore.WebApp.ViewModels;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : AppController
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Index(string term)
        //{
        //    var productsByCategory = unitOfWork.Products.GetByCategory(term);
        //    if (!productsByCategory.Any())
        //    {
        //        var productsByBrand = unitOfWork.Products.GetByBrand(term);
        //        if (!productsByBrand.Any())
        //        {
        //            //Retornar uma página amigável com esse conteúdo
        //            return Content(string.Format("Não há produtos na categoria ou marca {0}", term));
        //        }
        //        return View(productsByBrand);
        //    }
        //    return View(productsByCategory);
        //}

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
            var products = unitOfWork.Products.GetByCollection(1);
            var model = products.Select(p => p.ToProductViewModel()).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _Section2()
        {
            var products = unitOfWork.Products.GetByCollection(2);
            var model = products.Select(p => p.ToProductViewModel()).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public PartialViewResult _Section3()
        {
            var products = unitOfWork.Products.GetByCategory("Infantil");
            var model = products.Select(p => p.ToProductViewModel()).ToList();
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

    }
}