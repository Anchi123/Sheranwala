using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class HomeController : Controller
    {
        BL bl = new BL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = false;
            return View();
        }
        public ActionResult Branches()
        {
            ViewBag.Message = true;
            return View("About");
        }

        public ActionResult Contact()
        {
            if (TempData["success"] != null )
            {
                ViewBag.msg = "success";
                TempData.Remove("success");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactVM mod)
        {
            if(ModelState.IsValid)
            {
                if (bl.SaveContact(mod))
                {
                    TempData["success"] = "success";
                    return RedirectToAction("Contact");
                }
            }
            return View(mod);
        }

        public ActionResult _HomeFeatured()
        {
            var lst = bl.PropertyList().Where(x => x.IsFeatured).OrderByDescending(x => x.TransDate).Take(6).ToList();
            ViewBag.TypeList = bl.TypeList().Where(x => lst.Select(a => a.TypeID).Contains(x.TypeID)).ToList();
            ViewBag.Featured = lst;
            return PartialView();
        }

        public ActionResult _HomeRecent()
        {
            ViewBag.Recent = bl.PropertyList().Where(x => !x.IsFeatured).OrderByDescending(x => x.TransDate).Take(10).ToList();
            return PartialView();
        }
        public ActionResult _SubmitProperty()
        {
            int usr = bl.GetUserID(System.Web.HttpContext.Current);
            if(usr != 0)
                return PartialView();
            else
                return new EmptyResult();
        }
        public ActionResult Agents()
        {
            ViewBag.Agents = bl.AgentList();
                return View();
        }
        public ActionResult Gallery()
        {
            var mod = bl.GalleryList();
            return View(mod);
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogDetail()
        {
            return View();
        }
    }
}