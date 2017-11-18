using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RealEstate.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        BL bl = new BL();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Username,Password")] LoginVM mod)
        {
            string id = bl.GetUserValidation(mod);
            if (id != "")
            {
                //FormsAuthentication.SetAuthCookie(mod.Username, true);
                FormsAuthenticationTicket tic = new FormsAuthenticationTicket(1,
                    mod.Username,
                    DateTime.Now,
                    DateTime.Now.AddHours(24),
                    false,
                    id,
                    FormsAuthentication.FormsCookiePath);
                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(tic);
                // Create the cookie.
                Response.Cookies.Add(new
                HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Username and Passord Incorrect");
            mod.Password = "";
            return View(mod);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        [CHAA]

        public ActionResult ChangePassword()
        {
            ViewBag.msg = "";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM mod)
        {
            if (ModelState.IsValid)
            {
                int userId = bl.GetUserID(System.Web.HttpContext.Current);
                if (bl.ChangePassword(userId, mod.OldPassword, mod.Password))
                {
                    ViewBag.msg = "success";
                    return View(new ChangePasswordVM());
                }
                ViewBag.msg = "fail";
                return View(new ChangePasswordVM());
            }
            return View(new ChangePasswordVM());
        }

        public ActionResult LoginName()
        {
            var str = bl.GetUsername(System.Web.HttpContext.Current);
            return PartialView("_Login", str);
        }
    }
}