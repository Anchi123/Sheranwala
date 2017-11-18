using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstate.Models;
using System.IO;

namespace RealEstate.Controllers
{
    [CHAA]
    public class GalleriesController : Controller
    {
        private RealEstateEntities db = new RealEstateEntities();
        BL bl = new BL();

        // GET: Galleries
        public ActionResult Index()
        {
            return View(bl.GalleryList());
        }

        // GET: Galleries/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblGallery tblGallery = db.tblGalleries.Find(id);
        //    if (tblGallery == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblGallery);
        //}

        // GET: Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RowID,Title,Description")] GalleryVM mod)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetExtension(file.FileName);
                    tblGallery tbl = new tblGallery
                    {
                        Description = mod.Description,
                        Title = mod.Title,
                        Status = true,
                        Transdate = DateTime.Now.AddHours(12),
                        UserID = bl.GetUserID(System.Web.HttpContext.Current),
                        ImagePath = fileName
                    };
                    db.tblGalleries.Add(tbl);
                    db.SaveChanges();
                    var path = Path.Combine(Server.MapPath("~/Images/"), "gal" + tbl.RowID + fileName);
                    file.SaveAs(path);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No Image Found");
                }

            }

            return View(mod);
        }

        // GET: Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGallery tblGallery = db.tblGalleries.Find(id);
            if (tblGallery == null)
            {
                return HttpNotFound();
            }
            GalleryVM mod = new GalleryVM
            {
                Description = tblGallery.Description,
                Title = tblGallery.Title,
                ImagePath = tblGallery.ImagePath,
                RowID = tblGallery.RowID,
                 Status = tblGallery.Status
            };
            return View(mod);
        }

        // POST: Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RowID,Title,Description,Status")] GalleryVM mod)
        {
            if (ModelState.IsValid)
            {
                tblGallery tbl = db.tblGalleries.Find(mod.RowID);
                tbl.Description = mod.Description;
                    tbl.Title = mod.Title;
                    tbl.Status = true;
                
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetExtension(file.FileName);
                    tbl.ImagePath = fileName;
                    var path = Path.Combine(Server.MapPath("~/Images/"), "gal" + tbl.RowID + fileName);
                    file.SaveAs(path);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mod);
        }

        // GET: Galleries/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblGallery tblGallery = db.tblGalleries.Find(id);
        //    if (tblGallery == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblGallery);
        //}

        // POST: Galleries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblGallery tblGallery = db.tblGalleries.Find(id);
        //    db.tblGalleries.Remove(tblGallery);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
