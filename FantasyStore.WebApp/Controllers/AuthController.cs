using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FantasyStore.Domain;
using FantasyStore.Infrastructure;
using FantasyStore.WebApp.Extensions;
using FantasyStore.WebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace FantasyStore.WebApp.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly UserManager<User> _userManager;

        public AuthController()
            : this(Startup.UserManagerFactory.Invoke())
        {

        }

        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private User CurrentUser
        {
            get
            {
                return _userManager.FindById(User.Identity.GetUserId());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

        //
        // GET: /Auth/
        public ActionResult LogIn()
        {
            return View();
        }

        private async Task SignIn(User user)
        {
            var identity = await _userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            GetAuthenticationManager().SignIn(identity);
        }

        
        public async Task<ActionResult> Register()
        {
            return View();
        }

        private string DisplaySuccessMessage(string message)
        {
            return string.Format("<div class='alert alert-success'>{0}</div>", message);
        }

        [HttpPost]
        public async Task<ActionResult> MyAccount(UpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            CurrentUser.FirstName = model.FirstName;
            CurrentUser.LastName = model.LastName;
            CurrentUser.Document = model.Document;
            CurrentUser.BirthDate = Convert.ToDateTime(model.BirthDate, CultureInfo.GetCultureInfo("pt-BR"));
            CurrentUser.UserName = model.Email;

            await _userManager.UpdateAsync(CurrentUser);
            ViewBag.Confirmation = DisplaySuccessMessage("Dados atualizados com sucesso");
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult MyAccount()
        {
            if (CurrentUser == null)
            {
                return Redirect("/Home");
            }

            var model = CurrentUser.ToViewModel();
            
            ViewBag.FirstName = model.FirstName;
            ViewBag.LastName = model.LastName;
            ViewBag.Document = model.Document;
            ViewBag.Email = model.Email;
            ViewBag.BirthDate = model.BirthDate;
           
            return View();
        }

        [HttpGet]
        public ActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAddress(AddressViewModel model)
        {
            var userId = User.Identity.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            var user = _unitOfWork.Users.Get(userId);
            var address = new Address
            {
                Cep = model.Cep,
                City = model.City,
                Complement = model.Complement,
                HouseNumber = model.HouseNumber,
                IsDeliveryAddress = true,
                State = model.State,
                Street = model.Street,
                User = user
            };

            _unitOfWork.Addresses.Insert(address);
            _unitOfWork.Commit();
            ModelState.Clear();
            @ViewBag.Message = @"<div class='alert alert-success'>Endereço cadastrado com sucesso. 
                <strong><a href='/Checkout/Cart'>Voltar ao carrinho de compras</a></strong></div>";
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            DateTime? birthDate;
            try
            {
                birthDate = DateTime.Parse(model.BirthDate, CultureInfo.GetCultureInfo("pt-BR"));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Document = model.Document,
                BirthDate = birthDate
            };

            var result = await _userManager.CreateAsync(user, model.Password);
           
            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            var session = Session["CartId"];
            if (session != null)
            {
                Session.Clear();
            }

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }

      
    }

    
}