using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Infrastructure;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class OrderController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}