using System.Web.Mvc;
using FantasyStore.Infrastructure;

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
    }
}