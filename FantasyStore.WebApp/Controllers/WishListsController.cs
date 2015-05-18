using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
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

            ViewBag.WishLists = wishLists;
            
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WishListViewModel model)
        {
            if (User.Identity.GetUserId() == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var user = _unitOfWork.Users.Get(User.Identity.GetUserId());
            var code = DateTime.UtcNow.Ticks/10000;
            var wishList = new WishList
            {
                Code = code.ToString(),
                Name = model.Name,
                User = user
            };

            _unitOfWork.WishLists.Save(wishList);
            _unitOfWork.Commit();

            ViewBag.Message = string.Format("<div class='alert alert-success'>Lista criada com sucesso! Código da lista: <strong>{0}</strong></div>", code);
            ModelState.Clear();
            return View();
        }


    }
}