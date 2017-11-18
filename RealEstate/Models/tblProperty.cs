//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProperty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProperty()
        {
            this.tblImages = new HashSet<tblImage>();
        }
    
        public long PropertyID { get; set; }
        public string PropertyTitle { get; set; }
        public string Purpose { get; set; }
        public int TypeID { get; set; }
        public int CityID { get; set; }
        public int SocietyID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal LandArea { get; set; }
        public int UOMID { get; set; }
        public int UserID { get; set; }
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
    
        public virtual tblCity tblCity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblImage> tblImages { get; set; }
        public virtual tblSociety tblSociety { get; set; }
        public virtual tblType tblType { get; set; }
        public virtual tblUOM tblUOM { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
