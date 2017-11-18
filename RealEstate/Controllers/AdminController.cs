﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [CHAA]
    public class AdminController : Controller
    {
        private RealEstateEntities db = new RealEstateEntities();
        private PropertyController pro = new PropertyController();
        private BL bl = new BL();
        public ActionResult Index()
        {
            //ViewData["TypeVD"] = db.tblTypes.Where(x => x.Status).Select(x => new { TypeID = x.TypeID, Type = x.Type }).ToList();
            //ViewData["CityVD"] = db.tblCities.Where(x => x.Status).Select(x => new { CityID = x.CityID, City = x.City }).ToList();
            //ViewData["SocietyVD"] = db.tblSocieties.Where(x => x.Status).Select(x => new { SocietyID = x.SocietyID, Society = x.Society }).ToList();
            //ViewData["UOMVD"] = db.tblUOMs.Where(x => x.Status).Select(x => new { UOMID = x.UOMID, UOM = x.UOM }).ToList();
            ViewData["TypeVD"] = bl.TypeList();
            ViewData["CityVD"] = bl.CityList();
            ViewData["SocietyVD"] = bl.SocietyList();
            ViewData["UOMVD"] = bl.UOMList();

            ViewData["PurposeVD"] = new List<SelectListItem>() {
              new SelectListItem() {
                Text = "Sale", Value = "Sale"
              },
              new SelectListItem() {
                Text = "Rent", Value = "Rent"
              }
          };
            ViewData["StatusVD"] = new List<SelectListItem>() {
              new SelectListItem() {
                Text = "Hold", Value = "H"
              },
              new SelectListItem() {
                Text = "Acknowledge", Value = "A"
              },
              new SelectListItem() {
                Text = "Cancel", Value = "C"
              }
          };
            return View();
        }

        public ActionResult tblProperties_Read([DataSourceRequest]DataSourceRequest request)
        {
            //IQueryable<PropertyVM> tblproperties = db.tblProperties.Select(c => new PropertyVM
            //{
            //    PropertyID = c.PropertyID,
            //    PropertyTitle = c.PropertyTitle,
            //    Purpose = c.Purpose,
            //    TypeID = c.TypeID,
            //    CityID = c.CityID,
            //    SocietyID = c.SocietyID,
            //    Description = c.Description,
            //    Price = c.Price,
            //    LandArea = c.LandArea,
            //    UOMID = c.UOMID,
            //    UserID = c.UserID,
            //    TransDate = c.TransDate,
            //    Status = c.Status,
            //    IsFeatured = c.IsFeatured,
            //    Block = c.Block,
            //    ContactNo = c.ContactNo,
            //    Estate = c.Estate,
            //    IsDealer = c.IsDealer,
            //    Owner = c.Owner,
            //    PlotNo = c.PlotNo,
            //    User = c.tblUser.FullName
            //});
            var lst = bl.AdminPropertyList();
            DataSourceResult result = lst.ToDataSourceResult(request, c => new PropertyVM
            {
                PropertyID = c.PropertyID,
                PropertyTitle = c.PropertyTitle,
                Purpose = c.Purpose,
                TypeID = c.TypeID,
                CityID = c.CityID,
                SocietyID = c.SocietyID,
                Description = c.Description,
                Price = c.Price,
                LandArea = c.LandArea,
                UOMID = c.UOMID,
                UserID = c.UserID,
                TransDate = c.TransDate.Date,
                Status = c.Status,
                IsFeatured = c.IsFeatured,
                Block = c.Block,
                ContactNo = c.ContactNo,
                Estate = c.Estate,
                IsDealer = c.IsDealer,
                Owner = c.Owner,
                PlotNo = c.PlotNo,
                User  = c.User
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult tblProperties_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PropertyVM> tblproperties)
        {
            //ModelState["TransDate"].Errors.Clear();
            //&& ModelState.IsValid
            var entities = new List<tblProperty>();
            if (tblproperties != null)
            {
                foreach (var c in tblproperties)
                {
                    var entity = db.tblProperties.Find(c.PropertyID);
                    entity.PropertyTitle = c.PropertyTitle;
                    entity.Purpose = c.Purpose;
                    entity.TypeID = c.TypeID;
                    entity.CityID = c.CityID;
                    entity.SocietyID = c.SocietyID;
                    entity.Description = c.Description;
                    entity.Price = c.Price;
                    entity.LandArea = c.LandArea;
                    entity.UOMID = c.UOMID;
                    entity.Status = c.Status;
                    entity.IsFeatured = c.IsFeatured;
                    entity.Block = c.Block;
                    entity.ContactNo = c.ContactNo;
                    entity.Estate = c.Estate;
                    entity.IsDealer = c.IsDealer;
                    entity.Owner = c.Owner;
                    entity.PlotNo = c.PlotNo;
                    entities.Add(entity);
                    db.tblProperties.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState, tblProperty => new PropertyVM
            {
                PropertyTitle = tblProperty.PropertyTitle,
                Purpose = tblProperty.Purpose,
                Description = tblProperty.Description,
                Price = tblProperty.Price,
                LandArea = tblProperty.LandArea,
                TransDate = tblProperty.TransDate,
                Status = tblProperty.Status,
                IsFeatured = tblProperty.IsFeatured,
                CityID = tblProperty.CityID,
                PropertyID = tblProperty.PropertyID,
                SocietyID = tblProperty.SocietyID,
                TypeID = tblProperty.TypeID,
                UOMID = tblProperty.UOMID,
                UserID = tblProperty.UserID,
                Block = tblProperty.Block,
                ContactNo = tblProperty.ContactNo,
                Estate = tblProperty.Estate,
                IsDealer = tblProperty.IsDealer,
                Owner = tblProperty.Owner,
                PlotNo = tblProperty.PlotNo,
                 User = tblProperty.tblUser.FullName
            }));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
