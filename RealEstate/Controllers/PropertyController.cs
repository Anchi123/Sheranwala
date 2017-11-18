using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class PropertyController : Controller
    {
        RealEstateEntities db = new RealEstateEntities();
        BL bl = new BL();
        // GET: Property
        public ActionResult Index()
        {
            return View();
        }
        [CHAA]
        public ActionResult Create()
        {
            return View();
        }
        [CHAA]
        [HttpPost]
        public ActionResult Create(PropertyVM mod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (mod.SocietyID <= 0)
                    {
                        tblSociety soc = new tblSociety
                        {
                            CityID = mod.CityID,
                            Society = mod.OtherSociety,
                            Status = true
                        };
                        db.tblSocieties.Add(soc);
                        db.SaveChanges();
                        mod.SocietyID = soc.SocietyID;
                    }
                    tblProperty tbl = new tblProperty
                    {
                        CityID = mod.CityID,
                        Description = mod.Description,
                        IsFeatured = mod.IsFeatured,
                        LandArea = mod.LandArea,
                        Price = mod.Price,
                        PropertyTitle = mod.PropertyTitle,
                        Purpose = mod.Purpose,
                        SocietyID = mod.SocietyID,
                        Status = "H",
                        TransDate = DateTime.Now.AddHours(12),
                        TypeID = mod.TypeID,
                        UOMID = mod.UOMID,
                        UserID = bl.GetUserID(System.Web.HttpContext.Current),
                        Block = mod.Block,
                        ContactNo = mod.ContactNo,
                        Estate = mod.Estate,
                        IsDealer = mod.IsDealer,
                        Owner = mod.Owner,
                         PlotNo = mod.PlotNo
                    };
                    db.tblProperties.Add(tbl);
                    db.SaveChanges();

                    var coun = Request.Files.Count;
                    for (int i = 0; i < coun; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetExtension(file.FileName);
                            tblImage img = new tblImage
                            {
                                PropertyID = tbl.PropertyID,
                                ImagePath = fileName,
                                Status = true
                            };
                            db.tblImages.Add(img);
                            db.SaveChanges();
                            var path = Path.Combine(Server.MapPath("~/Images/"), img.ImageID+fileName);
                            file.SaveAs(path);
                        }
                    }
                    return RedirectToAction("Create");
                }
                catch (Exception)
                {
                }

            }
            return View(mod);
        }
        [CHAA]
        public ActionResult RemoveImage(long ID, long PropertyID)
        {
            try
            {
                
                var tbl = db.tblImages.Find(ID);
                db.tblImages.Remove(tbl);
                db.SaveChanges();
                return RedirectToAction("Edit", new { ID = PropertyID });
            }
            catch (Exception ex)
            {
            }
            return View("Edit");
        }
        [CHAA]
        public ActionResult Edit(long ID)
        {
            var mod = bl.GetPropertyForEdit(ID);
            return View(mod);
        }
        [CHAA]
        [HttpPost]
        public ActionResult Edit(PropertyVM mod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mo = db.tblProperties.Find(mod.PropertyID);

                    if (mod.SocietyID <= 0)
                    {
                        tblSociety soc = new tblSociety
                        {
                            CityID = mod.CityID,
                            Society = mod.OtherSociety,
                            Status = true
                        };
                        db.tblSocieties.Add(soc);
                        db.SaveChanges();
                        mod.SocietyID = soc.SocietyID;
                    }
                    //tblProperty tbl = new tblProperty
                    //{
                        mo.CityID = mod.CityID;
                        mo.Description = mod.Description;
                        //IsFeatured = mod.IsFeatured;
                        mo.LandArea = mod.LandArea;
                        mo.Price = mod.Price;
                        mo.PropertyTitle = mod.PropertyTitle;
                        mo.Purpose = mod.Purpose;
                        mo.SocietyID = mod.SocietyID;
                        //mo.Status = "H";
                        //TransDate = DateTime.Now;
                        mo.TypeID = mod.TypeID;
                        mo.UOMID = mod.UOMID;
                        mo.UserID = bl.GetUserID(System.Web.HttpContext.Current);
                        mo.Block = mod.Block;
                        mo.ContactNo = mod.ContactNo;
                        mo.Estate = mod.Estate;
                        mo.IsDealer = mod.IsDealer;
                        mo.Owner = mod.Owner;
                    mo.PlotNo = mod.PlotNo;
                    
                    //};
                    //db.tblProperties.Add(tbl);
                    db.SaveChanges();

                    var coun = Request.Files.Count;
                    for (int i = 0; i < coun; i++)
                    {
                        var file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetExtension(file.FileName);
                            tblImage img = new tblImage
                            {
                                PropertyID = mod.PropertyID,
                                ImagePath = fileName,
                                Status = true
                            };
                            db.tblImages.Add(img);
                            db.SaveChanges();
                            var path = Path.Combine(Server.MapPath("~/Images/"), img.ImageID + fileName);
                            file.SaveAs(path);
                            //return Json(new { Message = fileName });
                        }
                    }
                    return RedirectToAction("Properties", new { ID = mod.Purpose });
                }
                catch (Exception)
                {
                }

            }
            return View(mod);
        }
        [CHAA]
        public ActionResult UploadFile(HttpPostedFileBase uploadFile)
        {
            var file = Request.Files[0];
            //foreach (var v in file)
            //{
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                file.SaveAs(path);
                return Json(new { Message = fileName });
            }
            //}
            return Json(new { Message = "" });
            //return View();
        }

        public JsonResult CityList()
        {
            var lst = bl.CityList(); //db.tblCities.Where(x => x.Status).Select(x => new { CityID = x.CityID,City = x.City}).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TypeList()
        {
            var lst = bl.TypeList(); //db.tblTypes.Where(x => x.Status).Select(x => new { TypeID = x.TypeID, Type = x.Type }).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SocietyList(int CityID)
        {
            var lst = bl.SocietyList(CityID); //db.tblSocieties.Where(x => x.Status && x.CityID == CityID).Select(x => new { SocietyID = x.SocietyID, Society = x.Society }).ToList();
            lst.Add(new SocietyVM { SocietyID = 0, Society = "Other" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UOMList()
        {
            var lst = bl.UOMList(); //db.tblUOMs.Where(x => x.Status).Select(x => new { UOMID = x.UOMID, UOM = x.UOM }).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TypeLists()
        {
            var lst = bl.TypeList(); //db.tblTypes.Where(x => x.Status).Select(x => new { TypeID = x.TypeID, Type = x.Type }).ToList();
            lst.Insert(0, new TypeVM { TypeID = 0, Type = "All Types" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SocietyLists(int CityID)
        {
            var lst = bl.SocietyList(CityID); //db.tblSocieties.Where(x => x.Status && x.CityID == CityID).Select(x => new { SocietyID = x.SocietyID, Society = x.Society }).ToList();
            lst.Insert(0, new SocietyVM { SocietyID = 0, Society = "All Societies" });
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Search1(FormCollection frm)
        {
            SearchPropertyVM mod = new SearchPropertyVM();
            mod.Purpose = frm["Purpose1"];
            mod.Type = Convert.ToInt32(frm["TypeID1"]);
            mod.City = Convert.ToInt32(frm["CityID1"]);
            mod.Society = frm["SocietyID1"] == null ? 0 : Convert.ToInt32(frm["SocietyID1"]);
            mod.FromArea = String.IsNullOrEmpty(frm["FromArea1"]) ? 0 : Convert.ToInt32(frm["FromArea1"]);
            mod.ToArea = String.IsNullOrEmpty(frm["ToArea1"]) ? 0 : Convert.ToInt32(frm["ToArea1"]);
            mod.UOM = Convert.ToInt32(frm["UOMID1"]);
            mod.FromPrice = String.IsNullOrEmpty(frm["FromPrice1"]) ? 0 : Convert.ToInt64(frm["FromPrice1"]);
            mod.ToPrice = String.IsNullOrEmpty(frm["ToPrice1"]) ? 0 : Convert.ToInt32(frm["ToPrice1"]);
            mod.OrderID = 1;
            mod.PageNo = 1;


            int page = mod.PageNo;
            mod.PageNo = (mod.PageNo - 1) * 8;
            var lst = bl.SearchPropertyList(mod, bl.GetUserID(System.Web.HttpContext.Current));
            
            mod.TotalPage = (lst.Count() / 8) + 1;
            switch (mod.OrderID)
            {
                case 1:
                    lst = lst.OrderByDescending(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 2:
                    lst = lst.OrderBy(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 3:
                    lst = lst.OrderByDescending(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 4:
                    lst = lst.OrderBy(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
            }
            mod.PageNo = page;
            ViewBag.PropertyList = lst;
            //ViewBag.Para = mod;
            return View("SearchResult", mod);
            //return RedirectToAction("SearchResult", mod);

            //return View();
        }
        public ActionResult Search2(FormCollection frm)
        {
            SearchPropertyVM mod = new SearchPropertyVM();
            mod.Purpose = frm["Purpose2"];
            mod.Type = Convert.ToInt32(frm["TypeID2"]);
            mod.City = Convert.ToInt32(frm["CityID2"]);
            mod.Society = frm["SocietyID2"] == null ? 0 : Convert.ToInt32(frm["SocietyID2"]);
            mod.FromArea = String.IsNullOrEmpty(frm["FromArea2"]) ? 0 : Convert.ToInt32(frm["FromArea2"]);
            mod.ToArea = String.IsNullOrEmpty(frm["ToArea2"]) ? 0 : Convert.ToInt32(frm["ToArea2"]);
            mod.UOM = Convert.ToInt32(frm["UOMID2"]);
            mod.FromPrice = String.IsNullOrEmpty(frm["FromPrice2"]) ? 0 : Convert.ToInt64(frm["FromPrice2"]);
            mod.ToPrice = String.IsNullOrEmpty(frm["ToPrice2"]) ? 0 : Convert.ToInt32(frm["ToPrice2"]);
            mod.OrderID = 1;
            mod.PageNo = 1;


            int page = mod.PageNo;
            mod.PageNo = (mod.PageNo - 1) * 8;
            var lst = bl.SearchPropertyList(mod, bl.GetUserID(System.Web.HttpContext.Current));

            mod.TotalPage = (lst.Count() / 8) + 1;
            switch (mod.OrderID)
            {
                case 1:
                    lst = lst.OrderByDescending(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 2:
                    lst = lst.OrderBy(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 3:
                    lst = lst.OrderByDescending(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 4:
                    lst = lst.OrderBy(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
            }
            mod.PageNo = page;
            ViewBag.PropertyList = lst;
            //ViewBag.Para = mod;
            return View("SearchResult", mod);
            //return RedirectToAction("SearchResult", mod);

            //return View();
        }
        [HttpPost]
        public ActionResult SearchResult([Bind(Include = "Purpose,Type,City,Society,FromArea,ToArea,UOM,FromPrice,ToPrice,OrderID,PageNo,TotalPage")] SearchPropertyVM mod)
        {
            int page = mod.PageNo;
            mod.PageNo = (mod.PageNo - 1) * 8;
            var lst = bl.SearchPropertyList(mod,bl.GetUserID(System.Web.HttpContext.Current));
            
            mod.TotalPage = (lst.Count() / 8) + 1;
            switch (mod.OrderID)
            {
                case 1:
                    lst = lst.OrderByDescending(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 2:
                    lst = lst.OrderBy(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 3:
                    lst = lst.OrderByDescending(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 4:
                    lst = lst.OrderBy(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
            }
            mod.PageNo = page;
            ViewBag.PropertyList = lst;
            //ViewBag.Para = mod;
            return View(mod);
        }
        public ActionResult Properties(string ID)
        {
            SearchPropertyVM mod = new SearchPropertyVM();
            mod.Purpose = ID;
            mod.Type = 0;
            mod.City = 1;
            mod.Society = 0;
            mod.FromArea = 0;
            mod.ToArea = 0;
            mod.UOM = 1;
            mod.FromPrice = 0;
            mod.ToPrice = 0;
            mod.OrderID = 1;
            mod.PageNo = 1;


            int page = mod.PageNo;
            mod.PageNo = (mod.PageNo - 1) * 8;
            var lst = bl.SearchPropertyList(mod, bl.GetUserID(System.Web.HttpContext.Current));

            mod.TotalPage = (lst.Count() / 8) + 1;
            switch (mod.OrderID)
            {
                case 1:
                    lst = lst.OrderByDescending(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 2:
                    lst = lst.OrderBy(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 3:
                    lst = lst.OrderByDescending(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
                case 4:
                    lst = lst.OrderBy(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                    break;
            }
            mod.PageNo = page;
            ViewBag.PropertyList = lst;
            //ViewBag.Para = mod;
            return View("SearchResult", mod);
        }
        public ActionResult PropertyDetail(long ID)
        {
            var mod = bl.GetPropertyByID(ID);
            return View(mod);
        }
    }
}