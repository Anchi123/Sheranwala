using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RealEstate.Models
{
    public class BL
    {
        RealEstateEntities db = new RealEstateEntities();
        public List<CityVM> CityList()
        {
            var lst = db.tblCities.Where(x => x.Status).Select(x => new CityVM { CityID = x.CityID, City = x.City }).ToList();
            return lst;
        }
        public List<TypeVM> TypeList()
        {
            var lst = db.tblTypes.Where(x => x.Status).Select(x => new TypeVM { TypeID = x.TypeID, Type = x.Type }).ToList();
            //var lst = db.tblUsers.Where(x => x.).Select(x => new TypeVM { TypeID = x.TypeID, Type = x.Type }).ToList();

            return lst;
        }
        public List<SocietyVM> SocietyList(int CityID)
        {
            var lst = db.tblSocieties.Where(x => x.Status && x.CityID == CityID).Select(x => new SocietyVM { SocietyID = x.SocietyID, Society = x.Society }).ToList();
            return lst;
        }
        public List<SocietyVM> SocietyList()
        {
            var lst = db.tblSocieties.Where(x => x.Status).Select(x => new SocietyVM { SocietyID = x.SocietyID, Society = x.Society }).ToList();
            return lst;
        }
        public List<UOMVM> UOMList()
        {
            var lst = db.tblUOMs.Where(x => x.Status).Select(x => new UOMVM { UOMID = x.UOMID, UOM = x.UOM }).ToList();
            return lst;
        }
        public List<PropertyListVM> PropertyList()
        {
            var lst = db.tblProperties.Where(x => x.Status == "A").Select(x => new PropertyListVM
            {
                UOMID = x.UOMID,
                City = x.tblCity.City,
                CityID = x.CityID,
                Description = x.Description,
                IsFeatured = x.IsFeatured,
                LandArea = x.LandArea,
                Latitude = x.Latitude,
                Longitude = x.Latitude,
                Price = x.Price,
                PropertyID = x.PropertyID,
                PropertyTitle = x.PropertyTitle,
                Purpose = x.Purpose,
                Society = x.tblSociety.Society,
                SocietyID = x.SocietyID,
                Status = x.Status,
                TransDate = x.TransDate,
                Type = x.tblType.Type,
                TypeID = x.TypeID,
                UOM = x.tblUOM.UOM,
                User = x.tblUser.FullName,
                UserID = x.UserID,
                 Block = x.Block,
                //ContactNo = x.ContactNo,
                PlotNo = x.PlotNo,
                //  Owner = x.Owner,
                //   IsDealer = x.IsDealer,
                //    Estate = x.Estate,
                ImagePath = x.tblImages.Where(a => a.Status).Select(a => a.ImageID+a.ImagePath).FirstOrDefault() ?? "default.jpg"
            }).ToList();
            return lst;
        }
        public List<PropertyListVM> SearchPropertyList(SearchPropertyVM mod,int UserID)
        {
            List<PropertyListVM> lst = new List<PropertyListVM>();
            try
            {
                //mod.PageNo = (mod.PageNo - 1) * 8;

                lst = db.tblProperties.Where(x => x.Status == "A" && x.Purpose == mod.Purpose && (mod.Type == 0 || x.TypeID == mod.Type)
                && x.CityID == mod.City && (x.SocietyID == mod.Society || mod.Society == 0) && mod.FromArea <= x.LandArea && (mod.ToArea == 0 || mod.ToArea >= x.LandArea)
                && x.UOMID == mod.UOM && mod.FromPrice <= x.Price && (mod.ToPrice == 0 || mod.ToPrice >= x.Price)
                )
                    .Select(x => new PropertyListVM
                    {
                        UOMID = x.UOMID,
                        City = x.tblCity.City,
                        CityID = x.CityID,
                        Description = x.Description,
                        IsFeatured = x.IsFeatured,
                        LandArea = x.LandArea,
                        Latitude = x.Latitude,
                        Longitude = x.Latitude,
                        Price = x.Price,
                        PropertyID = x.PropertyID,
                        PropertyTitle = x.PropertyTitle,
                        Purpose = x.Purpose,
                        Society = x.tblSociety.Society,
                        SocietyID = x.SocietyID,
                        Status = x.Status,
                        TransDate = x.TransDate,
                        Type = x.tblType.Type,
                        TypeID = x.TypeID,
                        UOM = x.tblUOM.UOM,
                        User = x.tblUser.Username,
                        UserID = x.UserID,
                        Block = x.Block,
                         PlotNo = x.PlotNo,
                        //ContactNo = x.ContactNo,
                        //Estate = x.Estate,
                        ImagePath = x.tblImages.Where(a => a.Status).Select(a => a.ImageID + a.ImagePath).FirstOrDefault() ?? "default.jpg",
                        IsEdit = (x.UserID == UserID)
                    }).ToList();
                //switch (mod.OrderID)
                //{
                //    case 1:
                //        list = lst.OrderByDescending(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                //        break;
                //    case 2:
                //        list = lst.OrderBy(x => x.TransDate).Skip(mod.PageNo).Take(8).ToList();
                //        break;
                //    case 3:
                //        list = lst.OrderByDescending(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                //        break;
                //    case 4:
                //        list = lst.OrderBy(x => x.Price).Skip(mod.PageNo).Take(8).ToList();
                //        break;
                //}
                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }
        public PropertyListVM GetPropertyByID(long ID)
        {
            try
            {
                
                    var lst = db.tblProperties.Where(x => x.PropertyID == ID).Select(x => new PropertyListVM
                {
                    UOMID = x.UOMID,
                    City = x.tblCity.City,
                    CityID = x.CityID,
                    Description = x.Description,
                    IsFeatured = x.IsFeatured,
                    LandArea = x.LandArea,
                    Latitude = x.Latitude,
                    Longitude = x.Latitude,
                    Price = x.Price,
                    PropertyID = x.PropertyID,
                    PropertyTitle = x.PropertyTitle,
                    Purpose = x.Purpose,
                    Society = x.tblSociety.Society,
                    SocietyID = x.SocietyID,
                    Status = x.Status,
                    TransDate = x.TransDate,
                    Type = x.tblType.Type,
                    TypeID = x.TypeID,
                    UOM = x.tblUOM.UOM,
                    User = x.tblUser.Username,
                    UserID = x.UserID,
                    PlotNo = x.PlotNo,
                    Block = x.Block,
                    ImgList = x.tblImages.Where(a => a.Status).Select(a => a.ImageID + a.ImagePath).ToList(),
                    ContactNo = x.tblUser.tblAgents.FirstOrDefault().ContactNo
                }).FirstOrDefault();
                if(!lst.ImgList.Any())
                {
                    lst.ImgList.Add("default.jpg");
                }
                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public PropertyVM GetPropertyForEdit(long ID)
        {
            try
            {
                return db.tblProperties.Where(x => x.PropertyID == ID).Select(x => new PropertyVM
                {
                    UOMID = x.UOMID,
                    CityID = x.CityID,
                    Description = x.Description,
                    IsFeatured = x.IsFeatured,
                    LandArea = x.LandArea,
                    Latitude = x.Latitude,
                    Longitude = x.Latitude,
                    Price = x.Price,
                    PropertyID = x.PropertyID,
                    PropertyTitle = x.PropertyTitle,
                    Purpose = x.Purpose,
                    SocietyID = x.SocietyID,
                    Status = x.Status,
                    TransDate = x.TransDate,
                    TypeID = x.TypeID,
                    UserID = x.UserID,
                    Block = x.Block,
                    ContactNo = x.ContactNo,
                    Estate = x.Estate,
                    ImgList = x.tblImages.Where(a => a.Status).Select(a => a.ImageID + a.ImagePath).ToList(),
                    IsDealer = x.IsDealer,
                    Owner = x.Owner,
                    PlotNo = x.PlotNo,
                    OtherSociety = ""
                }).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public int GetUserID(HttpContext context)
        {
            try
            {
                HttpCookie authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
                int UserID = 0;
                if (authCookie != null)
                {
                    // Get the forms authentication ticket.
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    UserID = Convert.ToInt32(authTicket.UserData);
                }
                return UserID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public string GetUsername(HttpContext context)
        {
            try
            {
                HttpCookie authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
                string Username = "";
                if (authCookie != null)
                {
                    // Get the forms authentication ticket.
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    Username = authTicket.Name;
                }
                return Username;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public bool ChangePassword(int usrId, string oldPsw, string NewPsw)
        {
            try
            {
                var user = db.tblUsers.Where(x => x.UserID.Equals(usrId) && x.Password.Equals(oldPsw)).SingleOrDefault();
                if (user == null)
                {
                    return false;
                }
                else
                {
                    user.Password = NewPsw;
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public string GetUserValidation(LoginVM mod)
        {
            try
            {
                var usr = db.tblUsers.Where(x => x.Username == mod.Username && x.Password == mod.Password && x.Status).FirstOrDefault();
                if (usr != null)
                    return usr.UserID.ToString();
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        public List<AgentVM> AgentList()
        {
            try
            {
                return db.tblAgents.Where(x => x.Status).Select(x => new AgentVM
                {
                    AgentID = x.AgentID,
                    AgentName = x.AgentName,
                    BranchID = x.BranchID,
                    ContactNo = x.ContactNo,
                    Description = x.Description,
                    Email = x.Email,
                    Img = x.Img,
                    UserID = x.UserID,
                    Status = x.Status
                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<AgentVM>();
            }
           
            
        }
        public List<GalleryVM> GalleryList()
        {
            try
            {
                return db.tblGalleries.Where(x => x.Status).Select(x => new GalleryVM
                {
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    RowID = x.RowID,
                    Title = x.Title,
                    Transdate = x.Transdate
                }).ToList();
            }
            catch (Exception)
            {
                return new List<GalleryVM>();
            }
        }
        public bool SaveContact(ContactVM mod)
        {
            try
            {
                tblContact tbl = new tblContact
                {
                    Description = mod.Description,
                    Email = mod.Email,
                    FullName = mod.FullName,
                    PhoneNo = mod.PhoneNo,
                    Subject = mod.Subject
                };
                db.tblContacts.Add(tbl);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<PropertyVM> AdminPropertyList()
        {
            try
            {
                List<PropertyVM> tblproperties = db.tblProperties.ToList().Select(c => new PropertyVM
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
                    User = c.tblUser.FullName
                }).ToList();
                return tblproperties;
            }
            catch (Exception)
            {
                return new List<PropertyVM>();
            }
        }
    }
}