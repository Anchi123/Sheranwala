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
    
    public partial class tblAgent
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
    
        public virtual tblUser tblUser { get; set; }
        public virtual tblBranch tblBranch { get; set; }
    }
}