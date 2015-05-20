using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.WebApp.Util;
using FantasyStore.WebApp.ViewModels;
using Microsoft.AspNet.Identity;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class WishListsController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpGet]
        public ActionResult All()
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var wishLists = _unitOfWork.WishLists.GetByOwner(User.Identity.GetUserId());

            ViewBag.WishLists = wishLists.Any() ? wishLists : null;

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (id == 0) throw new ArgumentException("O id fornecido não pode ser zero.");

            var wishList = _unitOfWork.WishLists.Get(id);

            if (wishList == null)
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }

            @ViewBag.WishList = wishList;
            @ViewBag.CreatedAt = wishList.CreatedAt.ConvertFromUtc().ToString("dd/MM/yyyy hh:mm:ss");


            @ViewBag.Products = wishList.Products.Any() ? wishList.Products : null;
            return View();
        }

        public ActionResult AddToList(int listId, int productId)
        {
            var product = _unitOfWork.Products.Get(productId);
            var wishList = _unitOfWork.WishLists.Get(listId);
            wishList.Products.Add(product);
            _unitOfWork.WishLists.Update(wishList);
            _unitOfWork.Commit();

            return Json(wishList.Id, JsonRequestBehavior.AllowGet);
        }

        private static string GenerateWishListsHtml(IEnumerable<WishList> wishLists)
        {
            var rows = wishLists.Select(w =>
            {
                const string row = @"<tr>
                                 <td>{0}</td>
                                 <td>{1}</td>
                                 <td>{2}</td>
                            <tr>";
                var link = string.Format("<a href='/WishLists/Details/{0}'>Ver detalhes</a>", w.Id);
                return string.Format(row, w.Code, w.Name, link);
            });

            var rowsJoined = string.Join(string.Empty, rows);

            const string template = @"<table class='table table-striped table-hover'>
                                 <tr>
                                    <th>Código da lista</th>
                                    <th>Nome da lista</th>
                                    <th>Ação</th>
                                </tr>
                                {0}
                            </table>";

            return string.Format(template, rowsJoined);
        }


        [HttpGet]
        public ActionResult SearchByName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            var wishLists = _unitOfWork.WishLists.GetByName(name);

            if (!wishLists.Any())
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }

            var html = GenerateWishListsHtml(wishLists);

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchByCode(string code)
        {
            if (code == null) throw new ArgumentNullException("code");

            var wishList = _unitOfWork.WishLists.GetByCode(code);

            if (wishList == null)
            {
                return Json("404", JsonRequestBehavior.AllowGet);
            }

            var html = GenerateWishListsHtml(new List<WishList> { wishList });

            return Json(html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(WishListViewModel model)
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var user = _unitOfWork.Users.Get(User.Identity.GetUserId());
            var code = DateTime.UtcNow.Ticks / 10000;
            var wishList = new WishList
            {
                Code = code.ToString(),
                Name = model.Name,
                User = user,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWork.WishLists.Save(wishList);
            _unitOfWork.Commit();

            ViewBag.Message = string.Format("<div class='alert alert-success'>Lista criada com sucesso! Código da lista: <strong>{0}</strong></div>", code);
            ModelState.Clear();
            return View();
        }


    }
}