using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RealEstate.Models
{
    public class ViewModel
    {
    }
    public class CHAA : AuthorizeAttribute
    {
        BL bl = new BL();
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            int UserID = 0;
            if (authCookie != null)
            {
                // Get the forms authentication ticket.
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    UserID = Convert.ToInt32(authTicket.UserData);
                }
                catch (Exception)
                {
                }
            }

            if (UserID == 0)
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
        }
    }
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
    }
    public class ChangePasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
    public class PropertyVM
    {
        [Key]
        public long PropertyID { get; set; }
        [MaxLength(50)]
        public string PropertyTitle { get; set; }
        public string Purpose { get; set; }
        public int TypeID { get; set; }
        public int CityID { get; set; }
        public int SocietyID { get; set; }
        public string OtherSociety { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal LandArea { get; set; }
        public int UOMID { get; set; }
        public int UserID { get; set; }
        public string User { get; set; }
        public System.DateTime TransDate { get; set; }
        public string Status { get; set; }
        public bool IsFeatured { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string PlotNo { get; set; }
        public string Block { get; set; }
        public Nullable<bool> IsDealer { get; set; }
        public string Owner { get; set; }
        public string ContactNo { get; set; }
        public string Estate { get; set; }
        public List<string> ImgList { get; set; }
    }

    public class PropertyListVM
    {
        public long PropertyID { get; set; }
        public string PropertyTitle { get; set; }
        public string Purpose { get; set; }
        public int TypeID { get; set; }
        public string Type { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int SocietyID { get; set; }
        public string Society { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal LandArea { get; set; }
        public int UOMID { get; set; }
        public string UOM { get; set; }
        public int UserID { get; set; }
        public string User { get; set; }
        public System.DateTime TransDate { get; set; }
        public string Status { get; set; }
        public bool IsFeatured { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string PlotNo { get; set; }
        public string Block { get; set; }
        public Nullable<bool> IsDealer { get; set; }
        public string Owner { get; set; }
        public string ContactNo { get; set; }
        public string Estate { get; set; }
        public string ImagePath { get; set; }
        public bool IsEdit { get; set; }
        public List<string> ImgList { get; set; }
    }
    public class TypeVM
    {
        public int TypeID { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
    public class AgentVM
    {
        public int AgentID { get; set; }
        public string AgentName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int UserID { get; set; }
        public string Img { get; set; }
        public int BranchID { get; set; }
    }
    public class BranchVM
    {

        public int BranchID { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public bool Status { get; set; }
    }
    public class CityVM
    {
        public int CityID { get; set; }
        public string City { get; set; }
        public bool Status { get; set; }
    }
    public class ImageVM
    {
        public long ImageID { get; set; }
        public string ImagePath { get; set; }
        public long PropertyID { get; set; }
        public bool Status { get; set; }
    }
    public class SocietyVM
    {
        public int SocietyID { get; set; }
        public string Society { get; set; }
        public int CityID { get; set; }
        public bool Status { get; set; }
    }
    public class UOMVM
    {
        public int UOMID { get; set; }
        public string UOM { get; set; }
        public bool Status { get; set; }
    }
    public class UserVM
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }

    public class SearchPropertyVM
    {
        public string Purpose { get; set; }
        public int Type { get; set; }
        public int City { get; set; }
        public int Society { get; set; }
        public int FromArea { get; set; }
        public int ToArea { get; set; }
        public int UOM { get; set; }
        public long FromPrice { get; set; }
        public long ToPrice { get; set; }
        public int OrderID { get; set; }
        public int PageNo { get; set; }
        public int TotalPage { get; set; }

    }
    public class GalleryVM
    {
        public int RowID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public System.DateTime Transdate { get; set; }
        public int UserID { get; set; }
        public bool Status { get; set; }
    }
    public class ContactVM
    {
        public long RowID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}