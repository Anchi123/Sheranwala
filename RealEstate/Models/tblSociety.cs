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
    
    public partial class tblSociety
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSociety()
        {
            this.tblProperties = new HashSet<tblProperty>();
        }
    
        public int SocietyID { get; set; }
        public string Society { get; set; }
        public int CityID { get; set; }
        public bool Status { get; set; }
    
        public virtual tblCity tblCity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProperty> tblProperties { get; set; }
    }
}